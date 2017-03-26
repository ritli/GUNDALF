using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType
{
    onArea, onPickup, onKill
}

public class Trigger : MonoBehaviour {

    public TriggerType m_triggerType;

    void OnValidate()
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
                break;
            default:
                break;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TriggerEvent()
    {
        switch (m_triggerType)
        {
            case TriggerType.onArea:
                break;
            case TriggerType.onPickup:
                break;
            case TriggerType.onKill:
                break;
            default:
                break;
        }
    }
}
