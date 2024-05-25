using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovimentoBolaDeFogo : MonoBehaviour
{

    [SerializeField] private Rigidbody2D Corpo;
    [SerializeField] private float Velocidade;
    [SerializeField] private LayerMask Parede;
    [SerializeField] private Transform WallCheck;
    private bool TocouParede;


    // Start is called before the first frame update
    void Start()
    {
        TocouParede = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Movimento contínuo da bola de fogo
        Corpo.velocity = new Vector2(Velocidade, Corpo.velocity.y);

        // Verifica se a bola de fogo tocou uma parede
        TocouParede = Physics2D.OverlapBox(WallCheck.position, new Vector2(.5f, 1f), 0, Parede);

        if (TocouParede)
        {
            // Inverte a direção da velocidade
            Velocidade = -Velocidade;

            // Gira o objeto em 180 graus em torno do eixo Y para virar visualmente
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }
}
