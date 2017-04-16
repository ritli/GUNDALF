using UnityEngine;
using System.Collections;

public enum ItemType
{
    gun, armor, trinket
}

[System.Serializable]
public class ItemStats
{
    public string name;
    public float speed;
    public int health;
    public int damage;
    public bool visibleOnEquip;

    public Vector2 offset;
}

[System.Serializable]
public class GunStats
{
    public int shotCount;
    public GameObject bullet;
    public bool flashlight;
}
    
public class Item : MonoBehaviour {

    public delegate void onPickup();
    public onPickup pickupEvent;

    public ItemType m_itemType;
    public Sprite m_sprite;
    public bool m_EquipOnPickup;
    public ItemStats m_stats;
    public GunStats m_gunStats;

    bool m_inShop;
    bool m_equipped;

    void UpdateStats(int mult)
    {
        Manager.GetPlayer().GetComponent<PlayerStats>().UpdateStats(m_stats, mult);
    }
    
    /// <summary>
    /// Disables collider and sprite renderer based on if item will be displayed on player or not
    /// </summary>
    /// <param name="equip"></param>
    public void PickupItem(bool equip)
    {
        GetComponent<Collider2D>().enabled = !equip;

        if (pickupEvent != null)
        {
            pickupEvent();
        }

        switch (m_itemType)
        {
            case ItemType.gun:
                Manager.GetPlayer().GetComponentInChildren<PlayerGun>().EquipGun(this);
                Manager.GetCanvas().PlayGetItem(this);

                UpdateStats(1);

                gameObject.SetActive(false);
                break;
            case ItemType.armor:
                
                if (equip)
                {
                    UpdateStats(1);
                    m_equipped = equip;
                }
                break;
            case ItemType.trinket:
                GetComponent<Collider2D>().enabled = !equip;
                if (equip)
                {
                    UpdateStats(1);
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
