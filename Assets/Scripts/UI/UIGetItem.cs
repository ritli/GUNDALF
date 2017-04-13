using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGetItem : MonoBehaviour {

    Animator m_animator;
    Image m_item;
    Text m_text;

	void Start () {
        transform.localScale = Vector2.zero;

        m_animator = GetComponent<Animator>();
        m_item = transform.FindChild("Item").GetComponent<Image>();
        m_text = GetComponentInChildren<Text>();
    }

    public void PlayGetItem(Item i)
    {
        m_text.text = i.m_stats.name + " Get!";
        m_item.sprite = i.m_sprite;
        m_animator.Play("GetAnim");
    }

    IEnumerator PlayGetAnimation()
    {
        yield return new WaitForSeconds(1);
    }

	void Update () {
		
	}
}
