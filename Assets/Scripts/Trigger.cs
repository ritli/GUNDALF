using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType
{
    onArea, onPickup, onKill, onDialogueEnd, onDialogueStart
}

public enum TriggererType
{
    enemy, player
}

public class Trigger : MonoBehaviour {

    public delegate void OnTriggerEvent();
    public OnTriggerEvent onTriggerEvent;


    public TriggerType m_triggerType;
    public TriggererType m_triggererType;

    public DialogueContainer m_dialogue;
    public float m_timeToTrigger = 0;

    bool m_canRun;

    void OnEnable()
    {
        TriggerStart();
    }

    void OnDisable()
    {
        TriggerEnd();
    }

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

    void TriggerEvent()
    {
        Invoke("onTrigger", m_timeToTrigger);
    }

    void onTrigger()
    {
        if (onTriggerEvent != null)
        {
            onTriggerEvent();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (m_triggerType.Equals(TriggerType.onArea))
        {
            TriggerEvent();
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

            case TriggerType.onDialogueStart:
                m_dialogue.DialogueStart += TriggerEvent;
                break;
            case TriggerType.onDialogueEnd:
                m_dialogue.DialogueEnd += TriggerEvent;
                break;
            default:
                break;
        }
    }

    void TriggerEnd()
    {
        switch (m_triggerType)
        {
            case TriggerType.onArea:


                break;
            case TriggerType.onPickup:


                break;
            case TriggerType.onKill:
                break;

            case TriggerType.onDialogueStart:
                m_dialogue.DialogueStart -= TriggerEvent;
                break;
            case TriggerType.onDialogueEnd:
                m_dialogue.DialogueEnd -= TriggerEvent;
                break;
            default:
                break;
        }
    }
}
