using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Need 
{
    public int idx;
    public int cnt;
    public int startCnt;
    public float time;
    // copy constructor
    public Need(Need _previousResource)
    {
        idx = _previousResource.idx;
        cnt = _previousResource.cnt;
        startCnt = _previousResource.startCnt;
        time = _previousResource.time;
    }
    public Need(int _idx, int _cnt, int _startCnt, float _time)
    {
        idx = _idx;
        cnt = _cnt;
        startCnt = _startCnt;
        time = _time;
    }
}


public class Director : MonoBehaviour
{
    public Brain m_brain;
    public Plane m_plane;
    public NotificationHandler m_notificationHandler;


    float m_difficulty = 0.4f;
    [System.NonSerialized] public List<List<Need>> m_needs = new List<List<Need>>();
    int cnt_needs = 0;

    void Start()
    {
        Initialize();
        InvokeRepeating("Logic",1.0f, 5.0f);
        InvokeRepeating("UpdateNeeds",1.0f, 1.0f);
    }
    void Initialize() {
        for (int i = 0; i < 8; i++)
        {
            m_needs.Add(new List<Need>());
        }
    }
    void Logic()
    {
        Difficulty();
        GetNewNeeds();
    }
    public void DropNeed(int _charIdx,int _idxItem) {
        m_needs[_charIdx][0].cnt--;
        if (true || m_needs[_charIdx][0].cnt == 0)
        {
            m_needs[_charIdx].Clear();
            string resourceName = Brain.resources[_idxItem].name;
            GameObject.FindGameObjectWithTag("Notifications").GetComponent<NotificationHandler>().SendNotification(_charIdx, new string[]{
                "Psst... thanks for the " + resourceName + ".",
                "You finally brought the " +  resourceName +"! Be faster next time if you want me to stay in business.",
                "Thanks for bringing me " + resourceName +" pilot!",
                "Your hard work is appreciated, Compatriot.",
                "Finally, that took you all day. Nice " + resourceName,
                "Thanks! I knew you could do it, kid.",
                "Thank you for stopping by.",
                "Thanks, I owe you one."
            }[_charIdx]);

        }
    }
    void UpdateNeeds() { 
        for (int idxChar = 0; idxChar < 8; idxChar++)
        {
            if (m_needs[idxChar].Count > 0 && m_brain.m_charAlive[idxChar])
            {
                if (m_needs[idxChar][0].time <= 0)
                {
                    m_brain.KillChar(idxChar);
                }
                else
                {
                    m_needs[idxChar][0].time -= 1;

                }
            }
        }
    }
    void GetNewNeeds()
    {
        List<int> unavailableCharacters = new List<int>();
        List<int> busyCharacters = new List<int>();
        for (int i = 0; i < 8; i++)
        {
            if (m_needs[i].Count > 0 || !m_brain.m_charAlive[i])
            {
                unavailableCharacters.Add(i);
            }
            if (m_needs[i].Count > 0)
            {
                busyCharacters.Add(i);
            }
        }

        bool shouldAddNeed = m_difficulty > 0.9f ? busyCharacters.Count < 5 :
            m_difficulty > 0.7f ? busyCharacters.Count < 4 :
            m_difficulty > 0.5f ? busyCharacters.Count < 3 :
            m_difficulty > 0.2f ? busyCharacters.Count < 2 : 
            m_difficulty > 0.0f ? busyCharacters.Count < 1 : 
            false;

        int idxChar = Extensions.randomExclude(0,8,unavailableCharacters);
        if (shouldAddNeed && idxChar < 8)
        {
            if (!unavailableCharacters.Contains(idxChar))
            {
                unavailableCharacters.Add(idxChar);
            }
            int randomA = Extensions.randomExclude(0,8,unavailableCharacters);
            int randomB = Extensions.randomExclude(0,8,unavailableCharacters);
            Resource A = m_brain.m_inventory[idxChar][randomA];
            Resource B = m_brain.m_inventory[idxChar][randomA];
            int idxResource = A.amount < B.amount ? randomA : randomB;
            int amount = UnityEngine.Random.Range(1, 3); // TODO difficulty balancing

            /*
            float time = UnityEngine.Random.Range(1, 
                    (int)( (1 - m_difficulty) * (1.0f + UnityEngine.Random.Range(0,4f)) )
                ) * 10.0f;
            */
            /*
            float time = UnityEngine.Random.Range(1, 
                    1 + (int)( (1 - m_difficulty) * (1.0f + UnityEngine.Random.Range(0,4f - UnityEngine.Random.Range(1.0f,3.0f,) )) )
                ) * 10.0f;
            */
            float timeToFinish = (int)UnityEngine.Random.Range(1f, 1f + (
                (1.0f-m_difficulty)*UnityEngine.Random.Range(0.5f,2.0f*UnityEngine.Random.Range(0.75f - m_difficulty*0.4f,1.5f - m_difficulty*1.0f))
                ))*(37.0f*(1f - m_difficulty*0.0f));

            m_needs[idxChar].Add(new Need(idxResource,amount, (int)m_brain.m_inventory[idxChar][idxResource].amount,timeToFinish));


            string resourceName = Brain.resources[idxResource].name;
            GameObject.FindGameObjectWithTag("Notifications").GetComponent<NotificationHandler>().SendNotification(idxChar, new string[]{
                "Psst... can you get me some " + resourceName + "?",
                "Bring me some " +  resourceName + " please! You wouldn't want me to lose out on profits. ",
                "Heya! I need some more" + resourceName + "to operate my peanut stand." ,
                "Deliver a few " + resourceName + " for me, Compatriot.",
                "Deliver me some " + resourceName + ".",
                "Get me some" + resourceName + "as soon as you can, I believe in ya kid.",
                "Can you get me some " + resourceName + ", sweetie?",
                "Bring me some " + resourceName + ", would ya?"
            }[idxChar]);
        }

    }

    void Difficulty()
    {
        float phaseAtime = 0.0f;
        float phaseBtime = 50.0f;
        float phaseCtime = 80.0f;
        float phaseDtime = 400.0f;

        float phaseAdiff = 0.1f;
        float phaseBdiff = 0.3f;
        float phaseCdiff = 0.8f;
        float phaseDdiff = 1.0f;
        float t = Time.time;
        m_difficulty = Mathf.Lerp(phaseAdiff,phaseBdiff, Mathf.Lerp(0,1, (Time.time - phaseAtime)/(phaseBtime - phaseAtime)));
        m_difficulty = Mathf.Lerp(m_difficulty,phaseCdiff, Mathf.Lerp(0,1, (Time.time - phaseAtime - phaseBtime)/(phaseCtime - phaseBtime)));
        m_difficulty = Mathf.Lerp(m_difficulty,phaseDdiff, Mathf.Lerp(0,1, (Time.time - phaseAtime - phaseBtime - phaseCtime)/(phaseDtime - phaseCtime)));
    }

    void Update()
    {
        
    }
}


class Extensions
{
    public static int randomExclude(int _start, int _end, List<int> _excludes){
        int n = _end;
        int[] x = _excludes.ToArray();
        Array.Sort(x);
        System.Random r = new System.Random();
        int result = r.Next(n - x.Length);

        for (int i = 0; i < x.Length; i++)
        {
            if (result < x[i])
                return result;
            result++;
        }
        return result;
    }
}


