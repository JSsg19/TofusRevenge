using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private State state;
    private enum State
    {
        Normal,
    }
    void Update()
    {
        switch (state)
        {
            default:
            case State.Normal:

                break;
        }
    }
    void movement()
    {

    }
    public
    void jump()
    {

    }
}
