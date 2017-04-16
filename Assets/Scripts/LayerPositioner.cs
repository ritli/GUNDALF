using UnityEngine;
using System.Collections;

[RequireComponent( typeof(SpriteRenderer))]
public class LayerPositioner : MonoBehaviour {

    public bool m_RunInEditor = false;
    public bool m_BaseOnParent = false;
    [Range(-10, 10)]
    public int m_parentOffset;
    SpriteRenderer m_renderer;
    SpriteRenderer m_parent;

    void OnValidate()
    {
        if (m_RunInEditor)
        {
            m_renderer = GetComponent<SpriteRenderer>();
            PlaceInLayer();
        }
    }

	void Start () {
	    if (m_RunInEditor)
        {
            Destroy(this);
        }
        else
        {
            if (m_BaseOnParent)
            {
                m_parent = transform.parent.GetComponent<SpriteRenderer>();

            }

            m_renderer = GetComponent<SpriteRenderer>();
        }
	}
	
	void Update () {
        PlaceInLayer();
	}

    void PlaceInLayer()
    {
        int layer;

        if (m_BaseOnParent)
        {
            layer = m_parent.sortingOrder + m_parentOffset;
        }
        else
        {
            layer = -Mathf.FloorToInt(transform.position.y * 10);
        }

        m_renderer.sortingOrder = layer;
    }

}
