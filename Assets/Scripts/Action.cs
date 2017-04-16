using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour {

    public Trigger m_trigger;

    void OnEnable()
    {
        m_trigger.onTriggerEvent += DoAction;
    }

    void OnDisable()
    {
        m_trigger.onTriggerEvent -= DoAction;
    }

    protected virtual void DoAction()
    {

    }
    
}
