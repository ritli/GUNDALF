using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public GameObject m_explosionObject;
    public LayerMask m_CollisionLayers;

    public bool m_hostile;
    public float m_speed;
    public int m_damage;
    public float m_deathTime;

    Collider2D m_col;

    

    public void Init(float speed, float damage)
    {
        speed = m_speed;
        damage = m_damage;
    }

    void Start()
    {
        m_col = GetComponent<Collider2D>();

        Invoke("DestroyThis", m_deathTime);
    }

	void Update () {
        transform.Translate(transform.up * m_speed * Time.deltaTime, Space.World);
	}   

    void OnTriggerEnter2D(Collider2D c)
    {
        if (m_col.IsTouchingLayers(m_CollisionLayers)) //Walls and shit
        {
            DestroyThis();
        }
        
        else if (c.CompareTag("Enemy")) //Enemy
        {
            c.GetComponent<AIStats>().ReceiveDamage(m_damage);
            DestroyThis();
        }
        else if (c.CompareTag(("Player"))) //Player
        {
            //Do nothing
        }
        else if (c.CompareTag(("Item")) && !m_hostile) //Item
        {
            Manager.GetPlayer().GetComponent<PlayerStats>().AddItem(c.gameObject);
            c.GetComponent<Item>().DestroyItem(true);
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
