using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDoJogo : MonoBehaviour
{
    //Pega o Canvas da UI de morte
    [SerializeField] GameObject CanvasMorreu;

    public void Morreu()
    {
        //Para o tempo e ativa o Canvas
        Time.timeScale = 0.0f;
        CanvasMorreu.gameObject.SetActive(true);
    }
}
