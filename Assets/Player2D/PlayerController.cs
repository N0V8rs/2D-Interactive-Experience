using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator anim;
    Vector2 movement;
    public GameManager gameManager;
    public LevelManager _levelManager;
    public string sceneName;
    public Inventory Inventory;
    public static PlayerController Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Inventory = new Inventory();
        }
        else
        {
            Debug.LogError("More than one instance of PlayerController found!");
            Destroy(this.gameObject);
        }

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _levelManager = FindObjectOfType<LevelManager>();
    }

    void Start()
    {
        Inventory = GetComponent<Inventory>();

        if (Inventory == null)
        {
            Debug.LogError("Inventory component not found on PlayerController.");
        }
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        HandleMove();
        HandleAnim();

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("UnlockedDoor"))
        {
            GameObject.FindObjectOfType<LevelManager>().LoadScene(sceneName);
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            gameManager.GameWin();
            GameObject.FindObjectOfType<LevelManager>().LoadScene("WinningScene");

        }

        if (other.gameObject.CompareTag("LoseTrigger"))
        {
            gameManager.GameOver();
            gameManager.gameState = GameManager.GameState.GameOver;
        }
    }

    public void HandleMove()
    {
        Vector2 normalizedMovement = movement.normalized;

        rb.velocity = normalizedMovement * moveSpeed;
    }

    public void HandleAnim()
    {
        if (movement != Vector2.zero)
        {
            anim.SetFloat("XInput", movement.x);
            anim.SetFloat("YInput", movement.y);
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }
    }
}
