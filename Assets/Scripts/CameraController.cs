using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    Vector3 m_position;
    Vector3 m_lastPosition;
    float m_shakeAmount = 0;
    float m_shakeTime;

    public float m_interpolateVal = 0.5f;

    bool m_TimedLookActive = false;
    Vector3 m_timedLookPos;

    bool m_interpolateCamera = true;
	
	void FixedUpdate () {
        if (!m_TimedLookActive)  {
            GetPlayerPosition(ref m_position);
            GetAddedMousePos(ref m_position);
        }

        else { 
            GetLookPosition(ref m_position);
        }

        MoveToPosition(m_position);
    }

    void GetAddedMousePos(ref Vector3 position)
    {
        float extendPercent = (new Vector3(Screen.width * 0.5f, Screen.height * 0.5f) - Input.mousePosition).magnitude / (new Vector3(Screen.width * 0.5f, Screen.height * 0.5f) - new Vector3(Screen.width, Screen.height)).magnitude;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition)- Manager.GetPlayer().transform.position;
        float zpos = transform.position.z;

        position += mousePos.normalized * extendPercent * 5f;
        position.z = zpos;
    }

    void GetPlayerPosition(ref Vector3 position)
    {
        position = Manager.GetPlayer().transform.position;
        position.z = transform.position.z;
    }

    void GetLookPosition(ref Vector3 position)
    {
        position = m_timedLookPos;
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
            Vector3 pos = Vector3.Lerp(m_lastPosition, position, m_interpolateVal);
            GetShakePosition(ref pos);
            transform.position = pos;
        }
        else
        {
            transform.position = position;
        }

        m_lastPosition = transform.position;
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

    public void TimedLook(float time, Vector2 position)
    {
        m_timedLookPos = position;
        StartCoroutine(TimedLookRoutine(time));
    }

    IEnumerator TimedLookRoutine(float time)
    {
        m_TimedLookActive = true;

        yield return new WaitForSeconds(time);

        m_TimedLookActive = false;
    }

    public void TimedLookToggle(bool enabled, Vector2 position)
    {
        m_timedLookPos = position;
        m_TimedLookActive = enabled;
    }
}
