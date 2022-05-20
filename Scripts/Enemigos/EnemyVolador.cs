using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyVolador : Enemy
{

    //Patrullar
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float rangoPatrulla;

    public LayerMask whatIsNotGround;

    public NavMeshAgent agent;
    public GameObject espora;
    Animator anim;

    public override void Start()
    {
        base.Start();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        anim = GetComponent<Animator>();
    }

    public override void BuscarWalkPoint()
    {
        NavMeshHit hit;
        //print("buscando pnto en un rango de " + rangoPatrulla);
        walkPoint = Random.insideUnitCircle.normalized * rangoPatrulla + new Vector2(transform.position.x, transform.position.y);
        if (NavMesh.SamplePosition(walkPoint, out hit, rangoPatrulla, whatIsNotGround))
        {
            walkPoint = hit.position - new Vector3(0, 0, hit.position.z);
            walkPointSet = true;
        }
        //else
        //{
        //    print(walkPoint);
        //}

    }

    void ExpulsarEsporas()
    {
        //print("Atacar");
        Vector3 dir = new Vector3(0, 1);
        float t = 0;
        for (int i = 0; i < 6; i++)
        {
            GameObject esporaInst = Instantiate(espora, transform.position + dir, Quaternion.identity);
            esporaInst.GetComponent<Rigidbody2D>().AddForce(dir, ForceMode2D.Impulse);
            esporaInst.GetComponent<Espora>().tiempoParaExplotar += t;
            t += 0.1f;
            dir = Quaternion.AngleAxis(60, new Vector3(0, 0, 1)) * dir;
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        GetComponent<Animator>().SetTrigger("damaged");
    }

    public override void Atacar()
    {
        agent.SetDestination(transform.position);
        //transform.LookAt(player);
        if (!yaAtaco)
        {
            //animacion que activa la explosion de esporas
            anim.SetTrigger("ataca");
            //resetear atque en un tiempo
            yaAtaco = true;
            Invoke(nameof(ResetAttack), timeAttacks);
        }
    }

    public override void Patrullar()
    {
        //print("patrulla antes de buscando pnto encontrado:" + walkPointSet);
        if (!walkPointSet) BuscarWalkPoint();
        else
        {
            //print("se va al punto");
            agent.SetDestination(walkPoint);
        }

        Vector3 distance = transform.position - walkPoint;

        if (distance.magnitude < 1)
        {
            //print("llego a punto de patrulla");
            walkPointSet = false;
        }
    }

    public override void Perseguir()
    {
        agent.SetDestination(player.position);
    }

    //public override void Morir()
    //{
    //    GetComponent<Explodable>().explode();
    //    ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
    //    ef.doExplosion(transform.position);
    //    base.Morir();
    //}

}
