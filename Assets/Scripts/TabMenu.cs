using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabMenu : MonoBehaviour
{
    public Brain m_brain;
    public Director m_director;
    public List<GameObject> m_resourceBoxes;
    public List<GameObject> m_speechBubbles;
    public List<Sprite> m_resourceSprites;
    public AudioClip m_clipOpen;
    public AudioClip m_clipClose;
    Canvas m_canvas;
    AudioSource m_audioSource;
    void Start()
    {
        m_canvas = GetComponent<Canvas>();
        m_audioSource = GetComponent<AudioSource>();
        InvokeRepeating("UpdateData",1.0f, 0.5f);
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown("tab"))
        {
            m_audioSource.clip = m_clipOpen;
            m_audioSource.Play();
        } else if (Input.GetKeyUp("tab"))
        {
            m_audioSource.clip = m_clipClose;
            m_audioSource.Play();
        }

    }

    void UpdateData()
    {
        for (int idxCharacter = 0; idxCharacter < 8; idxCharacter++)
        {
            GameObject ResourceBox = m_resourceBoxes[idxCharacter];
            for (int idxColumn = 0; idxColumn < 2; idxColumn++)
            {
                GameObject Column = ResourceBox.transform.GetChild(idxColumn).gameObject;
                int idxResource = 0;
                for (int idxResourceRow = 0; idxResourceRow < 4; idxResourceRow++)
                {

                    GameObject ResourceRow = Column.transform.GetChild(idxResourceRow).gameObject;

                    GameObject Text = ResourceRow.transform.GetChild(1).gameObject;
                    Text.GetComponent<TMPro.TextMeshProUGUI>().text = ((int)m_brain.m_inventory[idxCharacter][idxResource].amount).ToString();
                    idxResource++;
                }

            }
        }
        for (int idxCharacter = 0; idxCharacter < 8; idxCharacter++)
        {
            GameObject SpeechBubble = m_speechBubbles[idxCharacter];

            bool needsResource = m_director.m_needs[idxCharacter].Count > 0;

            if (needsResource && m_brain.m_charAlive[idxCharacter])
            {
                SpeechBubble.GetComponent<RectTransform>().localScale = new Vector3(1f,1f,1f);

                Image image = SpeechBubble.transform.GetChild(0).GetComponent<Image>();
                TMPro.TextMeshProUGUI text = SpeechBubble.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();

                Need need = m_director.m_needs[idxCharacter][0];
                image.sprite = m_resourceSprites[need.idx];
                text.text = need.time.ToString() + "s";
                
            } else
            {
                SpeechBubble.GetComponent<RectTransform>().localScale = new Vector3(0f,0f,0f);
            }
        }

    }

    void Update()
    {
        m_canvas.enabled = Input.GetKey("tab");
    }
}
