using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Corpo;
    [SerializeField] private float Velocidade;
    [SerializeField] private float VelocidadeNoAr;
    [SerializeField] private float MultiplicadorVelocidade;
    private float DirecaoMovimento;
    private bool IndoParaDireita;


    [Header("Para Jump")]
    [SerializeField] private LayerMask Chao;
    [SerializeField] private Transform PeDoPersonagem;
    [SerializeField] private float ForcaPulo;
    private bool PertoDoChao;
    

    [Header("Para Wall Slide")]
    [SerializeField] private LayerMask Parede;
    [SerializeField] private Transform WallCheck;
    [SerializeField] private float VelocidadeWallSliding;
    [SerializeField] private float VelocidadeWallSlidingAcelerado;
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
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                Corpo.velocity = new Vector2(Corpo.velocity.x, Mathf.Clamp(Corpo.velocity.y, -VelocidadeWallSlidingAcelerado, float.MaxValue));
            }
            else
            {
                Corpo.velocity = new Vector2(Corpo.velocity.x, Mathf.Clamp(Corpo.velocity.y, -VelocidadeWallSliding, float.MaxValue));
            }
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
        PertoDoChao = Physics2D.BoxCast(PeDoPersonagem.position, new Vector2(0.5f, 0.2f), 0f, Vector2.down, 0.1f, Chao);


        //Pulo 
        if (PertoDoChao && Input.GetKeyDown(KeyCode.Space) && !Wallsliding)
        {
            Corpo.AddForce(Vector2.up * ForcaPulo);
            
        }

    }
}
