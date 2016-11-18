using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour {

    public int m_health;
    public int m_currenthealth;
    public List<Item> m_itemList;

	void Start () {
        //Updates Healthbar with current health
        ReceiveDamage(0);
	}
	
    public void AddItem(Item i)
    {
        m_itemList.Add(i);
    }

    public void ReceiveDamage(int amount)
    {
        m_currenthealth -= amount;
        Mathf.Clamp(m_currenthealth, 0, m_health);

        ManagerSingleton.GetCanvas().UpdateHealthbar(m_currenthealth, m_health);
    }
}
