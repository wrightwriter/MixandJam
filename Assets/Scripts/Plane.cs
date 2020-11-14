﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public Sprite[] sprites;
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

            if (m_isGrounded)
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
            else if (transform.eulerAngles.z >= 245 && transform.eulerAngles.z <= 295 || transform.eulerAngles.z >= 65 && transform.eulerAngles.z <= 115)
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            else
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];

            transform.GetComponent<SpriteRenderer>().flipY = (transform.eulerAngles.z <= 270 && transform.eulerAngles.z >= 90);
        }

        m_rigidBody2D.gravityScale = Mathf.Lerp(0.0005f, 0.2f, m_rigidBody2D.velocity.magnitude);

        m_vel *= m_planeVelocity * m_input.y;
        m_vel *= Time.fixedDeltaTime;
        m_rigidBody2D.velocity += m_vel;


        if (m_rigidBody2D.velocity.magnitude >= m_maxPlaneVelocity)
        {
            m_rigidBody2D.velocity = m_rigidBody2D.velocity.normalized * m_maxPlaneVelocity;
        }

        m_isGrounded = false;
        m_isGrounded = Physics2D.OverlapCircle(m_groundCheck.transform.position, 0.2f, LayerMask.GetMask("baa"));
    }

    void Update()
    {
        GetComponent<AudioSource>().volume = m_rigidBody2D.velocity.magnitude;
        DoInput();
    }
    void DoInput()
    {
        m_input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}


