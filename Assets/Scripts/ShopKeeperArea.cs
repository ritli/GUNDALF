using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class ShopKeeperArea : MonoBehaviour {

    bool m_playerInArea;
	
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            m_playerInArea = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            m_playerInArea = false;
        }
    }

    public bool GetPlayerInArea()
    {
        return m_playerInArea;
    }
}
