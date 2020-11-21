using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using UnityEngine.UI;

public class Player_2 : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed;
    public float rotate;
    public float drift;
    public float hp = 100;
    public int damage = 10;
    public int point = 0;
    [SerializeField]
    public GameController GC;
    public GameObject Back;
    public GameObject Prot;
    public GameObject _mine;
    public Transform em;
    public Slider slid;
    public Slider ammo;
    public AudioSource drive;
    public AudioSource bamp;
    public ParticleSystem shok;
    private int cource;
    private bool forward;
    private bool protect;
    public float clock;
    public bool seizing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        slid.maxValue = hp;
        cource = 8;
        protect = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GC.gmover)
        {
            if (collision.collider && !protect)
            {
                hp -= damage;
                bamp.Play();
                shok.Play();
                if (collision.gameObject.tag == "mine")
                {
                    hp -= 30f;
                }
            }
        }
    }

    #region Сюда лучше не заглядывать! Тут ад из ифов!!!
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "finish1")
        {
            if (cource == 8)
            {
                point++;
            }
        }
        else if (other.gameObject.tag == "z1")
        {
            if (cource == 8)
            {
                cource = 1;
                forward = true;
            }
            else
            {
                forward = false;
            }
        }
        else if (other.gameObject.tag == "z2")
        {
            if (cource == 1)
            {
                cource = 2;
                forward = true;
            }
            else
            {
                forward = false;
            }
        }
        else if (other.gameObject.tag == "z3")
        {
            if (cource == 2)
            {
                cource = 3;
                forward = true;
            }
            else
            {
                forward = false;
            }
        }
        else if (other.gameObject.tag == "z4")
        {
            if (cource == 3)
            {
                cource = 4;
                forward = true;
            }
            else
            {
                forward = false;
            }
        }
        else if (other.gameObject.tag == "z5")
        {
            if (cource == 4)
            {
                cource = 5;
                forward = true;
            }
            else
            {
                forward = false;
            }
        }
        else if (other.gameObject.tag == "z6")
        {
            if (cource == 5)
            {
                cource = 6;
                forward = true;
            }
            else
            {
                forward = false;
            }
        }
        else if (other.gameObject.tag == "z7")
        {
            if (cource == 6)
            {
                cource = 7;
                forward = true;
            }
            else
            {
                forward = false;
            }
        }
        else if (other.gameObject.tag == "z8")
        {
            if (cource == 7)
            {
                cource = 8;
                forward = true;
            }
            else
            {
                forward = false;
            }
        }
        else if (other.gameObject.tag == "repire")
        {
            if (hp < 100)
            {
                Destroy(other.gameObject);
                seizing = true;
                if (hp <= 80)
                {
                    hp += 20;
                }
                else
                {
                    hp = 100;
                }
            }
        }
        else if (other.gameObject.tag == "shield")
        {
            if (!protect)
            {
                Destroy(other.gameObject);
                StartCoroutine(Waiter());
                seizing = true;
            }
        }
    }
    #endregion

    bool noammo = true;
    float curent;

    private IEnumerator Waiter()
    {
        protect = true;
        Prot.SetActive(true);

        yield return new WaitForSecondsRealtime(clock);
        Prot.SetActive(false);
        protect = false;
    }


    private void Update()
    {
        slid.value = hp;
        ammo.value = curent;

        if (!forward)
        {
            Back.SetActive(true);
        }
        else
        {
            Back.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!noammo)
            {
                _ = Instantiate(_mine, em.position, Quaternion.identity);
                curent = 0;
                noammo = true;
            }
        }

        if (curent < 30f)
        {
            curent += Time.deltaTime * 3f;
        }
        else
        {
            noammo = false;
        }
        ammo.value = curent;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.MoveRotation(rb.rotation + rotate * Time.fixedDeltaTime);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.MoveRotation(rb.rotation - rotate * Time.fixedDeltaTime);
        }

        else if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Impulse);
            if (drive.isPlaying)
            {
                return;
            }
            else
            {
                drive.Play();
            }
        }
        else
        {
            rb.velocity = Vector2.Lerp(transform.up * rb.velocity.magnitude, rb.velocity, drift);
            drive.Pause();
        }
    }
}
