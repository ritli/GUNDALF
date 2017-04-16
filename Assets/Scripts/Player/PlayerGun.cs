using UnityEngine;
using System.Collections;

public class PlayerGun : MonoBehaviour {

    public GameObject m_bullet;
    public float m_cooldown;
    public Item m_gunItem;
    float m_currentCD;

    SpriteRenderer m_sprite;
    PlayerMove m_player;
    AudioSource m_audio;
    CameraController m_camera;
    UIManager m_canvas;

    bool m_reloadplayed = false;
    bool m_canShoot = true;

    public AudioClip m_reloadSound;
    public AudioClip m_gunshotSound;

	void Start () {
        m_audio = GetComponent<AudioSource>();
        m_sprite = GetComponent<SpriteRenderer>();
        m_player = GetComponentInParent<PlayerMove>();
        m_camera = Camera.main.GetComponent<CameraController>();
        m_canvas = Manager.GetCanvas();
        EquipGun(m_gunItem);
    }
	
    public void EquipGun(Item i)
    {
        m_canShoot = i;
        m_gunItem = i;

        if (i)
        {
            PlayReload();
            m_sprite.enabled = true;
            m_sprite.sprite = i.m_sprite;
            GetComponentInChildren<Light>().enabled = i.m_gunStats.flashlight;
        }
        else
        {
            GetComponentInChildren<Light>().enabled = false;
            m_sprite.enabled = false;
        }

    }

	void Update () {
        if (!m_player.m_controlsDisabled) { 
            LookAtMouse();

            if (m_canShoot){ 
                ShootUpdate();
            }

            m_canvas.UpdateCrosshair();
        }
    }

    void LookAtMouse()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        if (m_player.GetDirection() == 4)
        {
            m_sprite.sortingOrder = m_player.GetComponent<SpriteRenderer>().sortingOrder - 2;
        }
        else
        {
            m_sprite.sortingOrder = m_player.GetComponent<SpriteRenderer>().sortingOrder + 2;
        }
    }

    void ShootUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (m_currentCD >= m_cooldown)
            {
                m_reloadplayed = false;
                m_currentCD = 0f;

                Projectile p = ((GameObject)Instantiate(m_bullet, transform.position + transform.up * 0.8f, transform.rotation)).GetComponent<Projectile>();
                p.SetLayer(GetComponent<SpriteRenderer>().sortingOrder);
                for (int i = 1; i < m_gunItem.m_gunStats.shotCount; i++)
                {
                    Projectile b = ((GameObject)Instantiate(m_bullet, transform.position + transform.up * 0.8f + transform.right * i * 0.4f * (-1 * i%2 + (1 * i+1%2)), transform.rotation)).GetComponent<Projectile>();
                    b.SetLayer(GetComponent<SpriteRenderer>().sortingOrder);
                }
             
                PlayGunshot();
                m_camera.SetScreenShake(m_cooldown, 1f);
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
