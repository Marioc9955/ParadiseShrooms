using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //animacion de herida

        if (currentHealth <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        print("muere enemigo " + name);
        StartCoroutine(Desaparecer());
    }

    IEnumerator Desaparecer()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        if (sp != null)
        {
            bool desaparecer = false;
            while(!desaparecer)
            {
                sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, sp.color.a - Time.deltaTime);
                yield return new WaitForSeconds(0.00420f);
                if (sp.color.a <= 0.1f)
                {
                    desaparecer = true;
                    Destroy(gameObject);
                }
            }
        }
    }
}
