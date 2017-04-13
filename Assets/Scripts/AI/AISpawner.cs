using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour {

    public GameObject m_objectToSpawn;
    public float m_time;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(transform.position, Vector3.one);
    }

    void Start () {
        InvokeRepeating("Spawn", m_time, m_time);
    }
	
    void Spawn()
    {
        Instantiate(m_objectToSpawn, transform.position, Quaternion.identity);
    }

}
