using UnityEngine;
using System.Collections;
using UnityEditor;

[RequireComponent(typeof(Collider2D))]
public class ShopKeeper : MonoBehaviour {

    public Transform m_itemParent;
    public GameObject[] shopItems;
    GameObject[] m_spawnedItems;

    ShopKeeperArea m_area;

    public bool m_spawnShopItems = false;
    public bool m_clearList = false;

    void OnDrawGizmosSelected()
    {
        if (m_clearList)
        {
            m_spawnedItems = null;
            m_clearList = false;
        }

        if (m_spawnShopItems)
        {
            if (m_spawnedItems == null || m_spawnedItems.Length != shopItems.Length)
            {
                if (m_spawnedItems == null)
                {
                    m_spawnedItems = new GameObject[shopItems.Length];
                }

                if (m_spawnedItems.Length > shopItems.Length)
                {
                    for (int i = shopItems.Length; i < m_spawnedItems.Length; i++)
                    {
                        DestroyImmediate(m_spawnedItems[i]);
                    }
                }

                else
                {
                    GameObject[] g = m_spawnedItems;

                    m_spawnedItems = new GameObject[shopItems.Length];

                    for (int i = 0; i < shopItems.Length; i++)
                    {
                        m_spawnedItems.SetValue(g[i], i);
                    }
                }
            }

            for (int i = 0; i < shopItems.Length; i++)
            {
                GameObject g = null;
                bool spawnObject = true;

                if (m_spawnedItems[i] != null)
                {
                    spawnObject = false;
                }

                if (PrefabUtility.GetPrefabParent(m_spawnedItems[i]) == shopItems[i])
                {
                    spawnObject = false;
                }
                else
                {
                    if (m_spawnedItems[i] != null) { 
                        DestroyImmediate(m_spawnedItems[i]);
                        spawnObject = true;
                    }
                }

                if (spawnObject)
                {
                    g = (GameObject)PrefabUtility.InstantiatePrefab(shopItems[i]);

                    g.transform.position = transform.position + Vector3.up * i;
                    g.name = i.ToString();
                    g.transform.parent = m_itemParent;

                    m_spawnedItems[i] = g;
                }

            }

            m_spawnShopItems = false;
        }
    }

	void Start () {
        m_area = GetComponentInChildren<ShopKeeperArea>();

        InitSpawnedItems();
	}
	
    void InitSpawnedItems()
    {
        m_spawnedItems = new GameObject[shopItems.Length];

        for (int i = 0; i < shopItems.Length; i++)
        {
            if (shopItems[i] != null) { 
                m_spawnedItems[i] = m_itemParent.GetChild(i).gameObject;
            }
        }
    }

	void Update () {
        DisplayItems(m_area.GetPlayerInArea());	     
	}

    void DisplayItems(bool visible)
    {
        for (int i = 0; i < m_spawnedItems.Length; i++)
        {
            if (m_spawnedItems[i].GetComponent<Item>().GetEquipped())
            {
                RemoveFromItemList(i);
                break;
            }
            m_spawnedItems[i].SetActive(visible);
        }
    }

    void RemoveFromItemList(int index)
    {
        GameObject[] g = new GameObject[m_spawnedItems.Length-1];

        for (int i = 0; i < index; i++)
        {
            g[i] = m_spawnedItems[i];
        }
        for (int i = index; i < g.Length; i++)
        {
            g[i] = m_spawnedItems[i];
        }
    }
}
