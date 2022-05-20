using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimiteInicialEnemy : MonoBehaviour
{
    AudioManager aud;
    [SerializeField] int numEnemigoFinal; //1 2 o 3
    [SerializeField] GameObject enemigoFinal;

    [SerializeField] Transform player;

    bool enemigoActivo;

    private void Start()
    {
        if (transform.position.x < player.position.x)
        {
            enemigoActivo = false;
        }
        else
        {
            enemigoActivo = true;
        }
        aud = AudioManager.instance;
    }

    private void Update()
    {
        if (!enemigoActivo && transform.position.x > player.position.x)
        {
            GetComponent<Collider2D>().enabled = true;
            GetComponent<Collider2D>().isTrigger = false;
            gameObject.layer = 3;
            enemigoFinal.SetActive(true);
            aud.StopAll();
            aud.Play("EnemigoFinal" + numEnemigoFinal);
            enemigoActivo = true;
        }
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        GetComponent<Collider2D>().isTrigger = false;
    //        gameObject.layer = 3;
    //        aud.StopAll();
    //        aud.Play("EnemigoFinal"+numEnemigoFinal);
    //    }
    //}
}
