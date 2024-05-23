using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    [SerializeField] private float Velocidade;
    [SerializeField] private float MultiplicadorVelocidade;
    [SerializeField] private Transform PeDoPersonagem;
    [SerializeField] private LayerMask Chao;
    private bool PertoDoChao;
    


    //O corpo do jogador
    [SerializeField] private Rigidbody2D Corpo;
    //Para ele não pular infinitamente
    private bool PodePular = false;
    [SerializeField] private float ForcaPulo;

    [SerializeField] private LayerMask Parede;
    [SerializeField] private Transform WallCheck;
    [SerializeField] private float VelocidadeWallSliding;
    private bool TocandoParede;
    private bool Wallsliding;
    



    void Update()
    {
        Andar();
        Pular();
        WallSlide();
        
    }

    public void WallSlide()
    {
        TocandoParede = Physics2D.OverlapBox(WallCheck.position, new Vector2(.2f, .8f), 0, Parede);

        if (TocandoParede && !PertoDoChao)
        {
            Wallsliding = true;
        }
        else
        {
            Wallsliding = false;
        }

        if (Wallsliding)
        {
            Corpo.velocity = new Vector2(Corpo.velocity.x, Mathf.Clamp(Corpo.velocity.y, -VelocidadeWallSliding, float.MaxValue));
        }
    
    }

    public void Andar()
    {
        //Define a velocidade do corpo baseada na tecla pressionada (Input.GetAxisRaw("Horizontal"))
        //A função retorna 1 se a seta pra direita ou D foram pressionados
        //Retorna -1 se a seta da esquerda ou A foram pressionados
        //Retorna 0 se nenhum direcional foi pressionado
        float movimento_horizontal = Velocidade * Input.GetAxisRaw("Horizontal");

        // Correr rapido com LShift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movimento_horizontal *= MultiplicadorVelocidade;

        }

        //Neste caso, não se usa Time.deltaTime, porque RigidBody2D.velocity já opera baseado na taxa de frames
        Corpo.velocity = new Vector2(movimento_horizontal, Corpo.velocity.y);
    }

    public void Pular()
    {
        //Cria uma caixa, se a caixa colidir com o chao, pode pular
        //Nessa função se passa a posição, tamanho, angulo e distancia(tamanho) em relação a direção
        //Tambem passa um layer mask, pra que somente os layers associados a Chao sejam considerados
        PertoDoChao = Physics2D.BoxCast(PeDoPersonagem.position, new Vector2(0.5f, 0.2f), 0f, Vector2.down, 0.1f, Chao);

        //Se o acerto tem um resultado não nulo, pode pular
        if (PertoDoChao)
        {
            PodePular = true;
        }
        else //Caso contrário, não se pode pular
        {
            PodePular = false;
        }

        //Se a barra de espaço foi pressionada e o jogador pode pular
        if (Input.GetKeyDown(KeyCode.Space) && PodePular)
        {
            //Adiciona uma força para cima proporcional à ForçaPulo
            Corpo.AddForce(Vector2.up * ForcaPulo);
            //Proíbe o jogador de pular
            PodePular = false;
        }
    }
}
