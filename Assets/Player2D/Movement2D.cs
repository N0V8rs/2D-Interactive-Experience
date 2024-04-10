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

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _levelManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        HandleMove();
        HandleAnimmation();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("WinTrigger"))
        {
            GameObject.FindObjectOfType<LevelManager>().LoadScene(sceneName);
        }

        if (other.gameObject.CompareTag("FinishTrigger"))
        {
            gameManager.GameWin();
            GameObject.FindObjectOfType<LevelManager>().LoadScene("WinningScene");

        }

        if (other.gameObject.CompareTag("DeathTrigger"))
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

    public void HandleAnimmation()
    {
        if (movement != Vector2.zero)
        {
            anim.SetFloat("MovementX", movement.x);
            anim.SetFloat("MovementY", movement.y);
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }
    }
}
