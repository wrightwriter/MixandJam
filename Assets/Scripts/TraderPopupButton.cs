using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TraderPopupButton : MonoBehaviour
{
    public int m_resourceIdx;
    public Brain m_brain;
    private Button m_button;
    public TraderPopup m_traderPopup;
    void Start()
    {
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(() => {
            m_brain.SendMessage("CarryItem", new Vector2(m_traderPopup.m_currIdx,m_resourceIdx)); 
        });
    }

    // Update is called once per frame
    void Update()
    {
    }
}
