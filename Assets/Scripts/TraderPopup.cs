using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraderPopup : MonoBehaviour
{
    Trader m_trader;
    public Brain m_brain;
    public AudioClip m_pickupItem;
    public AudioClip m_dropItem;
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

            GameObject ResourceImage = ResourceRow.transform.GetChild(0).gameObject;
            GameObject Text = ResourceRow.transform.GetChild(1).gameObject;
            Text.GetComponent<TMPro.TextMeshProUGUI>().text = ((int)m_brain.m_inventory[idxCharacter][idxResourceRow].amount).ToString();

            //ResourceImage.SetActive(m_brain.m_inventory[idxCharacter][idxResourceRow].amount > 1.0f);
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
