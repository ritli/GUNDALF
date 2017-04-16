using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_SetActive : Action {

    public bool m_active;
    public GameObject m_target;

    protected override void DoAction()
    {
        IPauseable target = m_target.GetComponent<IPauseable>();

        if (m_active)
        {
            target.Enable();
        }
        else
        {
            target.Disable();
        }
    }
}
