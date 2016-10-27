using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    Rigidbody2D m_rigidbody;
    SpriteRenderer m_sprite;
    Animator m_anim;
    PlayerStats m_stats;

    int direction;
    public float m_moveSpeed = 10;

	void Start () {
        m_stats = GetComponent<PlayerStats>();
        m_sprite = GetComponent<SpriteRenderer>();
        m_anim = GetComponent<Animator>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

	void Update () {
        Vector2 movement = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Move(movement);
	}

    void SwitchState()
    {
        if (m_stats.m_currenthealth <= 0)
        {
            gameObject.SetActive(false);
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
    
    public int GetDirection()
    {
        return direction;
    }


}
