using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trader : MonoBehaviour
{
    public GameObject m_traderPopup;
    public int idx = 0;
    void Start()
    {
    }


    private void OnTriggerStay2D(Collider2D collider)
    {

        Plane plane = collider.gameObject.GetComponent<Plane>();
        if (plane != null)
        {
            if (plane.m_isGrounded && collider.attachedRigidbody.velocity.magnitude < 0.2f && m_traderPopup.GetComponent<CanvasScaler>().scaleFactor != 1)
            {
                m_traderPopup.GetComponent<TraderPopup>().Show();
                m_traderPopup.GetComponent<TraderPopup>().m_currIdx = idx;
                GameObject.FindGameObjectWithTag("Notifications").GetComponent<NotificationHandler>().SendNotification(idx, "Welcome to My place");
            } else if (!plane.m_isGrounded && m_traderPopup.GetComponent<CanvasScaler>().scaleFactor != 0)
            {
                m_traderPopup.GetComponent<TraderPopup>().Hide();
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_traderPopup.GetComponent<TraderPopup>().Hide();
    }

    void Update()
    {
        
    }
}
