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
        Temporizador = TempoEntreSaltos; // Inicializa o temporizador para que o primeiro salto aconteça imediatamente
    }

    void Update()
    {
        // Contador de tempo para o próximo salto
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
            // Inverte a direção do salto
            DirecaoWallJump = -DirecaoWallJump;

            // Gira o objeto em 180 graus em torno do eixo Y para virar visualmente
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }

    void Saltar()
    {
        // Aplica uma força na diagonal, modificada pela direção do salto
        Corpo.AddForce(new Vector2(ForcaWallJump * DirecaoWallJump * AnguloWallJump.x, ForcaWallJump * AnguloWallJump.y), ForceMode2D.Impulse);
    }

    // Método para visualizar a área de detecção no editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(WallCheck.position, new Vector2(.5f, 1f));
    }
}
