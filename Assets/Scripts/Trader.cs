using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trader : MonoBehaviour
{
    public GameObject m_traderPopup;
    public int idx = 0;
    private bool visited = false;
    AudioSource m_audioSourceDie;
    void Start()
    {
    }

    /*
    public void Die()
    {
        m_audioSourceDie.PlayOneShot(m_audioSourceDie.clip);

        gca
    }
    */

    public void Die()
    {
        m_audioSourceDie.PlayOneShot(m_audioSourceDie.clip);
        float t = Time.time;
        // wait 5s
        //yield return new WaitUntil(() => Time.time > t + 2f);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        transform.position += new Vector3(2000f,2000f, 2000f);
        
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
                if (!visited) GameObject.FindGameObjectWithTag("Notifications").GetComponent<NotificationHandler>().SendNotification(idx, new string[]{
                    "Psst... wanna buy some catnip?",
                    "This is my prized lemonade stand! Wanna buy some?",
                    "Morning, how ya doin today?",
                    "Good morning, Compatriot!",
                    "Welcome to the meat shack.",
                    "Welcome to my tennis court, kid.",
                    "G’day sweetie! You want something to eat? It's on the house.",
                    "Do you want something? I don't have all day."
                }[idx]);
                visited = true;
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
