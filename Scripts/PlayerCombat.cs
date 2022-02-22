using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoAtaque
{
    Corto,
    Distancia
}

public class PlayerCombat : MonoBehaviour
{
    KeyCode shortAttackKey = KeyCode.X, rangeAttackKey = KeyCode.C;

    [SerializeField] private Animator animatorArma;

    [SerializeField] private Transform attackPoint, armaPos;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackRate = 2;

    [SerializeField] private LayerMask enemyLayers;
    float nextAttackTime = 0;

    TipoAtaque tp;
    PlayerStats stats;

    public GameObject municionPrefab, armaCortaPrefab;
    GameObject municionActual;
    public float fuerzaDisparo;

    private void Start()
    {
        stats = PlayerStats.instance;
        stats.municionActual = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(shortAttackKey))
            {
                tp = TipoAtaque.Corto;
                CambiarArma(armaCortaPrefab);
                AttackCorto();
                nextAttackTime = Time.time + 1 / attackRate;
            }
            if (Input.GetKeyDown(rangeAttackKey))
            {
                tp = TipoAtaque.Distancia;
                CambiarArma(municionPrefab);
                AtaqueDistancia();
                nextAttackTime = Time.time + 1 / attackRate;
            }
        }
    }

    void CambiarArma(GameObject armaNueva)
    {
        if (armaPos.childCount > 1)
        {
            Destroy(armaPos.GetChild(0).gameObject);
        }
        switch (tp)
        {
            case TipoAtaque.Corto:
                Instantiate(armaCortaPrefab, armaPos).transform.SetAsFirstSibling();
                break;
            case TipoAtaque.Distancia:
                if (stats.municionActual > 0)
                {
                    municionActual = Instantiate(municionPrefab, armaPos);
                    municionActual.transform.SetAsFirstSibling();
                }
                break;
        }
    }

    void AttackCorto()
    {
        animatorArma.SetTrigger("attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(100);
        }
    }

    void AtaqueDistancia()
    {
        //verificar si tiene por lo menos uno de municion
        if (stats.municionActual > 0)
        {
            //disparar
            municionActual.transform.parent = null;
            Rigidbody2D rbAmmo = municionActual.GetComponent<Rigidbody2D>();
            rbAmmo.gravityScale = 1;
            rbAmmo.AddForce(new Vector2(fuerzaDisparo * armaPos.parent.localScale.x, fuerzaDisparo * 0.75f), ForceMode2D.Impulse);
            rbAmmo.constraints = RigidbodyConstraints2D.None;
            municionActual = null;
            stats.municionActual--;
        }
        //Equipar municion si todavia tiene
        if (stats.municionActual > 0)
        {
            CambiarArma(municionPrefab);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
