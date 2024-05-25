using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorDoJogo : MonoBehaviour
{


    //Pega o Canvas da UI de morte
    [SerializeField] GameObject CanvasMorreu;
    [SerializeField] GameObject CanvasGameOver;


    public void Morreu()
    {
        //Para o tempo e ativa o Canvas
        Time.timeScale = 0.0f;
        CanvasMorreu.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        CanvasGameOver.gameObject.SetActive(true);
    }

    public void ProximaFase()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
