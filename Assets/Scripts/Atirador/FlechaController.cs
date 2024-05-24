using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlechaController : MonoBehaviour
{
    //Velocidade da flecha
    [SerializeField] private float Velocidade;

    void Update()
    {
        //Move a flecha na direção à sua direita
        //O Time.deltaTime faz a velocidade adaptar à taxa de fps do jogo,
        //assim mantendo a velocidade constante em computadores de desempenho diferentes
        transform.transform.position += transform.right * Velocidade * Time.deltaTime;
    }

    //Ao colidir
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.collider.tag == "DestruidorDeFlecha")
        {
            Destroy(this.gameObject);
        }
    }

    //Ao sair do campo de visão de todas as câmeras
    private void OnBecameInvisible()
    {
        //Destrói a flecha
        Destroy(this.gameObject);
    }
}
