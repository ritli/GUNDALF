using UnityEngine;
using System.Collections;

public class ManagerSingleton : MonoBehaviour {

    PlayerMove m_player;

    static ManagerSingleton m_instance;

	void Start () {
        if (FindObjectsOfType<ManagerSingleton>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            Init();

            m_instance = this;

            InvokeRepeating("BulletCleanup", 5f, 5f);
        }
    }

    void Init()
    {
        m_player = FindObjectOfType<PlayerMove>();
    }

    public static PlayerMove GetPlayer()
    {
        return m_instance.m_player;
    }
}
