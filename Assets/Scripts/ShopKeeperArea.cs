using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class ShopKeeperArea : MonoBehaviour {

    bool m_playerInArea;

	void Start () {
	
	}
	
    void OnTriggerEnter2D(Collider2D col)
    {
  
    }

    public bool GetPlayerInArea()
    {
        return m_playerInArea;
    }
}
