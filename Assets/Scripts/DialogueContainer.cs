﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueContainer : MonoBehaviour {

    public TriggerType m_triggerType;

    public string m_Sender;
    [TextArea]
    public string m_Dialogue;
    public Sprite m_portrait;
    public Color color;
    [Space]


    [Header("Time Options")]
    
    public float m_duration;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_triggerType == TriggerType.onArea) { 

            if (collision.CompareTag("Player"))
                TriggerEvent();
        }
    }
    void TriggerEvent()
    {
        Manager.GetCanvas().PrintMessage(m_Dialogue, m_Sender, m_portrait, color);

        GetComponent<Collider2D>().enabled = false;
    }
}
