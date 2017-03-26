using UnityEngine;
using System.Collections;

public enum ItemType
{
    gun, armor, trinket
}

[System.Serializable]
public struct ItemStats
{
    public ItemType itemType;
    public string name;
    public float speed;
    public int health;
    public int damage;
    public bool visibleOnEquip;

    public Vector2 offset;
}
    
public class Item : MonoBehaviour {

    public Sprite m_sprite;
    public bool m_EquipOnPickup;
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
    /// Disables collider and sprite renderer based on if item will be displayed on player or not
    /// </summary>
    /// <param name="equip"></param>
    public void PickupItem(bool equip)
    {
        GetComponent<Collider2D>().enabled = !equip;

        switch (m_stats.itemType)
        {
            case ItemType.gun:
                Manager.GetPlayer().GetComponentInChildren<PlayerGun>().EquipGun(this);
                Manager.GetCanvas().PlayGetItem(this);
                Equip();
                print("Delete");
                gameObject.SetActive(false);
                break;
            case ItemType.armor:
                
                if (equip)
                {
                    Equip();
                    m_equipped = equip;
                }
                break;
            case ItemType.trinket:
                GetComponent<Collider2D>().enabled = !equip;
                if (equip)
                {
                    Equip();
                    m_equipped = equip;
                }
                break;
            default:
                break;
        }
    }

    public void SetInShop(bool inShop)
    {
        m_inShop = inShop;
    }

    public bool GetEquipped() {
        return m_equipped;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (m_EquipOnPickup) { 
            if (col.CompareTag("Player"))
            {
                PickupItem(true);
            }
        }
    }

}
