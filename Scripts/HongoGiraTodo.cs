using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HongoGiraTodo : MonoBehaviour
{
    [SerializeField] bool girarDeCabeza;

    [SerializeField] Animator animVCam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (girarDeCabeza)
            {
                animVCam.SetTrigger("cabeza");
            }
            else
            {
                animVCam.SetTrigger("normal");
            }
        }
    }
}
