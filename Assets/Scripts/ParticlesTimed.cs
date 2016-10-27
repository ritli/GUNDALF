using UnityEngine;
using System.Collections;

public class ParticlesTimed : MonoBehaviour {

    ParticleSystem m_particles;

    public bool m_detached = false;
    public bool m_decayColor;

    public float m_lifetime;
    float m_timeElapsed;
    int m_fromAlpha = 0;

    Vector3 m_direction = Vector3.zero;

    void Start()
    {
        if (m_decayColor)
        {
            m_fromAlpha = 1;
        }

        if (m_detached)
        {
            Detach();
        }
    }

	void Update () {
        if (m_detached)
        {
            if (m_timeElapsed > m_lifetime)
            {
                Destroy(gameObject);
            }

            Color color = m_particles.startColor;
            m_particles.startColor = new Color(color.r, color.g, color.b, Mathf.Lerp(m_fromAlpha, 0, m_timeElapsed / m_lifetime));

            transform.Translate(m_direction * Time.deltaTime, Space.World);

            m_timeElapsed += Time.deltaTime;
        }
	}

    public void SetSpeed(Vector3 dir, float speed)
    {
        m_direction = dir * speed;
    }

    public void Detach()
    {
        m_particles = GetComponent<ParticleSystem>();
        transform.parent = null;
        m_detached = true;
    }
}
