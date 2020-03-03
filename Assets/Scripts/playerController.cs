using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;
    float moveX;
    private State state;

    float dashCount;
    private enum State
    {
        Normal,
        Dash,
        Ult
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = State.Normal;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        switch (state)
        {
            default:
            case State.Normal:
                movement();
                jump();
                StartCoroutine(dashSwitch(.1f));
                break;
            case State.Dash:
                rb.gravityScale = 0f;
                rb.velocity = transform.right * moveX * 3500f;
                StartCoroutine(returnNormalByT(.04f));
                break;
        }
    }
    IEnumerator returnNormalByT(float t)
    {
        yield return new WaitForSeconds(t);
        rb.gravityScale = 1f;
        dashCount--;
        rb.velocity = Vector3.zero;
        state = State.Normal;
    }
    void movement()
    {
        float extraMoveSpeed = 5f;
        moveX = Input.GetAxis("Horizontal") * Time.deltaTime;
        transform.position += moveX * transform.right * extraMoveSpeed;
    }
    public LayerMask groundLayer;
    public GameObject gCheckObject;
    public KeyCode jumpKey;
    public bool isGrounded;
    void jump()
    {
        float extraJumpSpeed = 5f;
        if (Physics2D.Raycast(gCheckObject.transform.position, -transform.up, .1f, groundLayer)) 
        {
            StartCoroutine(jumpCd());
        }
        else
        {
            isGrounded = false;
        }
        if (Input.GetKeyDown(jumpKey) && isGrounded) 
        {
            Debug.Log("Jump");
            rb.velocity = extraJumpSpeed * Vector3.up;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(gCheckObject.transform.position, -transform.up);
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
    IEnumerator jumpCd()
    {
        yield return new WaitForSeconds(.1f);
        isGrounded = true;
        dashCount = 1f;
    }
    IEnumerator dashSwitch(float t)
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCount > 0)
        {
            state = State.Dash;
        }
        yield return new WaitForSeconds(t);
    }
}