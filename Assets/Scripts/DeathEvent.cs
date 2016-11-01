using UnityEngine;
using System.Collections;

public enum DeathEventType
{
    SpawnObject
}

public class DeathEvent : MonoBehaviour {   

    public DeathEventType m_type;

    public GameObject m_ObjectToSpawn;

    bool m_eventFired = false;

    public void TriggerEvent()
    {
        if (!m_eventFired)
        {
            m_eventFired = true;

            switch (m_type)
            {
                case DeathEventType.SpawnObject:
                    Instantiate(m_ObjectToSpawn, transform.position, Quaternion.identity);
                    break;
                default:
                    break;
            } 
        }
    }
    
}
