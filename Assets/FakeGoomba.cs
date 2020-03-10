using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGoomba : MonoBehaviour
{
    [SerializeField] [Range(1, 100)] float speed = 15;
    [SerializeField] [Range(1, 100)] float jumpHeight = 1;
    public float detectRange = 2f;
    public LayerMask ground;

    bool isGrounded;
    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, -transform.up, .1f);
        transform.position += -transform.right * Time.deltaTime * speed;
        if (Physics2D.Raycast(transform.position, -transform.right, detectRange, ground) && isGrounded) 
        {
            Jump();
        }
    }
    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * jumpHeight;
    }
}