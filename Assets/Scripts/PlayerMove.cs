using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public GameObject m_explosionObject;

    Rigidbody2D m_rigidbody;
    SpriteRenderer m_sprite;
    Animator m_anim;
    PlayerStats m_stats;

    int direction;
    public float m_moveSpeed = 10;

    public bool m_controlsDisabled = false;
    bool m_alive = true;

	void Start () {
        m_stats = GetComponent<PlayerStats>();
        m_sprite = GetComponent<SpriteRenderer>();
        m_anim = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

	void Update () {
        Vector2 movement = Vector2.zero;

        if (!m_controlsDisabled)
        {   
            movement = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        Move(movement);

        SwitchState();
    }

    void SwitchState()
    {
        if (m_stats.m_currenthealth <= 0)
        {
            KillPlayer(true);
        }
    }

    void Move(Vector2 dir)
    {
        m_rigidbody.AddForce(dir * m_moveSpeed * Time.deltaTime, ForceMode2D.Impulse);

        int direction = 0;

        if (Mathf.Abs(dir.x) >= Mathf.Abs(dir.y) && dir.x != 0)
        {
            direction = 1;
            m_sprite.flipX = true;
            if (dir.x > 0)
            {
                m_sprite.flipX = false;
            }
        }
        else if (dir.y > 0)
        {
            direction = 4;
        }
        else if (dir.y < 0)
        {
            direction = 2;
        }

        this.direction = direction;
        m_anim.SetInteger("Direction", direction);
    }
    
    public void KillPlayer(bool isDead)
    {
        if (true)
        {
            GetComponent<DeathEvent>().TriggerEvent();
        }


        GetComponent<SpriteRenderer>().enabled = !isDead;
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = !isDead;


        GetComponent<Collider2D>().enabled = !isDead;
        m_controlsDisabled = isDead;
    }

    public bool GetAlive()
    {
        return m_alive;
    }

    public int GetDirection()
    {
        return direction;
    }
}
