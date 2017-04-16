using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject m_uiGundalf;
    public GameObject m_ingamePanel;
    public Slider m_uiHealthbar;
    public GameObject m_crosshair;
    UIGetItem m_getItem;
    UIDialogue m_dialogue;


    void Start () {
        m_uiHealthbar = GetComponentInChildren<Slider>();
        m_getItem = GetComponentInChildren<UIGetItem>();
        m_dialogue = GetComponentInChildren<UIDialogue>();
        m_uiGundalf.SetActive(true);
        Cursor.visible = false;
        Time.timeScale = 1;
	}

    public void PlayGetItem(Item i)
    {
        m_getItem.enabled = true;
        m_getItem.PlayGetItem(i);
    }

    public void PrintMessage(string message, string name, Sprite portrait, Color color, DialogueContainer thisObject)
    {
        m_dialogue.m_container = thisObject;
        m_dialogue.m_dialogueToPrint = message;
        m_dialogue.PrintText(name, portrait, color);
    }

    public void UpdateCrosshair()
    {
        m_crosshair.transform.position = Input.mousePosition;
    }

    public void UpdateHealthbar(int currenthealth, int maxhealth)
    {
        m_uiHealthbar.maxValue = maxhealth;
        m_uiHealthbar.value = currenthealth;
    }

    public void PlayButtonFunc()
    {
        Time.timeScale = 1;
        m_uiGundalf.SetActive(false);
        m_crosshair.GetComponent<Image>().enabled = true;
    }



}
