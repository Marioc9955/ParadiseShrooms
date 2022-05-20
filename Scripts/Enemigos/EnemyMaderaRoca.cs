using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaderaRoca : Enemy
{
    Animator anim;

    [SerializeField] List<Transform> wayPoints, puntoDisparoPiedras, puntoDisparoRamas;
    public float velocidad = 2;
    float distanciaCambio = 0.2f;
    byte siguientePos = 0;

    [SerializeField] GameObject ramaPrefab, piedraPrefab;

    [SerializeField] float velDisparo;

    [SerializeField] LimiteFinalEnemy lim;

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
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
        if (siguientePos == 9 || siguientePos==10)
        {
            anim.SetTrigger("corre");
        }
        else
        {
            anim.SetTrigger("attack");
        }
    }

    void DispararRama()
    {
        StartCoroutine(DispararMunicion(ramaPrefab));
    }

    IEnumerator DispararMunicion(GameObject prefab)
    {
        foreach (Transform t in puntoDisparoPiedras)
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

    void DispararPiedra()
    {
        StartCoroutine(DispararMunicion(piedraPrefab));
    }

    public override void TakeDamage(int damage)
    {
        AudioManager.instance.Play("DamageMadera");
        AudioManager.instance.Play("DamageRoca");
        anim.SetTrigger("damaged");
        base.TakeDamage(damage);
    }

    public override void Morir()
    {
        lim.MuereEnemigoFinal();
        base.Morir();
    }

    //IEnumerator DispararPiedras()
    //{
    //    foreach (Transform t in puntoDisparoPiedras)
    //    {
    //        Vector2 dir = Random.insideUnitCircle;
    //        GameObject piedraDisparo = Instantiate(piedraPrefab, t.position, Quaternion.identity);
    //        Physics2D.IgnoreCollision(piedraDisparo.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    //        Rigidbody2D rb = piedraDisparo.GetComponent<Rigidbody2D>();
    //        rb.AddTorque(5);
    //        rb.velocity = velDisparo * dir;
    //        yield return new WaitForSeconds(0.5f);
    //    }
    //}

}
