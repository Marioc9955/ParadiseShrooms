using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMadera : Enemy
{
    Animator anim;

    [SerializeField] private GameObject ramaPrefab;
    [SerializeField] Transform pntA, pntB, puntoDisparo;

    AudioManager aud;
    
    public float velocidad = 2;
    [SerializeField] float distanciaCambio = 1.5f;
    bool pntAaB = true;

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    public override void Patrullar()
    {
        //print(name + " patrullando");
        transform.position = Vector3.MoveTowards(
            transform.position,
            pntAaB ? pntA.position : pntB.position,
            velocidad * Time.deltaTime);

        if (Vector3.Distance(transform.position,
            pntAaB ? pntA.position : pntB.position) < distanciaCambio)
        {
            //print("cambia dir");
            transform.forward = -transform.forward;
            pntAaB = !pntAaB;
        }
    }

    public override void Atacar()
    {
        //print(name + " ataca");
        transform.forward = Vector3.Cross(player.position - transform.position, Vector3.up).normalized;
        if (!yaAtaco)
        {
            anim.SetTrigger("attack");
            yaAtaco = true;
            Invoke(nameof(ResetAttack), timeAttacks);
        }
    }

    public override void Perseguir()
    {
        //print(name + " persigue");
        //transform.forward = (player.position - transform.position).normalized;
        transform.forward = Vector3.Cross(player.position - transform.position, Vector3.up).normalized;
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            velocidad * Time.deltaTime);
       
    }

    //metodo llamado por animator
    void Disparar()
    {
        GameObject ramaDisparo = Instantiate(ramaPrefab, puntoDisparo.position, Quaternion.AngleAxis(-90, Vector3.forward));
        ramaDisparo.transform.forward = transform.forward;
        ramaDisparo.transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
        Physics2D.IgnoreCollision(ramaDisparo.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        ramaDisparo.GetComponent<Rigidbody2D>().velocity = new Vector2(velocidad * 5 , 0) * transform.right;
    }

    public override void TakeDamage(int damage)
    {
        AudioManager.instance.Play("DamageMadera");
        anim.SetTrigger("damaged");
        base.TakeDamage(damage);
    }
}
