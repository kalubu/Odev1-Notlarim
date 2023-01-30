using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController2 : MonoBehaviour
{
    public float jumpForce = 150.0f;
    public float speed = 1.0f;

    private float moveDirection;
    private bool grounded = true;
    private bool jump;
    private bool moving;
    private Rigidbody2D _rigidBody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if(_rigidBody2D.velocity != Vector2.zero)
        {
            moving= true;
        }
        else
        {
            moving = false;
        }
        _rigidBody2D.velocity = new Vector2(speed * moveDirection, _rigidBody2D.velocity.y);
        if(jump == true)
        {
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x, jumpForce);
            jump = false;
        }
    }
    private void Update()
    {
        if(grounded == true && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) {
            if (Input.GetKey(KeyCode.A))
            {
                moveDirection = -1.0f;
                _spriteRenderer.flipX = true;
                anim.SetFloat("speed", speed);
            }
            else if(Input.GetKey(KeyCode.D))
            {
                moveDirection = 1.0f;
                _spriteRenderer.flipX = false;
                anim.SetFloat("speed", speed);
            }
        }   else if (grounded == true)
        { 
            moveDirection = 0;
            anim.SetFloat("speed", 0.0f);

        }
        if(grounded == true && Input.GetKey(KeyCode.W))
        {
            jump = true;
            grounded = false;
            anim.SetTrigger("jump");
            anim.SetBool("grounded", false);
        }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("ground"))
        {
            anim.SetBool("grounded", true);
            grounded = true;
        }
        
    }
}
