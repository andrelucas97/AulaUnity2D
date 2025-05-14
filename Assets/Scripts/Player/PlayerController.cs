using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // VAR PRIVADAS
    private Rigidbody2D rb;
    private float moveX;
    private Animator animator;
    private CapsuleCollider2D colliderPlayer;

    // VAR PÚBLICAS
    [Header("Atributes")]
    public float speed;
    public int addJumps;
    public float jumpForce;
    public int life;
    public string levelName;    

    [Header("Bool")]
    [HideInInspector] //para nao aparecer na Unity
    public bool isGrounded;

    [Header("UI")]
    public TextMeshProUGUI textLife;

    [Header("GameObject")]
    public GameObject menuDie;
    public GameObject pauseMenu;


    private void Awake()
    {
        if (PlayerPrefs.GetInt("wasLoaded") == 1)
        {
            life = PlayerPrefs.GetInt("Life", 0);
            Debug.Log("Game loaded!");
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        colliderPlayer = GetComponent<CapsuleCollider2D>();

        // Carregando Cena Salva

        if (PlayerPrefs.HasKey("wasLoaded") && PlayerPrefs.GetInt("wasLoaded") == 1)
        {
            //LoadGame.Instance.wasLoaded = true;

            float x = PlayerPrefs.GetFloat("PlayerPosX", transform.position.x);
            float y = PlayerPrefs.GetFloat("PlayerPosY", transform.position.y);
            transform.position = new Vector3(x, y, transform.position.z);
        }

    }

    void Update()
    {       
        moveX = Input.GetAxis("Horizontal");

        if (isGrounded)
        {
            addJumps = 1;
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            else
            {
                if (Input.GetButtonDown("Jump") && addJumps > 0)
                {
                    addJumps--;
                    Jump();
                }
            }
        }
        Attack();

        textLife.text = life.ToString();

        // -- Morte --
        if(life <= 0)
        {
            life = Mathf.Max(life, 0);
            this.enabled = false;
            colliderPlayer.enabled = false;
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
            animator.Play("Die", -1);
            menuDie.SetActive(true);
        }

        // -- Pause Menu --
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePause();
        }

        // -- SALVANDO GAME --
        if (Input.GetKeyDown(KeyCode.P))
        {
            string activeScene = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("LevelSaved", activeScene);
            PlayerPrefs.SetInt("Life", life);

            // Salvando posição Player
            PlayerPrefs.SetFloat("PlayerPosX", transform.position.x);
            PlayerPrefs.SetFloat("PlayerPosY", transform.position.y);

            Debug.Log("Game saved");
        }

    }

    void TogglePause()
    {

        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        } else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        animator.SetBool("isJump", true);
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            animator.Play(("Attack"), -1);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemie"))
        {
            KeeperController keeper = collision.GetComponent<KeeperController>();
            if (keeper != null)
            {
                keeper.life--;
            } else
            {
                Debug.LogWarning("Enemie com tag 'Enemie' não tem o componente KeeperController: " + collision.name);
            }
        } 
        if (collision.CompareTag("Gizmo"))
        {
            GizmoController gizmo = collision.GetComponent<GizmoController>();
            if (gizmo != null)
            {
                gizmo.life--;
            } else
            {
                Debug.LogWarning("Gizmo com tag 'Gizmo' não tem o componente GizmoController: " + collision.name);
            }
        }
    }

void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            animator.SetBool("isJump", false);

        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    #region Move
    void Move()
    {
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        if (moveX > 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            animator.SetBool("isRun", true);
        }

        if (moveX < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            animator.SetBool("isRun", true);
        }

        if (moveX == 0)
        {
            animator.SetBool("isRun", false);

        }
    }
    #endregion
}
