using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plane : MonoBehaviour
{
    public Sprite[] sprites;
    Rigidbody2D m_rigidBody2D;
    public Transform m_front;
    public Transform m_groundCheck;
    public Transform m_up;
    public GameObject minimap_plane;
    public Slider boost_slider;

    public bool m_isGrounded = false;
    public float m_planeVelocity = 100f;
    public float m_maxPlaneVelocity = 500f;
    public float m_plane_fuel = 100f;
    private float start_maxVelocity = -1;
    private float start_accel = -1;

    public float left_bound = -30f;
    public float right_bound = 30f;
    public float down_bound = -8f;
    public float up_bound = 30f;
    public float bound_wind_strength = 0.3f;

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
        if (start_maxVelocity == -1) start_maxVelocity = m_maxPlaneVelocity;
        if (start_accel == -1) start_accel = m_planeVelocity;
        m_dir = (m_front.position - transform.position).normalized;
        m_dirUp = (m_up.position - transform.position).normalized;

        m_vel = m_dir + Vector2.up * 0.2f * m_rigidBody2D.velocity.magnitude / m_maxPlaneVelocity;
        m_vel += m_dirUp * m_input.x * 1.2f;


        if (m_input.x != 0f)
        {
            transform.Rotate(new Vector3(0, 0, -m_input.x * (m_input.y != 0 ? 100f : 60f) * Time.fixedDeltaTime));

            if (transform.eulerAngles.z >= 245 && transform.eulerAngles.z <= 295 || transform.eulerAngles.z >= 65 && transform.eulerAngles.z <= 115)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
                minimap_plane.GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
                minimap_plane.GetComponent<SpriteRenderer>().sprite = sprites[0];
            }

            transform.GetComponent<SpriteRenderer>().flipY = (transform.eulerAngles.z <= 270 && transform.eulerAngles.z >= 90);
            minimap_plane.GetComponent<SpriteRenderer>().flipY = (transform.eulerAngles.z <= 270 && transform.eulerAngles.z >= 90);
        }

        if(Input.GetKey(KeyCode.Space) && m_plane_fuel > 0)
        {
            m_plane_fuel -= 25 * Time.deltaTime;
            boost_slider.value = m_plane_fuel;
            m_maxPlaneVelocity = start_maxVelocity * 3;
            m_planeVelocity = start_accel * 3;
            GetComponent<TrailRenderer>().startColor = Color.red;
        } else
        {
            m_maxPlaneVelocity = Mathf.Lerp(m_maxPlaneVelocity,start_maxVelocity,1f*Time.deltaTime);
            m_planeVelocity = Mathf.Lerp(m_planeVelocity, start_accel, 1f * Time.deltaTime);
            GetComponent<TrailRenderer>().startColor = Color.blue;
        }

        m_rigidBody2D.gravityScale = Mathf.Lerp(0.0005f, 0.2f, m_rigidBody2D.velocity.magnitude);

        m_vel *= m_planeVelocity * m_input.y;
        m_vel *= Time.fixedDeltaTime;



        if (transform.position.x < left_bound)
        {
            m_vel += Vector2.right * (transform.position.x - left_bound) * (transform.position.x - left_bound) * bound_wind_strength;
        }
        if (transform.position.x > right_bound)
        {
            m_vel += Vector2.left * (transform.position.x - right_bound) * (transform.position.x - right_bound) * bound_wind_strength;
        }
        if (transform.position.y < down_bound)
        {
            m_vel += Vector2.up * (transform.position.y - down_bound) * (transform.position.y - down_bound) * bound_wind_strength;
        }
        if (transform.position.y > up_bound)
        {
            m_vel += Vector2.down * (transform.position.y - up_bound) * (transform.position.y - up_bound) * bound_wind_strength;
        }


        m_rigidBody2D.velocity += m_vel;


        if (m_rigidBody2D.velocity.magnitude >= m_maxPlaneVelocity)
        {
            m_rigidBody2D.velocity = m_rigidBody2D.velocity.normalized * m_maxPlaneVelocity;
        }

        m_isGrounded = false;
        m_isGrounded = Physics2D.OverlapCircle(m_groundCheck.transform.position, 2f, LayerMask.GetMask("platforms"));

        GetComponent<AudioSource>().volume = Mathf.Lerp(GetComponent<AudioSource>().volume, Mathf.Abs(m_input.y), 2.0f*Time.fixedDeltaTime  ) ;

        if(m_plane_fuel < 100) m_plane_fuel += 10 * Time.deltaTime;
    }

    void Update()
    {
        DoInput();
        boost_slider.value = m_plane_fuel;
    }
    void DoInput()
    {
        m_input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}


