using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{

    private void Start()
    {
        if (transform.childCount>0)
        {
            ParticleSystem l = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
            ParticleSystem.ShapeModule sh = l.shape;
            Vector3 scale = new Vector3(transform.lossyScale.x / 2, 1, 1);
            sh.scale = scale;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.Find("GameController").GetComponent<GameController>().GameOver();
        }
    }
}
