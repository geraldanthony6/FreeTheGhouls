using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer playerSprite;
    public float playerSpeed = 7f;
    float jumpForce = 12f;
    
    bool canJump = true;
    bool isJumping = false;
    public bool isHit = false;

    [SerializeField]Rigidbody2D playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if(horizontal < 0){
            playerSprite.flipX = true;
        } else if(horizontal > 0){
            playerSprite.flipX = false;
        }
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        transform.Translate(Vector2.right * playerSpeed * horizontal * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space) && canJump == true){
            canJump = false;
            isJumping = true;
            animator.SetBool("IsJumping", true);
            playerRb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Floor"))
        {
            canJump = true;
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(PlayerHit());
        } 
    }

    IEnumerator PlayerHit()
    {
        animator.SetBool("isHit", true);

        yield return new WaitForSeconds(1);

        animator.SetBool("isHit", false);
    }
}
