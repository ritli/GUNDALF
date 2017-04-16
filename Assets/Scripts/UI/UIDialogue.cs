using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogue : MonoBehaviour {

    [Multiline]
    public string m_dialogueToPrint;
    Text m_text;
    string m_senderName;

    public AudioClip[] m_chatAudioClips;
    public AudioClip m_chatOpenClip;
    public AudioClip m_chatCloseClip;
    public DialogueContainer m_container;

    AudioSource m_audio;
    Animator m_animator;
    Image m_image;
    Text m_nameText;

    const float m_printInterval = 0.02f;

	void Start () {
        m_animator = GetComponent<Animator>();
        m_text = GetComponentInChildren<Text>();
        m_audio = GetComponent<AudioSource>();
        m_image = transform.FindChild("Portrait").GetComponent<Image>();
        m_nameText = transform.FindChild("NamePanel").GetComponentInChildren<Text>();
    }

    void PlayRandomAudio()
    {
        m_audio.PlayOneShot(m_chatAudioClips[Random.Range(0, m_chatAudioClips.Length)]);
    }

    void PlayOpenSound()
    {
        m_audio.PlayOneShot(m_chatOpenClip);
    }

    void PlayCloseSound()
    {
        m_audio.PlayOneShot(m_chatCloseClip);
    }

    public void PrintText(string name, Sprite portrait, Color color)
    {
        m_image.sprite = portrait;
        m_image.color = color;
        m_senderName = name;
        StartCoroutine(PrintLoop());
    }

    IEnumerator PrintLoop()
    {
        m_nameText.transform.parent.gameObject.SetActive(true);
        m_nameText.text = m_senderName;

        m_text.text = null;

        Manager.GetPlayer().m_controlsDisabled = true;

        PlayOpenAnim();
        PlayOpenSound();

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < m_dialogueToPrint.Length; i++)
        {
            m_text.text += m_dialogueToPrint[i];
            yield return new WaitForSeconds(m_printInterval);

            if (i % 4 == 0)
            {
                PlayRandomAudio();
            }

            if (m_dialogueToPrint[i] == '\n')
            {
                yield return new WaitForSeconds(m_printInterval * 10);
            }
        }

        while (!Input.GetButton("Fire1"))
        {
            yield return new WaitForEndOfFrame();
        }

        m_container.EventEnded();
        Manager.GetPlayer().m_controlsDisabled = false;

        PlayCloseAnim();
        PlayCloseSound();

        m_nameText.transform.parent.gameObject.SetActive(false); 
    }

    void PlayOpenAnim()
    {
        m_animator.Play("Open");
    }

    void PlayCloseAnim()
    {
        m_animator.Play("Close");
    }
}
