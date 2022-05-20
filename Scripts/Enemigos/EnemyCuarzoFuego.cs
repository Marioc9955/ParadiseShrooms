using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCuarzoFuego : Enemy
{
    [SerializeField] List<Transform> wayPoints;
    public float velocidad = 2;
    float distanciaCambio = 0.2f;
    byte siguientePos = 0;

    Animator anim;

    [SerializeField] GameObject quarzoDisparo;
    [SerializeField] float velDisparo;
    [SerializeField] Transform puntoDisparo;
    [SerializeField] LimiteFinalEnemy lim;

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //sig posiciones para NO atacar: 0 1 6 7
        if (siguientePos == 6 || siguientePos == 0 || siguientePos == 1 || siguientePos == 6 || siguientePos==7)
        {
            anim.SetTrigger("corre");
        }
        else
        {
            anim.SetTrigger("attack");
        }

        velocidad += 0.15f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(
            transform.position,
            wayPoints[siguientePos].transform.position,
            velocidad * Time.deltaTime);

        if (Vector3.Distance(transform.position,
            wayPoints[siguientePos].transform.position) < distanciaCambio)
        {
            siguientePos++;
            if (siguientePos >= wayPoints.Count)
            {
                siguientePos = 0;
                //Debug.Log(siguientePos);
            }

        }
    }

    void Disparar()
    {
        Vector2 dir = Random.insideUnitCircle;
        GameObject ramaDisparo = Instantiate(quarzoDisparo, puntoDisparo.position, Quaternion.identity);
        Physics2D.IgnoreCollision(ramaDisparo.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Rigidbody2D rb = ramaDisparo.GetComponent<Rigidbody2D>();
        rb.AddTorque(5);
        rb.velocity = velDisparo * dir;
    }

    public override void TakeDamage(int damage)
    {
        AudioManager.instance.Play("DamageCuarzo");
        anim.SetTrigger("damaged");
        base.TakeDamage(damage);
    }

    public override void Morir()
    {
        lim.MuereEnemigoFinal();
        base.Morir();
    }
}
