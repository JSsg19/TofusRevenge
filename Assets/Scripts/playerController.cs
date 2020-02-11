using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;

    private State state;
    private enum State
    {
        Normal,
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = State.Normal;
    }
    void Update()
    {
        switch (state)
        {
            default:
            case State.Normal:
                movement();
                jump();
                break;
        }
    }
    void movement()
    {
        float extraMoveSpeed = 5f;
        float moveX = Input.GetAxis("Horizontal") * Time.deltaTime;
        transform.position += moveX * transform.right * extraMoveSpeed;
    }
    public LayerMask groundLayer;
    public GameObject gCheckObject;
    public KeyCode jumpKey;
    public bool isGrounded;
    void jump()
    {
        float extraJumpSpeed = 5f;
        if(Physics2D.OverlapCircleAll(transform.position,.51f, groundLayer).Length > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        if (Input.GetKeyDown(jumpKey) && isGrounded) 
        {
            Debug.Log("Jump");
            rb.velocity = extraJumpSpeed * transform.up;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gCheckObject.transform.position, .52f);
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}