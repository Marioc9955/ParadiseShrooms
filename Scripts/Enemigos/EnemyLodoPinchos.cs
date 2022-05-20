using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLodoPinchos : Enemy
{
    [SerializeField] List<Transform> wayPoints, puntosDisparoPinchos;
    public float velocidad = 2;
    float distanciaCambio = 1.2f;
    byte siguientePos = 0;

    [SerializeField] GameObject pinchoDisparo;

    [SerializeField] float velDisparo;

    Animator anim;

    [SerializeField] LimiteFinalEnemy lim;

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //sig posiciones para atacar: 6, 9, 11, 13, >=15, <19, >=20, <24
        if (siguientePos == 6 || siguientePos == 9 || siguientePos == 11 || siguientePos == 13 ||
            (siguientePos >= 15 && siguientePos < 19) || (siguientePos >= 20 && siguientePos < 24))
        {
            anim.SetTrigger("attack");
        }
        else
        {
            anim.SetTrigger("corre");
        }


        velocidad += 0.1f * Time.deltaTime;
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

    void DispararPinchos()
    {
        StartCoroutine(DispararMunicion(pinchoDisparo));
    }

    IEnumerator DispararMunicion(GameObject prefab)
    {
        foreach (Transform t in puntosDisparoPinchos)
        {
            Vector2 dir = Random.insideUnitCircle;
            GameObject ramaDisparo = Instantiate(prefab, t.position, Quaternion.identity);
            Physics2D.IgnoreCollision(ramaDisparo.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            Rigidbody2D rb = ramaDisparo.GetComponent<Rigidbody2D>();
            rb.AddTorque(5);
            rb.velocity = velDisparo * dir;
            yield return new WaitForSeconds(0.5f);
        }
    }

    public override void TakeDamage(int damage)
    {
        AudioManager.instance.Play("DamageMadera");
        anim.SetTrigger("damaged");
        base.TakeDamage(damage);
    }

    public override void Morir()
    {
        lim.MuereEnemigoFinal();
        base.Morir();
    }
}
