using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public float hp;
    private void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void takeDamage(float value)
    {
        hp -= value;
    }
}
