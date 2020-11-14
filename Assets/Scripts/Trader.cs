using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader : MonoBehaviour
{
    public TraderPopup m_traderPopup;
    void Start()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Plane plane = collision.gameObject.GetComponent<Plane>();

        if (plane.m_isGrounded && collision.otherRigidbody.velocity.magnitude < 0.1f)
        {
            m_traderPopup.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        
    }
}
