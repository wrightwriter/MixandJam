using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#pragma warning disable 0168
#pragma warning disable 0219
#pragma warning disable 0414 



public class Brain : MonoBehaviour
{
    static int CATNIP = 0;
    static int LEMONADE = 1;
    static int NUTS = 2;
    static int PARTS = 3;
    static int MEAT = 4;
    static int TENNISBALL = 5;
    static int BERRIES = 6;
    static int FISH = 7;
    public static List<Resource> resources = new List<Resource>() { 
        new Resource( "catnip", 0.0f),
        new Resource( "lemonade", 0.0f),
        new Resource( "nuts", 0.0f),
        new Resource( "parts", 0.0f),
        new Resource( "meat", 0.0f),
        new Resource( "tennisball", 0.0f),
        new Resource( "berries", 0.0f),
        new Resource( "fish", 0.0f),
    };
    [System.NonSerialized] public List<List<Resource>> m_inventory = new List<List<Resource>>();
    [System.NonSerialized] public List<float> m_velocity = new List<float>();
    [System.NonSerialized] public List<bool> m_charAlive = new List<bool>();
    

    public List<GameObject> m_traders;
    [System.NonSerialized] public List<int> m_playerCarrying = new List<int>();
    public GameObject m_player;
    public Director m_director;
    public TraderPopup m_traderPopup;


    void Start()
    {
        Initialize();
        InvokeRepeating("Logic", 1.0f, 1.0f);
    }
    void Initialize() { 
        for (int idxTrader = 0; idxTrader < 8; idxTrader++) {

            List<Resource> resourceCopy = new List<Resource>(resources.Count);
            resources.ForEach((item) =>
            {
                resourceCopy.Add( new Resource(item));
            });
            m_inventory.Add(resourceCopy);
            m_velocity.Add(0.0f);


            for (int idxResource = 0; idxResource < 8; idxResource++) {
                m_inventory[idxTrader][idxResource].amount = UnityEngine.Random.Range(0, 4.0f);
            }

            m_charAlive.Add(true);
        }


    }

    void Logic()
    {
        for (int idxTrader = 0; idxTrader < 8; idxTrader++) {
            m_velocity[idxTrader] = Mathf.PerlinNoise(Time.fixedTime*0.05f + idxTrader*200f, 0.1f)  + 0.1f;
            m_inventory[idxTrader][idxTrader].amount += m_velocity[idxTrader] * 0.05f;

            UnityEngine.Random.State prevRandState = UnityEngine.Random.state;
            UnityEngine.Random.InitState(1111111);
            for (int idxResource = 0; idxResource < 8; idxResource++) {
                Mathf.PerlinNoise( idxTrader * 100.0f, idxResource * 2.0f + 0.1f * Time.time*(0.5f + UnityEngine.Random.Range(0f,1f)));
            }
            UnityEngine.Random.state = prevRandState;

        }

    }

    public void KillChar( int idxChar)
    {
        m_charAlive[idxChar] = false;
        m_director.m_needs[idxChar].Clear();

        GameObject.FindGameObjectWithTag("Notifications").GetComponent<NotificationHandler>().SendNotification(idxChar, new string[]{
            "Ah no, they found me out!",
            "Nooo! My prized lemonade stand is going down! ",
            "Oh no! They got me!",
            "Remember Compatriot: Courage is a person's most imporat attribute.",
            "Dammit, I got shot down.",
            "I'm going down but I still believe in you!",
            "I'm falling! Oh dearest, all my berries are gonna be ruined!",
            "Snap! Looks like my shop got detonated."
        }[idxChar]);
        m_traders[idxChar].GetComponent<Trader>().Die();
    }
    public void PickupOrDropItemFromTrader( TraderPopupMessage _input)
    {

        int charIdx = (int)_input.traderIdx;
        int resIdx = (int)_input.resourceIdx;
        if (_input.pickupOrDrop == "pickup")
        {

            if (m_playerCarrying.Count < 4)
            {

                if (m_inventory[charIdx][resIdx].amount >= 1.0f)
                {
                    m_playerCarrying.Add(resIdx);
                    m_inventory[charIdx][resIdx].amount--;

                    m_traderPopup.GetComponent<AudioSource>().clip = m_traderPopup.m_pickupItem;
                    m_traderPopup.GetComponent<AudioSource>().Play();
                }

            }
        } else
        {
            if (m_playerCarrying.Count > 0)
            {
                for (int idxItem = 0; idxItem < m_playerCarrying.Count; idxItem++)
                {
                    if (m_playerCarrying[idxItem] == resIdx) {
                        if (m_director.m_needs[charIdx].Count > 0 && m_director.m_needs[charIdx][0].idx == resIdx)
                        {
                            m_director.DropNeed(charIdx,idxItem);
                        }
                        m_inventory[charIdx][resIdx].amount++;
                        m_playerCarrying.RemoveAt(idxItem);
                        m_traderPopup.GetComponent<AudioSource>().clip = m_traderPopup.m_dropItem;
                        m_traderPopup.GetComponent<AudioSource>().Play();
                        return;
                    }
                }

            }
        }

        
    }
    

    void Update()
    {
        
    }
}

public class Resource
{
    public string name;
    public float amount;
    // copy constructor
    public Resource(Resource _previousResource)
    {
        name = _previousResource.name;
        amount = _previousResource.amount;
    }
    public Resource(string _name, float _amount)
    {
        name = _name;
        amount = _amount;
    }
}

