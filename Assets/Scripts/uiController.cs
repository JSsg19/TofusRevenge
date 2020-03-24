using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiController : MonoBehaviour
{
    public Slider energyBar;
    void Update()
    { //Ökar energyBar värdet med 10 varje sekund
        energyBar.value += Time.deltaTime * 10f;
    }
}