using UnityEngine;
using System.Collections;

public class AIStats : MonoBehaviour {

    public int m_health;
    public int m_currentHealth;
    public float m_speed;

    bool alive;

	void Start ()
    {
        m_currentHealth = m_health;
    }

    public void ReceiveDamage(int amount)
    {
        m_health -= amount;
    }
}
