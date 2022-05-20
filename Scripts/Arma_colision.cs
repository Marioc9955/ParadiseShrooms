using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma_colision : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);

        }
    }
}
