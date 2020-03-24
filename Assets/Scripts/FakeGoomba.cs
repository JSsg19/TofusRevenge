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
        isGrounded = Physics2D.Raycast(transform.position, -transform.up, .1f); //Skapar en onsynlig stråle nedåt och om den träffar något blir variablen sann och om inte blir den falsk
        transform.position += -transform.right * Time.deltaTime * speed;   //Flyttar FakeGoomba åt vänster med hastigheten av speed i sekunden
        if (Physics2D.Raycast(transform.position, -transform.right, detectRange, ground) && isGrounded)  
        {  //Skickar ut en onsynlig stråle med en viss längd och om den träffar något i layer "Ground" och om isGrounded är sann så kallar den på hoppfunktionen
            Jump(); 
        }
    }
    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * jumpHeight;  //Adderar hastighet upåt
    }
}