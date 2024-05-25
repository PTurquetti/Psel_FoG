using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorVida : MonoBehaviour
{


    [SerializeField] private int VidaMax;
    [SerializeField] private int VidaAtual;
    private bool PodeMorrer;


    // Start is called before the first frame update
    void Start()
    {
        VidaAtual = VidaMax;
        PodeMorrer = true;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Verifica se colidiu com o jogador
        if (collision.collider.tag == "Inimigo" || collision.collider.tag == "Lava")
        {
            //Se sim, avisa o controlador do jogo que ele morreu
            VidaAtual -= 1;
            


            if (VidaAtual > 0 && PodeMorrer)
            {
                PodeMorrer = false;
                Morreu();
                
            }else if(VidaAtual == 0 && PodeMorrer)
            {
                GameOver();
            }
        }
        else if (collision.collider.tag == "Finish")
        {

            GameObject.FindGameObjectWithTag("Controlador").GetComponent<ControladorDoJogo>().ProximaFase();

        }




    }

    public void Morreu()
    {
        
            GameObject.FindGameObjectWithTag("Controlador").GetComponent<ControladorDoJogo>().Morreu();
        
    }


    public void GameOver()
    {

        GameObject.FindGameObjectWithTag("Controlador").GetComponent<ControladorDoJogo>().GameOver();

    }
}
