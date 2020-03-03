using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;
    float moveX;
    private State state;

    float dashCount;
    public KeyCode ultKey;
    uiController ui;
    private enum State
    {
        Normal,
        Dash,
        Ult
    }
    private void Start()
    {
        ui = FindObjectOfType<uiController>();
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
                rb.simulated = true;
                movement();
                jump();
                StartCoroutine(dashSwitch(.1f));
                if (Input.GetKeyDown(ultKey) && ui.energyBar.value == 100) 
                {
                    state = State.Ult;
                }
                break;
            case State.Dash:
                rb.simulated = false;
                transform.position += transform.right * moveX * 50f;
                StartCoroutine(returnNormalByT(.04f));
                break;
            case State.Ult:
                ui.energyBar.value = 0;
                rb.simulated = false;
                returnNormalByT(3f);
                break;
        }
    }
    void ult()
    {

    }
    IEnumerator returnNormalByT(float t)
    {
        yield return new WaitForSeconds(t);
        state = State.Normal;
        dashCount--;
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