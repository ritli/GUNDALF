using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject m_uiGundalf;
    public GameObject m_ingamePanel;
    public Slider m_uiHealthbar;
    public GameObject m_crosshair;

	void Start () {
        Cursor.visible = false;
        Time.timeScale = 0;
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
