using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject m_uiGundalf;
    public Slider m_uiHealthbar;

	void Start () {
        Time.timeScale = 0;
	}
	
	void Update () {
	
        
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
    }
}
