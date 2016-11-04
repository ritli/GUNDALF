using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    public int m_health;
    public int m_currenthealth;


	void Start () {
        ReceiveDamage(0);
	}
	
	void Update () {
	
	}

    public void ReceiveDamage(int amount)
    {
        m_currenthealth -= amount;
        Mathf.Clamp(m_currenthealth, 0, m_health);

        ManagerSingleton.GetCanvas().UpdateHealthbar(m_currenthealth, m_health);
    }
}
