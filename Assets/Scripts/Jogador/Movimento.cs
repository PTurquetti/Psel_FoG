using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    [SerializeField] private float Velocidade;
    [SerializeField] private float VelocidadeNoAr;
    [SerializeField] private float MultiplicadorVelocidade;
    [SerializeField] private Transform PeDoPersonagem;
    [SerializeField] private LayerMask Chao;
    private float DirecaoMovimento;
    private bool PertoDoChao;
    private bool IndoParaDireita;
    


    //O corpo do jogador
    [SerializeField] private Rigidbody2D Corpo;
    //Para ele não pular infinitamente
    private bool PodePular = false;
    [SerializeField] private float ForcaPulo;

    [Header("Para Wall Slide")]
    [SerializeField] private LayerMask Parede;
    [SerializeField] private Transform WallCheck;
    [SerializeField] private float VelocidadeWallSliding;
    private bool TocandoParede;
    private bool Wallsliding;

    [Header("Para Wall Jump")]
    [SerializeField] float ForcaWallJump;
    [SerializeField] float DirecaoWallJulp = -1f;
    [SerializeField] Vector2 AnguloWallJump;
    



    private void Start()
    {
        IndoParaDireita = true;
        AnguloWallJump.Normalize();
    }

    void Update()
    {
        StatusPlayer();
        Andar();
        Pular();
        WallSlide();
        WallJump();
    }

    public void StatusPlayer()
    {
        DirecaoMovimento = Input.GetAxisRaw("Horizontal");

        //Cria uma caixa, se a caixa colidir com o chao, pode pular
        //Nessa função se passa a posição, tamanho, angulo e distancia(tamanho) em relação a direção
        //Tambem passa um layer mask, pra que somente os layers associados a Chao sejam considerados
        PertoDoChao = Physics2D.BoxCast(PeDoPersonagem.position, new Vector2(0.5f, 0.2f), 0f, Vector2.down, 0.1f, Chao);
        TocandoParede = Physics2D.OverlapBox(WallCheck.position, new Vector2(.2f, .8f), 0, Parede);

        


    }


    public void Flip()
    {
        if (!Wallsliding)
        {
            IndoParaDireita = !IndoParaDireita;
            DirecaoWallJulp *= -1;
            transform.Rotate(0, 180, 0);
        }

    }

    public void WallSlide()
    {
        

        if (TocandoParede && !PertoDoChao && Corpo.velocity.y < 0)
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


    public void WallJump()
    {
        if (Wallsliding && Input.GetKeyDown(KeyCode.Space))
        {
            Corpo.AddForce(new Vector2(ForcaWallJump * DirecaoWallJulp * AnguloWallJump.x, ForcaWallJump * AnguloWallJump.y), ForceMode2D.Impulse);
            
        }
    }

    public void Andar()
    {
        //Define a velocidade do corpo baseada na tecla pressionada (Input.GetAxisRaw("Horizontal"))
        //A função retorna 1 se a seta pra direita ou D foram pressionados
        //Retorna -1 se a seta da esquerda ou A foram pressionados
        //Retorna 0 se nenhum direcional foi pressionado
        float movimento_horizontal = Velocidade * DirecaoMovimento;

        // Correr rapido com LShift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movimento_horizontal *= MultiplicadorVelocidade;

        }

        if (movimento_horizontal > 0 && !IndoParaDireita)
        {
            Flip();
        }
        else if(movimento_horizontal < 0 && IndoParaDireita)
        {
            Flip();
        }

        //Neste caso, não se usa Time.deltaTime, porque RigidBody2D.velocity já opera baseado na taxa de frames
        if (PertoDoChao)
        {
            Corpo.velocity = new Vector2(movimento_horizontal, Corpo.velocity.y);

        }else if (!PertoDoChao && !Wallsliding && DirecaoMovimento != 0)
        {
            Corpo.AddForce(new Vector2(VelocidadeNoAr * DirecaoMovimento, 0));
            if (Mathf.Abs(Corpo.velocity.x) > Velocidade)
            {
                Corpo.velocity = new Vector2(movimento_horizontal, Corpo.velocity.y);
            }
        }

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
