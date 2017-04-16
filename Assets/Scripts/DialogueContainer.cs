using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueContainer : MonoBehaviour {

    public TriggerType m_triggerType;

    public string m_Sender;
    [TextArea]
    public string m_Dialogue;
    public Sprite m_portrait;
    public Color color;

    public delegate void OnDialogueEnd();
    public delegate void OnDialogueStart();
    public OnDialogueEnd DialogueEnd;
    public OnDialogueStart DialogueStart;

    public float m_duration;

    void OnEnable()
    {
        if (m_triggerType.Equals(TriggerType.onPickup))
        {
            GetComponent<Item>().pickupEvent += TriggerEvent;
        }
    }

    void OnDisable()
    {
        if (m_triggerType.Equals(TriggerType.onPickup))
        {
            GetComponent<Item>().pickupEvent -= TriggerEvent;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_triggerType == TriggerType.onArea) { 

            if (collision.CompareTag("Player"))
                TriggerEvent();
        }
    }
    void TriggerEvent()
    {
        EventStarted();

        Manager.GetCanvas().PrintMessage(m_Dialogue, m_Sender, m_portrait, color, this);

        GetComponent<Collider2D>().enabled = false;
    }

    public void EventEnded()
    {
        if (DialogueEnd != null)
        {
            DialogueEnd();
        }
    }

    public void EventStarted()
    {
        if (DialogueStart != null)
        {
            DialogueStart();
        }
    }
}
