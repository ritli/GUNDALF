using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour {

    public int m_health;
    public int m_currenthealth;
    public List<GameObject> m_itemList;

	void Start () {
        //Updates Healthbar with current health
        ReceiveDamage(0);
	}
	
    public void AddItem(GameObject i)
    {
        m_itemList.Add(i);
        i.transform.parent = transform;
        i.transform.position = transform.position;
    }

    void CreateItemObject(GameObject g)
    {

    }
    /// <summary>
    /// Updates player stats from external source.
    /// </summary>
    /// <param name="s">Stats struct.</param>
    /// <param name="multiplier">Added stats are multiplied by this.</param>
    public void UpdateStats(ItemStats s, int multiplier)
    {
        m_health += s.health;

        ReceiveDamage(0);
    }

    public void ReceiveDamage(int amount)
    {
        m_currenthealth -= amount;
        Mathf.Clamp(m_currenthealth, 0, m_health);

        Manager.GetCanvas().UpdateHealthbar(m_currenthealth, m_health);
    }
}
