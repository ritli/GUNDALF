using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class ShopKeeper : MonoBehaviour {

    public Transform m_itemParent;
    public GameObject[] shopItems;
    int[] m_spawnedItemIDs;

    public bool m_spawnShopItems = false;

    void OnDrawGizmosSelected()
    {
        if (m_spawnShopItems)
        {
            if (m_spawnedItemIDs == null)
            {
                m_spawnedItemIDs = new int[shopItems.Length];
            }

            for (int i = 0; i < shopItems.Length; i++)
            {
                GameObject g = null;
                bool spawnObject = true;


                if (m_itemParent.childCount != shopItems.Length) {
                    spawnObject = false;
                }

                else if (m_itemParent.GetChild(i).GetInstanceID() == shopItems[i].GetInstanceID())
                {
                    spawnObject = false;

                    
                }
                else
                {
                    DestroyImmediate(m_itemParent.GetChild(i).gameObject);
                }

                if (spawnObject)
                {
                    g = (GameObject)Instantiate(shopItems[i], transform.position + Vector3.up * i, Quaternion.identity);

                    g.name = i.ToString();
                    g.transform.parent = m_itemParent;
                }

            }

            m_spawnShopItems = false;
        }
    }

	void Start () {
	    
	}
	
	void Update () {
	
	}
}
