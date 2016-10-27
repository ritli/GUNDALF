using UnityEngine;
using System.Collections;

[RequireComponent( typeof(SpriteRenderer))]
public class LayerPositioner : MonoBehaviour {

    public bool m_RunInEditor = false;

    SpriteRenderer m_renderer;

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
            m_renderer = GetComponent<SpriteRenderer>();
        }
	}
	
	void Update () {
        PlaceInLayer();
	}

    void PlaceInLayer()
    {
        int layer = Mathf.FloorToInt(transform.position.y * 10);

        m_renderer.sortingOrder = -layer;
    }
}
