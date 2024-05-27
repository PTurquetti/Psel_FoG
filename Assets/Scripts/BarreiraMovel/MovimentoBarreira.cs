using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoBarreira : MonoBehaviour
{

    [SerializeField] private float Velocidade;
    [SerializeField] private float TempoDestruicao;
    private float Timer;

    // Start is called before the first frame update
    void Start()
    {
        Timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.right * Velocidade) * Time.deltaTime;

        Timer += Time.deltaTime;

        if (Timer >= TempoDestruicao)
        {
            Destroy(this.gameObject);
        }
    }

}
