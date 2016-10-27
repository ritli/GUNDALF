using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {


	void Start () {
        Time.timeScale = 0;
	}
	
	void Update () {
	
	}

    public void PlayButtonFunc()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
