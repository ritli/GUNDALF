using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public GameObject m_explosionObject;

    public float m_speed;
    public int m_damage;
    public float m_deathTime;

    public void Init(float speed, float damage)
    {
        speed = m_speed;
        damage = m_damage;
    }

    void Start()
    {
        Invoke("DestroyThis", m_deathTime);
    }

	void Update () {
        transform.Translate(transform.up * m_speed * Time.deltaTime, Space.World);
	}   

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.GetComponent<AIStats>())
        {
            c.GetComponent<AIStats>().ReceiveDamage(m_damage);
            DestroyThis();
        }
        else if (c.GetComponent<PlayerMove>() || c.GetComponent<Projectile>()) 
        {
            //Do nothing
        }
        else
        {
            DestroyThis();
        }
    }

    void DestroyThis()
    {
        if (m_explosionObject)
            Instantiate(m_explosionObject, transform.position, Quaternion.identity);
        GetComponentInChildren<ParticlesTimed>().SetSpeed(transform.up, m_speed);
        GetComponentInChildren<ParticlesTimed>().DetachFromParent();
        Destroy(gameObject);
    }

    public void SetLayer(int id)
    {
        GetComponent<SpriteRenderer>().sortingOrder = id;
        if (transform.GetChild(0))
        { 
            transform.GetChild(0).GetComponent<ParticleSystemRenderer>().sortingOrder = id;
        }

    }
    
}   
