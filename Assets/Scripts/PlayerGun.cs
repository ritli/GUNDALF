using UnityEngine;
using System.Collections;

public class PlayerGun : MonoBehaviour {

    public GameObject m_bullet;
    public float m_cooldown;
    float m_currentCD;
    SpriteRenderer m_sprite;
    PlayerMove m_player;
    AudioSource m_audio;

    bool m_reloadplayed = false;

    public AudioClip m_reloadSound;
    public AudioClip m_gunshotSound;

	void Start () {
        m_audio = GetComponent<AudioSource>();
        m_sprite = GetComponent<SpriteRenderer>();
        m_player = GetComponentInParent<PlayerMove>();
	}
	
	void Update () {
        LookAtMouse();
        Shoot();
	}

    void LookAtMouse()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        if (m_player.GetDirection() == 4)
        {
            m_sprite.sortingOrder = m_player.GetComponent<SpriteRenderer>().sortingOrder - 1;
        }
        else
        {
            m_sprite.sortingOrder = m_player.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
    }

    void Shoot()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (m_currentCD >= m_cooldown)
            {
                m_reloadplayed = false;
                m_currentCD = 0f;
                Projectile p = ((GameObject)Instantiate(m_bullet, transform.position + transform.up * 0.8f, transform.rotation)).GetComponent<Projectile>();
                p.SetLayer(GetComponent<SpriteRenderer>().sortingLayerID);
                PlayGunshot();
            }
            else
            {
                m_currentCD += Time.deltaTime;
            }
        }
        else if (!m_reloadplayed)
        {
            Invoke("PlayReload", 0.5f);
            m_reloadplayed = true;
        }
    }

    void PlayGunshot()
    {
            m_audio.PlayOneShot(m_gunshotSound, 0.7f);
    }
    void PlayReload()
    {
            m_audio.PlayOneShot(m_reloadSound, 1);
    }
}
