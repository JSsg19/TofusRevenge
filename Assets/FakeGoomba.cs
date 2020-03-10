using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGoomba : MonoBehaviour
{
    [SerializeField] [Range(1, 100)] float speed = 15;
    public float detectRange = 2f;
    void Update()
    {
        transform.position += -transform.right * Time.deltaTime * speed;
        if (Physics2D.Raycast(transform.position, -transform.right,detectRange))
        {
            Jump();
        }
    }
    void Jump()
    {

    }
}
