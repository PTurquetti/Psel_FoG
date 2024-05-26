using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuadradoSaltitante : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Corpo;
    [SerializeField] private LayerMask Parede;
    [SerializeField] private Transform WallCheck;
    [SerializeField] private float ForcaWallJump;
    [SerializeField] private Vector2 AnguloWallJump;
    private bool TocouParede;
    private float DirecaoWallJump = 1;
    private float TempoEntreSaltos = 2f;
    private float Temporizador;

    void Start()
    {
        Temporizador = TempoEntreSaltos; // Inicializa o temporizador para que o primeiro salto aconte�a imediatamente
    }

    void Update()
    {
        // Contador de tempo para o pr�ximo salto
        Temporizador -= Time.deltaTime;
        if (Temporizador <= 0)
        {
            Saltar();
            Temporizador = TempoEntreSaltos; // Reinicia o temporizador
        }

        // Verifica se o quadrado tocou uma parede
        TocouParede = Physics2D.OverlapBox(WallCheck.position, new Vector2(.5f, 1f), 0, Parede);

        if (TocouParede)
        {
            // Inverte a dire��o do salto
            DirecaoWallJump = -DirecaoWallJump;

            // Gira o objeto em 180 graus em torno do eixo Y para virar visualmente
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }

    void Saltar()
    {
        // Aplica uma for�a na diagonal, modificada pela dire��o do salto
        Corpo.AddForce(new Vector2(ForcaWallJump * DirecaoWallJump * AnguloWallJump.x, ForcaWallJump * AnguloWallJump.y), ForceMode2D.Impulse);
    }

    // M�todo para visualizar a �rea de detec��o no editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(WallCheck.position, new Vector2(.5f, 1f));
    }
}
