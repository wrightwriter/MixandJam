using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            m_brain.SendMessage("CarryItem", new Vector2(m_traderPopup.m_currIdx,m_resourceIdx)); 
        } else if (eventData.button == PointerEventData.InputButton.Right)
        {
            m_brain.SendMessage("DropItem", new Vector2(m_traderPopup.m_currIdx,m_resourceIdx)); 

        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
