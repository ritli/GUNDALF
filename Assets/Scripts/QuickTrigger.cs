using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTrigger : MonoBehaviour {

    public AudioClip ac;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

           Manager.GetCamera().GetComponent<AudioSource>().clip = ac;
           Manager.GetCamera().GetComponent<AudioSource>().time = 30;
           Manager.GetCamera().GetComponent<AudioSource>().Play(); 
        }
    }
}
