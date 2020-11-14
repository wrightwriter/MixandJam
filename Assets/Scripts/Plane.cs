using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    Rigidbody2D m_rigidBody2D;
    public Transform m_front;
    public Transform m_groundCheck;
    public Transform m_up;

    public bool m_isGrounded = false;
    public float m_planeVelocity = 1f;
    public float m_maxPlaneVelocity = 2f;
    Vector2 m_vel;
    Vector2 m_dir;
    Vector2 m_dirUp;

    Vector2 m_input;

    void Start()
    {
        m_rigidBody2D = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        m_dir = (m_front.position - transform.position).normalized;
        m_dirUp = (m_up.position - transform.position).normalized;

        if (m_input.y != 0.0f)
        {
            m_vel = m_dir;
            m_vel += m_dirUp * m_input.x*1f;

            if (m_input.x != 0f)
            {
                transform.Rotate(new Vector3(0, 0, -m_input.x*100f*Time.fixedDeltaTime));
                if (Mathf.Round(transform.eulerAngles.z) == 180) { transform.Rotate(new Vector3(0, 0, -180)); }
            }

            m_rigidBody2D.gravityScale = Mathf.Lerp(1.0f,0.05f,m_rigidBody2D.velocity.magnitude);

            m_vel *= m_planeVelocity*m_input.y;
            m_vel *= Time.fixedDeltaTime;
            m_rigidBody2D.velocity += m_vel;

            if( m_rigidBody2D.velocity.magnitude >= m_maxPlaneVelocity)
            {
                m_rigidBody2D.velocity = m_rigidBody2D.velocity.normalized*m_maxPlaneVelocity;
            }

            //m_rigidBody2D.velocity = 
        }

        ;
        if (Physics2D.OverlapCircle(m_groundCheck.transform.position, 0.1f, LayerMask.NameToLayer("platforms")) != null)
            m_isGrounded = true;
    }

    void Update()
    {
        DoInput();
    }
    void DoInput()
    {
        m_input = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
    }
}
