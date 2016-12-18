using UnityEngine;
using System.Collections;

[System.Serializable]
public struct ItemStats
{
    public float speed;
    public int health;
    public int damage;
    public bool visibleOnEquip;

    public Vector2 offset;
}

public class Item : MonoBehaviour {
    
    public ItemStats m_stats;

    bool m_inShop;
    bool m_equipped;

    void Equip()
    {
        Manager.GetPlayer().GetComponent<PlayerStats>().UpdateStats(m_stats, 1);
    }

    void UnEquip()
    {
        Manager.GetPlayer().GetComponent<PlayerStats>().UpdateStats(m_stats, -1);
    }
    /// <summary>
    /// Disables collider and sprite renderer based on if item will be equipped or not
    /// </summary>
    /// <param name="equip"></param>
    public void PickupItem(bool equip)
    {
        GetComponent<Collider2D>().enabled = !equip;

        if (equip)
        {
            Equip();
            m_equipped = equip;
        }
    }

    public void SetInShop(bool inShop)
    {
        m_inShop = inShop;
    }

    public bool GetEquipped() {
        return m_equipped;
    }
}
