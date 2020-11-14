using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraderPopup : MonoBehaviour
{
    Trader m_trader;
    public Brain m_brain;
    public GameObject m_resourceBox;
    public int m_currIdx;
    
    void Start()
    {
        Hide();
    }
    void FixedUpdate()
    {
        int idxCharacter = m_currIdx;
        for (int idxResourceRow = 0; idxResourceRow < 8; idxResourceRow++)
        {
            Transform ResourceRow = m_resourceBox.transform.GetChild(idxResourceRow);

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

    public void Hide()
    {
        GetComponent<CanvasScaler>().scaleFactor = 0.0f;
    }
    public void Show()
    {
        GetComponent<CanvasScaler>().scaleFactor = 1.0f;
    }
    void Update()
    {
         
    }
}
