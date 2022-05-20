using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunicionEnemigo : MonoBehaviour
{
    private void Start()
    {
        Explodable e;
        if (TryGetComponent<Explodable>(out e))
        {
            e.allowRuntimeFragmentation = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInChildren<PlayerCombat>().TakeDamage(5);
        }
        Explodable e;
        if (TryGetComponent<Explodable>(out e))
        {
            //print("explota rama");
            e.explode();
            ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
            ef.doExplosion(transform.position);
        }
    }
}
