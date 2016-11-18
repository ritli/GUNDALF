using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    Vector3 m_position;
    Vector3 m_lastPosition;
    float m_shakeAmount = 0;
    float m_shakeTime;

    bool m_interpolateCamera = true;
	
	void FixedUpdate () {

        GetPlayerPosition(ref m_position);
        GetShakePosition(ref m_position);

        MoveToPosition(m_position);
    }

    void GetPlayerPosition(ref Vector3 position)
    {
        position = Manager.GetPlayer().transform.position;
        position.z = transform.position.z;
    }

    void GetShakePosition(ref Vector3 position)
    {
        position += (Vector3)Random.insideUnitCircle * m_shakeAmount;
    }

    void MoveToPosition(Vector3 position)
    {
        if (m_interpolateCamera)
        {
            transform.position = Vector3.Lerp(m_lastPosition, position, 0.05f);
        }
        else
        {
            transform.position = position;
        }

        m_lastPosition = position;
    }

    void StopScreenShake()
    {
        m_shakeAmount = 0;
    }

    public void SetScreenShake(float time, float amount)
    {
        if (amount != 0)
        {
            Invoke("StopScreenShake", time);
        }

        m_shakeAmount = amount;
    }
}
