using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public float hp;
    private void Die()
    {
        if (hp <= 0)
        {
            //die
        }
    }
    public void takeDamage(float value)
    {
        hp -= value;
    }
}
