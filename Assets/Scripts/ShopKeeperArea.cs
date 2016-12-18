using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class ShopKeeperArea : MonoBehaviour {

    bool m_playerInArea;
    public bool m_moveCamera = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            m_playerInArea = true;
            Manager.GetCamera().TimedLookToggle(true, transform.position);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            m_playerInArea = false;
            Manager.GetCamera().TimedLookToggle(false, transform.position);
        }
    }

    public bool GetPlayerInArea()
    {
        return m_playerInArea;
    }
}
