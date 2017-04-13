using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGobler : MonoBehaviour {

    public State m_state;
    public float m_attackRange;
    public float m_aggroRange;
    public float m_cooldown;
    public float m_knockBack;
    public float m_randomStabThreshold;
    float m_randomOffset;
    AIStats m_stats;
    Vector3 m_distToPlayer;
    bool m_attacking = false;
    bool m_onCooldown = false;
    public int m_damage;
    Transform m_player;
    Animator m_anim;
    AIWeapon m_weapon;

    Vector3 m_randomVector;
    float m_randomInterval;
    float m_randomIntervalTime;
    bool m_randomMoving;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_attackRange);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, m_aggroRange);
    }

    void Start()
    {
        m_randomOffset = Random.value;
        m_weapon = GetComponentInChildren<AIWeapon>();
        m_anim = GetComponent<Animator>();
        m_stats = GetComponent<AIStats>();
        m_player = Manager.GetPlayer().transform;

        m_randomInterval = Random.value;
    }

    void Update()
    {
        UpdatePlayerDistance();
        SwitchState();
        CheckState();
        UpdateAnimator();
    }

    void SwitchState()
    {
        if (m_stats.m_health <= 0)
        {
            m_state = State.DYING;
        }
        else if (m_distToPlayer.magnitude < m_aggroRange && m_distToPlayer.magnitude > m_attackRange)
        {
            m_state = State.MOVING;
        }
        else if (m_distToPlayer.magnitude < m_attackRange)
        {
            m_state = State.ATTACKING;
        }
        else
        {
            m_state = State.IDLE;
        }
    }
    
    void UpdatePlayerDistance()
    {
        m_distToPlayer = m_player.position - transform.position;

    }

    void CheckState()
    {
        switch (m_state)
        {
            case State.MOVING:
                m_weapon.PointAtTarget(m_player.position);
                m_weapon.StabAtTarget(0.2f + m_randomStabThreshold * m_randomOffset, 0.3f + m_randomStabThreshold * m_randomOffset);
                MoveToPlayer();
                break;
            case State.ATTACKING:
                if (!m_onCooldown)
                {
                    Attack();
                }
                break;
            case State.IDLE:
                break;
            case State.DYING:
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    void Attack()
    {
        m_onCooldown = true;

        Manager.GetPlayerStats().ReceiveDamage(m_damage);

        StartCoroutine(ApplyKnockback(m_knockBack));

        Invoke("ResetCooldown", m_cooldown);
    }

    void ResetCooldown()
    {
        m_onCooldown = false;
    }

    IEnumerator ApplyKnockback(float knockback)
    {
        float knockbackTime = 0.4f;
        float time = 0;

        Vector3 direction = -m_distToPlayer.normalized;

        while (time < knockbackTime)
        {
            knockback = Mathf.Lerp(knockback, 0, time / knockbackTime);

            transform.Translate(direction * knockback * Time.deltaTime);

            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
        }
    }

    void PeriodicRandomMovement()
    {
        if (m_randomIntervalTime > m_randomInterval)
        {

            if (m_randomMoving)
            {
                m_randomVector = Vector3.Slerp(m_distToPlayer.normalized, Random.insideUnitCircle, 0.6f).normalized * 0.01f;
            }
            else
            {
                m_randomVector = Vector3.zero;
            }

            m_randomInterval = Random.value;
            m_randomMoving = !m_randomMoving;
            m_randomIntervalTime = 0;
        }

        m_randomIntervalTime += Time.deltaTime;

    }

    void MoveToPlayer()
    {
        PeriodicRandomMovement();

        transform.Translate(m_distToPlayer.normalized * m_stats.m_speed * Time.deltaTime + m_randomVector);
    }

    void UpdateAnimator()
    {
        m_anim.SetInteger("State", (int)m_state);
    }
}
