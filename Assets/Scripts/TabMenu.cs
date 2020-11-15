using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabMenu : MonoBehaviour
{
    public Brain m_brain;
    public List<GameObject> m_resourceBoxes;
    public List<GameObject> m_speechBubbles;
    public List<Sprite> m_resourceSprites;
    Canvas m_canvas;
    void Start()
    {
        m_canvas = GetComponent<Canvas>();
        InvokeRepeating("UpdateData",1.0f, 0.4f);
    }
    void FixedUpdate()
    {

    }

    void UpdateData()
    {
        for (int idxCharacter = 0; idxCharacter < 8; idxCharacter++)
        {
            GameObject ResourceBox = m_resourceBoxes[idxCharacter];
            for (int idxResourceRow = 0; idxResourceRow < 8; idxResourceRow++)
            {
                Transform ResourceRow = ResourceBox.transform.GetChild(idxResourceRow);

                GameObject ResourceFirstBox = ResourceRow.transform.GetChild(0).gameObject;
                GameObject ResourceSecondBox = ResourceRow.transform.GetChild(1).gameObject;
                GameObject ResourceThirdBox = ResourceRow.transform.GetChild(2).gameObject;
                GameObject ResourceFourthBox = ResourceRow.transform.GetChild(3).gameObject;

                ResourceFirstBox.SetActive(m_brain.m_inventory[idxCharacter][idxResourceRow].amount > 1.0f);
                ResourceSecondBox.SetActive(m_brain.m_inventory[idxCharacter][idxResourceRow].amount > 2.0f);
                ResourceThirdBox.SetActive(m_brain.m_inventory[idxCharacter][idxResourceRow].amount > 3.0f);
                ResourceFourthBox.SetActive(m_brain.m_inventory[idxCharacter][idxResourceRow].amount > 4.0f);

            }
        }
        for (int idxCharacter = 0; idxCharacter < 8; idxCharacter++)
        {
            GameObject SpeechBubble = m_speechBubbles[idxCharacter];
            //if ()
        }

    }

    void Update()
    {
        m_canvas.enabled = Input.GetKey("tab");
    }
}
