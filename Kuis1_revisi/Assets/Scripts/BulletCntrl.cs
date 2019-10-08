using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCntrl : MonoBehaviour
{
    public Vector2 speed;
    Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = speed;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
