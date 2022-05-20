using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public LayerMask whatIsPlayer;
    public Transform player;

    //Atacar
    public float timeAttacks;

    public float rangoVision, rangoAtaque;

    [HideInInspector]
    public bool yaAtaco, playerInVision, playerInAtaque;

    public int maxHealth = 100;
    int currentHealth;

    [SerializeField] int collisionDamage;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    public virtual void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        playerInVision = Physics2D.OverlapCircle(transform.position, rangoVision, whatIsPlayer);
        playerInAtaque = Physics2D.OverlapCircle(transform.position, rangoAtaque, whatIsPlayer);

        if (!playerInVision && !playerInAtaque) Patrullar();
        if (playerInVision && !playerInAtaque) Perseguir();
        if (playerInAtaque && playerInVision) Atacar();

        //if (walkPointSet)
        //{
        //    transform.rotation = Quaternion.FromToRotation(Vector2.right, (walkPoint - transform.position).normalized);
        //}
    }

    public virtual void Patrullar()
    {
        //print("patrulla antes de buscando pnto encontrado:" + walkPointSet);
        //if (!walkPointSet) BuscarWalkPoint();
        //else
        //{
        //    print("se va al punto");
        //    agent.SetDestination(walkPoint);
        //}

        //Vector3 distance = transform.position - walkPoint;

        //if (distance.magnitude < 1)
        //{
        //    print("llego a punto de patrulla");
        //    walkPointSet = false;
        //}
    }

    public virtual void BuscarWalkPoint()
    {
        //walkPoint = Random.insideUnitCircle.normalized * rangoPatrulla;
        //if (!Physics2D.OverlapCircle(walkPoint, 1, whatIsGround)) walkPointSet = true;
    }

    public virtual void Perseguir()
    {
        //agent.SetDestination(player.position);
    }

    public virtual void Atacar()
    {
        //agent.SetDestination(transform.position);
        //if (!yaAtaco)
        //{
        //    //animacion que activa la explosion de esporas
        //    anim.SetTrigger("ataca");
        //    //resetear atque en un tiempo
        //    yaAtaco = true;
        //    Invoke(nameof(ResetAttack), timeAttacks);
        //}
    }

    public void ResetAttack()
    {
        yaAtaco = false;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Morir();
        }
    }

    public virtual void Morir()
    {
        Explodable e;
        if (TryGetComponent<Explodable>(out e))
        {
            e.explode();
            ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
            ef.doExplosion(transform.position);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //print("choca con jugador " + name);
            collision.gameObject.GetComponentInChildren<PlayerCombat>().TakeDamage(collisionDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoAtaque);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoVision);
    }
}
