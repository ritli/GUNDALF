using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPauseable
{
    void Disable();
    void Enable(); 
}

public class AIShitrog : MonoBehaviour, IPauseable {

    public GameObject m_projectile;
    public float m_aoeRange;
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



    public bool m_enabled = true;

    public void Enable()
    {
        m_enabled = true;
    }

    public void Disable()
    {
        m_enabled = false;
    }

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
        m_anim = GetComponent<Animator>();
        m_stats = GetComponent<AIStats>();
        m_player = Manager.GetPlayer().transform;

    }

    void Update()
    {
        if (m_enabled)
        {
            UpdatePlayerDistance();
            SwitchState();
            CheckState();
            UpdateAnimator();
        }
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
                MoveToPlayer();
                break;
            case State.ATTACKING:
                if (!m_onCooldown)
                {
                    StartCoroutine(Shoot());
                }
                break;
            case State.IDLE:
                break;
            case State.DYING:
                GetComponent<DeathEvent>().TriggerEvent();
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    void Attack()
    {
        m_onCooldown = true;

        //Manager.GetPlayerStats().ReceiveDamage(m_damage, transform.position);

        Invoke("ResetCooldown", m_cooldown);
    }

    void ResetCooldown()
    {
        m_onCooldown = false;
    }

    IEnumerator Shoot()
    {
        m_onCooldown = true;

        Invoke("ResetCooldown", m_cooldown);

        Vector3 shootposition = m_player.transform.position;
        float shootTime = 0.6f;
        float shootTimeElapsed = 0;

        float yOffset = 2;

        GameObject projectile = Instantiate(m_projectile, transform.position, Quaternion.identity);

        while (shootTimeElapsed < shootTime)
        {

            yOffset = Mathf.Sin(shootTimeElapsed / shootTime) * yOffset;

            projectile.transform.position = Vector3.Slerp(transform.position, shootposition, shootTimeElapsed / shootTime);


            yield return new WaitForEndOfFrame();
            shootTimeElapsed += Time.deltaTime;
        }

        if (Vector2.Distance(shootposition, m_player.transform.position) < m_aoeRange)
        {
            Manager.GetPlayerStats().ReceiveDamage(m_damage);

        }

        projectile.GetComponent<DeathEvent>().TriggerEvent();
        Destroy(projectile);
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

    void MoveToPlayer()
    {

        transform.Translate(m_distToPlayer.normalized * m_stats.m_speed * Time.deltaTime);
    }

    void UpdateAnimator()
    {
        m_anim.SetInteger("State", (int)m_state);
    }
}
