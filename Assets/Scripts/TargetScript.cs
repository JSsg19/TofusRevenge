using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public float hp;
    private void Update()
    {
        if (hp <= 0)
        { //Om hp = 0 så föstörs objectet
            Destroy(gameObject);
        }
    }
    public void takeDamage(float value)
    { //Sätter hp till hp minus parametern "value"
        hp -= value;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "playerP")
        { //Om en kollision med playerP ta skada 
            takeDamage(FindObjectOfType<Shooting>().damage);
        }
    }
}
