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
    public KeyCode aimKey;
    uiController ui;
    private enum State  
    { //Alla olika states som player kan ha
        Normal,
        Dash,
        Ult,
        Aiming
    }
    private void Start()
    { 
        ui = FindObjectOfType<uiController>();
        rb = GetComponent<Rigidbody2D>();
        state = State.Normal;  //Sätter state till "Normal" när spelet startas
        Cursor.lockState = CursorLockMode.Locked; //Låser muspekaren i mitten av skärmen och gör den onsynlig
    }
    void Update()
    {
        switch (state)
        {  //Hanterar vilken state som ska hanteras
            default:
            case State.Normal:
                rb.simulated = true; //tillåter spelarens rigidbody att påvärkas av extärna krafter
                movement(); //Tillåter "movement"
                jump(); //Tillåter funktionen "jump"
                StartCoroutine(dashSwitch(.1f)); 
                if (Input.GetKeyDown(ultKey) && ui.energyBar.value == 100) 
                {  //Om den angivna knappen trycks ned så och värdet på "energyBar" är lika med 100 så sätts "state" till "Ult"
                    state = State.Ult;
                }
                if (Input.GetKeyDown(aimKey) && isGrounded)
                { //Om den angivna knappen trycks ned och "isGrounded" är sant så byt state till "Aiming"
                    state = State.Aiming;
                }
                break;
            case State.Dash:
                rb.simulated = false;  //Slutar tillåta Players Rigidbody att bli påverkad av extärna krafter
                transform.position += transform.right * moveX * 50f; //Flytar spelaren i sidled beroende på den aktiva riktningen.
                StartCoroutine(returnNormalByT(.04f)); //Startar en Timer som sätter staten till "normal" efter den givna parametern
                break;
            case State.Ult: 
                ui.energyBar.value = 0; //Sätter "energyBar" värdet till 0
                rb.simulated = false; //Slutar tillåta Players Rigidbody att bli påverkad av extärna krafter
                StartCoroutine(returnNormalByT(3f)); //Startar en Timer som sätter staten till "normal" efter den givna parametern
                ult(); //Kallar på funktionen "ult"
                break;
            case State.Aiming:
                if (Input.GetKeyUp(aimKey))
                {//Om den angivna knappen trycks ned så byter funktionen "State" till "Normal"
                    StartCoroutine(returnNormalByT(0));
                }
                break;
        }
    }
    void ult()
    { //(inte klar)
        //funktionen för Ult
    }
    IEnumerator returnNormalByT(float t)
    { //Statar en Timer med t sekunder och byter "state" till "normal" samt sänker "dashCount" med 1 när timern är klar
        yield return new WaitForSeconds(t);
        state = State.Normal;
        dashCount--;
    }
    void movement()
    {
        float extraMoveSpeed = 5f; //En lokal variable som sparar värdet för rörelsehastigheten
        moveX = Input.GetAxis("Horizontal") * Time.deltaTime; //Sätter den aktiva rikningen till höger eller vänster beroende på vilken knapp som trycks ned (A eller D)
        transform.position += moveX * transform.right * extraMoveSpeed; // Ändrar positionen i den aktiva riktningen gånger rörelsehastigheten
    }
    public LayerMask groundLayer; //Lagret för mark
    public GameObject gCheckObject; 
    public KeyCode jumpKey;
    public bool isGrounded;
    void jump()
    { 
        float extraJumpSpeed = 5f; //en lokal variabel som sparar värdet för hopphastigheten uppåt
        if (Physics2D.Raycast(gCheckObject.transform.position, -transform.up, .1f, groundLayer)) 
        {  //Sickar en onsynlig stråle nedåt och om den träffar något med lagret "groundLayer" så startas "jumpCd"
            StartCoroutine(jumpCd()); 
        }
        else
        { //Annars så sätter vi isGrounded till false
            isGrounded = false;
        }
        if (Input.GetKeyDown(jumpKey) && isGrounded) 
        { //Om den angivna knappen trycks ned och "isGrounded" är lika med "True" så adderas hastighet uppåt
            Debug.Log("Jump");
            rb.velocity = extraJumpSpeed * Vector3.up;
        }
    }
    private void OnDrawGizmos()
    { //Ritar ut olika mått som är synliga i inspektor
        Gizmos.color = Color.green;
        Gizmos.DrawRay(gCheckObject.transform.position, -transform.up);
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
    IEnumerator jumpCd()
    { //Startar en timer på 0.1 sekunder och sätter isGrounded till True och dashCount till 1
        yield return new WaitForSeconds(.1f);
        isGrounded = true;
        dashCount = 1f;
    }
    IEnumerator dashSwitch(float t)
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCount > 0)
        { //Om den angivna knappen trycks ned och dashCount är större än 0 så byts "state" till dash
            state = State.Dash;
        }
        yield return new WaitForSeconds(t); //Vänta på t sekunder
    }
}