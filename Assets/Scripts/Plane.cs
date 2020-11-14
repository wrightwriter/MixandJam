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
    public float m_planeVelocity = 100f;
    public float m_maxPlaneVelocity = 500f;
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

        m_vel = m_dir + Vector2.up * 0.2f * m_rigidBody2D.velocity.magnitude / m_maxPlaneVelocity;
        m_vel += m_dirUp * m_input.x * 1.2f;

        if (m_input.x != 0f)
        {
            transform.Rotate(new Vector3(0, 0, -m_input.x * (m_input.y != 0 ? 100f : 60f) * Time.fixedDeltaTime));
            transform.GetComponent<SpriteRenderer>().flipY = (transform.eulerAngles.z <= 270 && transform.eulerAngles.z >= 90);
        }

        m_rigidBody2D.gravityScale = Mathf.Lerp(0.0005f, 0.2f, m_rigidBody2D.velocity.magnitude);

        m_vel *= m_planeVelocity * m_input.y;
        m_vel *= Time.fixedDeltaTime;
        m_rigidBody2D.velocity += m_vel;


        //rotates volocity to match sprite
        Vector3 sprite_forwards = new Vector3(Mathf.Cos(transform.eulerAngles.z), Mathf.Sin(transform.eulerAngles.z), 0);
        float singleStep = .4f * Time.fixedDeltaTime;
        m_rigidBody2D.velocity = Vector3.RotateTowards(m_rigidBody2D.velocity, sprite_forwards, singleStep, 0.0f);



        if (m_rigidBody2D.velocity.magnitude >= m_maxPlaneVelocity)
        {
            m_rigidBody2D.velocity = m_rigidBody2D.velocity.normalized * m_maxPlaneVelocity;
        }

        if (Physics2D.OverlapCircle(m_groundCheck.transform.position, 0.1f, LayerMask.NameToLayer("platforms")) != null)
            m_isGrounded = true;
    }

    void Update()
    {
        DoInput();
    }
    void DoInput()
    {
        m_input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}


