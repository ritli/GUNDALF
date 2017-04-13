using UnityEngine;
using System.Collections;

public enum State
{
    MOVING, IDLE, DYING, ATTACKING
}

public class AIBlob : MonoBehaviour {

    public State m_state;
    public float m_attackRange;
    AIStats m_stats;
    Vector3 m_distToPlayer;
    bool m_attacking = false;
    public int m_damage;


    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, m_attackRange);
    }

	void Start () {
        m_stats = GetComponent<AIStats>();
	}

    void Update()
    {
        SwitchState();
        CheckState();
    }

    void SwitchState()
    {
        if (m_stats.m_health <= 0)
        {
            m_state = State.DYING;
        }
        else if (!m_state.Equals(State.ATTACKING))
        {
            m_state = State.MOVING;
        }
        else
        {

        }

    }

    void CheckState()
    {
        switch (m_state)
        {
            case State.MOVING:
                MoveToPlayer();
                break;
            case State.ATTACKING:
                if (!m_attacking)
                {
                    StartCoroutine(AttackSequence());
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

    void MoveToPlayer()
    {
        m_distToPlayer = (Manager.GetPlayer().transform.position - transform.position);

        transform.Translate(m_distToPlayer.normalized * m_stats.m_speed * Time.deltaTime);

        if (m_distToPlayer.magnitude < m_attackRange)
        {
            m_state = State.ATTACKING;
        }
    }

    IEnumerator AttackSequence()
    {
        m_attacking = true;

        float windupTime = 0.8f;

        Animator ani = transform.GetChild(0).GetComponent<Animator>();

        ani.SetInteger("State", 1);

        yield return new WaitForSeconds(windupTime);

        ani.SetInteger("State", 0);

        m_distToPlayer = (Manager.GetPlayer().transform.position - transform.position);

        if (m_distToPlayer.magnitude < m_attackRange)
        {
            Manager.GetPlayer().GetComponent<PlayerStats>().ReceiveDamage(m_damage);
        }

        m_state = State.IDLE;
        m_attacking = false;
    }
}
