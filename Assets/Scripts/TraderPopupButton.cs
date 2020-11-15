using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TraderPopupMessage
{
    public int traderIdx;
    public int resourceIdx;
    public bool success = false;
    public string pickupOrDrop = "pickup";
}

public class TraderPopupButton : MonoBehaviour, IPointerClickHandler
{
    public int m_resourceIdx;
    public Brain m_brain;
    private Button m_button;
    public TraderPopup m_traderPopup;
    void Start()
    {
        m_button = GetComponent<Button>();
        /*
        m_button.onClick.AddListener(() => {
            m_brain.SendMessage("CarryItem", new Vector2(m_traderPopup.m_currIdx,m_resourceIdx)); 
        });
        */
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TraderPopupMessage message = new TraderPopupMessage();
        message.resourceIdx = m_resourceIdx;
        message.traderIdx = m_traderPopup.m_currIdx;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            message.pickupOrDrop = "pickup";
        } else if (eventData.button == PointerEventData.InputButton.Right)
        {
            message.pickupOrDrop = "drop";
        } else
        {
            return;
        }

        m_brain.SendMessage("PickupOrDropItemFromTrader", message); 
    }

    // Update is called once per frame
    void Update()
    {
    }
}
