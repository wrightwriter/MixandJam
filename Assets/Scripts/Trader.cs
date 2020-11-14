using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            if (plane.m_isGrounded && collider.attachedRigidbody.velocity.magnitude < 0.1f)
            {
                m_traderPopup.SetActive(true);
                m_traderPopup.GetComponent<TraderPopup>();
            }
        }

    }

    void Update()
    {
        
    }
}
