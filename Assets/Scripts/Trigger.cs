using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType
{
    onArea, onPickup, onKill
}

public enum TriggererType
{
    enemy, player
}

public class Trigger : MonoBehaviour {

    public delegate void TriggerEvent();

    public TriggerType m_triggerType;
    public TriggererType m_triggererType;

    public TriggerEvent m_triggerEvent;

    bool m_canRun;

    void OnValidate()
    {
        switch (m_triggerType)
        {
            case TriggerType.onArea:
                if (!GetComponent<Collider2D>())
                {
                    Debug.LogError("No collider attached to trigger");
                    m_canRun = false;
                }

                break;
            case TriggerType.onPickup:
                if (!GetComponent<Item>())
                {
                    Debug.LogError("No item attached to trigger");
                    m_canRun = false;
                }
                break;
            case TriggerType.onKill:
                if (!GetComponent<PlayerStats>() || !GetComponent<AIStats>())
                {
                    Debug.LogError("No stats attached to trigger");
                    m_canRun = false;
                }
                break;
            default:
                break;
        }
    }

    void Update()
    {
        switch (m_triggerType)
        {
            case TriggerType.onArea:
                if (!GetComponent<Collider2D>())
                {
                    Debug.LogError("No collider attached to trigger");
                }

                break;
            case TriggerType.onPickup:
                if (!GetComponent<Item>())
                {
                    Debug.LogError("No item attached to trigger");
                }
                break;
            case TriggerType.onKill:
                if (!GetComponent<PlayerStats>() || !GetComponent<AIStats>())
                {
                    Debug.LogError("No stats attached to trigger");
                }
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (m_triggerType.Equals(TriggerType.onArea))
        {
            m_triggerEvent();
        }
    }

    void TriggerStart()
    {
        switch (m_triggerType)
        {
            case TriggerType.onArea:
                if (!GetComponent<Collider2D>())
                {
                    gameObject.AddComponent<BoxCollider2D>();
                }

                break;
            case TriggerType.onPickup:
                if (!GetComponent<Item>())
                {
                    gameObject.AddComponent<BoxCollider2D>();
                }

                break;
            case TriggerType.onKill:
                break;
            default:
                break;
        }
    }
}
