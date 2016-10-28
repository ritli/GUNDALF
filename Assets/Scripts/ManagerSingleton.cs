using UnityEngine;
using System.Collections;

public class ManagerSingleton : MonoBehaviour {

    PlayerMove m_player;
    UIManager m_canvas;

    static ManagerSingleton m_instance;

	void Start () {
        if (FindObjectsOfType<ManagerSingleton>().Length > 1)
        {
            Debug.Log("AWAT");

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
    }

    public static UIManager GetCanvas()
    {
        return m_instance.m_canvas;
    }

    public static PlayerMove GetPlayer()
    {
        return m_instance.m_player;
    }
}
