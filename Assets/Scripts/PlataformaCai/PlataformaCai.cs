using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaCai : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Corpo;

    [SerializeField] private float DelayQueda;
    [SerializeField] private float DelayDestruicao;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Queda());
        }
    }


    private IEnumerator Queda()
    {
        yield return new WaitForSeconds(DelayQueda);
        Corpo.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, DelayDestruicao);

    }
}
