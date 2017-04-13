using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWeapon : MonoBehaviour {

    SpriteRenderer m_sprite;
    Animator m_animator;
    bool m_busy;
    Vector3 m_initialOffset;

	void Start () {
        m_animator = GetComponent<Animator>();
        m_sprite = GetComponent<SpriteRenderer>();
        m_initialOffset = transform.position - transform.parent.position;
	}

    public void PointAtTarget(Vector2 target)
    {
        Vector3 diff = (Vector3)target - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);


        m_sprite.sortingOrder = transform.parent.GetComponent<SpriteRenderer>().sortingOrder + 1;
    }

    public void StabAtTarget(float time, float distance)
    {
        if (!m_busy)
        {
            m_busy = true;
            StartCoroutine(Stab(time, distance));
        }

    }

    IEnumerator Stab(float time, float distance)
    {
        float timeElapsed = 0;
        
        float a = 0, b = distance;
        float lastDist = 0;

        for (int i = 0; i < 2; i++)
        {
            while (timeElapsed < time)
            {
                float dist = Mathf.Lerp(a, b, timeElapsed/time);

                transform.Translate(Vector3.up * (dist - lastDist), Space.Self);

                yield return new WaitForEndOfFrame();
                timeElapsed += Time.deltaTime;
                lastDist = dist;
            }

            a = b;
            b = 0;
            timeElapsed = 0;
            lastDist = 0;
        }

        transform.position = transform.parent.position + m_initialOffset;

        m_busy = false;
    }
}
