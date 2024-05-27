using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBarreira : MonoBehaviour
{

    [SerializeField] private GameObject Barreira;
    [SerializeField] private float IntervaloSpawn;
    private float Timer;
    [SerializeField] private float VariacaoY;


    // Start is called before the first frame update
    void Start()
    {
        Timer = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if(Timer < IntervaloSpawn)
        {
            Timer += Time.deltaTime;
        }
        else
        {
            Spawn();
            Timer = 0;
        }

    }

    public void Spawn()
    {
        float MaisBaixo = transform.position.y - VariacaoY;
        float MaisAlto = transform.position.y + VariacaoY;

        Instantiate(Barreira, new Vector3(transform.position.x, Random.Range(MaisBaixo, MaisAlto), 0), transform.rotation);
        
    }
}
