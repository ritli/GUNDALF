using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
public class SpriteRandomizer : MonoBehaviour {

bool m_fired = false;
    public Sprite[] m_sprites;

    void OnValidate()
    {
        if (m_sprites.Length > 0 && !m_fired)
        {
            GetComponent<SpriteRenderer>().sprite = m_sprites[Mathf.FloorToInt(Random.Range(0, m_sprites.Length))];
            m_fired = true;
        }
    }
}
#endif
