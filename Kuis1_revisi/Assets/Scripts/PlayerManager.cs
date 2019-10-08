using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public float speedRunX, jumpSpeedY,speed, delayBeforeDoubleJump;
    public bool facingRight, jumping, isGrounded, canDoubleJump;
    public GameObject leftbullet, rightbullet;
    Animator anime;
    Transform firepas;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        facingRight = true;

        firepas = transform.Find("firePas");
    }

    // Update is called once per frame
    void Update()
    {
        //player movement
        MovePlayer(speed);

        //player Jump and Fall
        HandleJumpAndFall();

        //player flip
        Flip();

        // left player movement
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            speed = -speedRunX;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            speed = 0;
        }

        // right player movement
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            speed = speedRunX;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            speed = 0;
        }

        //Single jump Player movement
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            Jump();
        }

        //Double jump Player movement
        if (Input.GetKeyDown(KeyCode.UpArrow) && canDoubleJump)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }

    }

    private void MovePlayer(float Playerspeed)
    {
        //code player movement

        if (Playerspeed < 0 && !jumping || Playerspeed > 00 && !jumping)
        {
            anime.SetInteger("State", 2);
        }
        if (Playerspeed == 0 && !jumping)
        {
            anime.SetInteger("State", 0);
        }

        rb.velocity = new Vector3(speed, rb.velocity.y, 0);
    }

    void HandleJumpAndFall()
    {
        if (jumping)
        {
            if (rb.velocity.y > 0)
            {
                anime.SetInteger("State", 3);
            }
            else
            {
                anime.SetInteger("State", 1);
            }
        }
    }

    void Flip()
    {
        //code flip to the player direction
        if (speed > 0 && !facingRight || speed < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            jumping = false;
            canDoubleJump = false;
            anime.SetInteger("State", 0);
        }
        if (collision.gameObject.tag == "Box")
        {
            isGrounded = true;
            jumping = false;
            canDoubleJump = false;
            anime.SetInteger("State", 0);
        }
        if (collision.gameObject.tag == "BoxRed")
        {
            isGrounded = true;
            jumping = false;
            canDoubleJump = false;
            anime.SetInteger("State", 0);
        }
        if (collision.gameObject.tag == "batasTanah")
        {
            SceneManager.LoadScene("Menu");
        }

    }

    public void Jump()
    {
        //Single Jump
        if (isGrounded)
        {
            jumping = true;
            isGrounded = false;
            rb.AddForce(new Vector2(rb.velocity.x, jumpSpeedY));
            anime.SetInteger("State", 3);
            Invoke("EnableDoubleJump", delayBeforeDoubleJump);
        }

        //Double Jump
        if (canDoubleJump)
        {
            canDoubleJump = false;
            rb.AddForce(new Vector2(rb.velocity.x, jumpSpeedY));
            anime.SetInteger("State", 3);
        }
    }

    void EnableDoubleJump()
    {
        canDoubleJump = true;
    }

    private void Fire()
    {
        if (facingRight)
        {
            Instantiate(rightbullet, firepas.position, Quaternion.identity);
        }
        if (!facingRight)
        {
            Instantiate(leftbullet, firepas.position, Quaternion.identity);
        }
    }

    public void LoadLevel(int x) {
        SceneManager.LoadScene(x);
    }
}
