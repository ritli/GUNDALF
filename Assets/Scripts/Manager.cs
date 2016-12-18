﻿using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

    PlayerMove m_player;
    UIManager m_canvas;
    CameraController m_camera;

    static Manager m_instance;

	void Start () {
        if (FindObjectsOfType<Manager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            Init();

            m_instance = this;
        }
    }

    void Init()
    {
        m_player = FindObjectOfType<PlayerMove>();
        m_canvas = FindObjectOfType<UIManager>();
        m_camera = FindObjectOfType<CameraController>();
    }

    public static UIManager GetCanvas()
    {
        return m_instance.m_canvas;
    }

    public static PlayerMove GetPlayer()
    {
        return m_instance.m_player;
    }
    public static CameraController GetCamera()
    {
        return m_instance.m_camera;
    }
}
