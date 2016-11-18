using UnityEngine;
using System.Collections;

[System.Serializable]
public struct ItemStats
{
    public float speed;
    public int health;
    public int damage;
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

    public void DestroyItem(bool equip)
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = equip;
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
