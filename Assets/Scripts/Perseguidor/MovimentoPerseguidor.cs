using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPerseguidor : MonoBehaviour
{
    [SerializeField] private Transform[] PontosPatrulha;
    [SerializeField] private float Velocidade;
    private int DestinoPatrulha;

    [SerializeField] private Transform Jogador;
    [SerializeField] private  float DistanciaPerseguicao;
    private bool Perseguindo;


    

    void Update()
    {
        if(DestinoPatrulha == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, PontosPatrulha[0].position, Velocidade * Time.deltaTime);
            if(Vector2.Distance(transform.position, PontosPatrulha[0].position) < .2f)
            {
                DestinoPatrulha = 1;
            }
        }

        if (DestinoPatrulha == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, PontosPatrulha[1].position, Velocidade * Time.deltaTime);
            if (Vector2.Distance(transform.position, PontosPatrulha[1].position) < .2f)
            {
                DestinoPatrulha = 0;
            }
        }
    }
}
