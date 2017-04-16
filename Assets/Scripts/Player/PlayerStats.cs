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
        i.transform.position = transform.position + (Vector3)i.GetComponent<Item>().m_stats.offset;
        i.gameObject.GetComponent<SpriteRenderer>().enabled = i.GetComponent<Item>().m_stats.visibleOnEquip;
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

    public void ReceiveDamage(int amount, Vector3 location)
    {
        float knockScale = 1;

        ReceiveDamage(amount);
        StartCoroutine(ApplyKnockback(amount * knockScale, location));
    }

    IEnumerator ApplyKnockback(float knockback, Vector3 location)
    {
        float knockbackTime = 0.4f;
        float time = 0;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        Vector3 direction = (transform.position - location).normalized;

        while (time < knockbackTime)
        {
            sprite.color = Color.Lerp(Color.red, Color.white, time / knockbackTime);

            knockback = Mathf.Lerp(knockback, 0, time / knockbackTime);

            transform.Translate(direction * knockback * Time.deltaTime);

            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
        }
    }
}
