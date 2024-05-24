using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorVida : MonoBehaviour
{


    [SerializeField] private int VidaMax;
    [SerializeField] private int VidaAtual;


    // Start is called before the first frame update
    void Start()
    {
        VidaAtual = VidaMax;
    }

    // Update is called once per frame
    void Update()
    {
        Morreu();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Verifica se colidiu com o jogador
        if (collision.collider.tag == "Flecha" || collision.collider.tag == "Lava")
        {
            //Se sim, avisa o controlador do jogo que ele morreu
            VidaAtual -= 1;
        }

        
    }

    public void Morreu()
    {
        if (VidaAtual <= 0)
        {
            GameObject.FindGameObjectWithTag("Controlador").GetComponent<ControladorDoJogo>().Morreu();
        }
    }


}
