using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum TipoAtaque
{
    Corto,
    Distancia
}

public class PlayerCombat : MonoBehaviour
{
    KeyCode shortAttackKey = KeyCode.X, rangeAttackKey = KeyCode.C;

    public Animator animPlayer, animatorArmaCorta;

    [SerializeField] private Transform attackPoint, armaCortaPos, armaLargaPos, direccionView;
    [SerializeField] private float attackRange = 0.75f;
    [SerializeField] private float shortAttackRate = 2, rangeAttackRate;

    [SerializeField] private LayerMask enemyLayers;
    float nextShortAttackTime = 0;
    float nextRangeAttackTime = 0;

    TipoAtaque tp;
    PlayerStats stats;

    public GameObject municionPrefab, armaCortaPrefab;
    GameObject municionActual;
    public float fuerzaDisparo;

    public SpriteRenderer sp;

    public GameObject player;

    AudioManager aud;

    [SerializeField] TarodevController.PlayerController controller;

    [SerializeField] Transform vidaFlama;

    [SerializeField] GameObject vCam;

    private void Start()
    {
        stats = PlayerStats.instance;
        //stats.municionActual = 50;
        stats.vidaActual = stats.vidaMax;
        aud = AudioManager.instance;
        //controller = GetComponent<TarodevController.PlayerController>();

        if (stats.munPiedras > 0)
        {
            // CambiarArma(municionPrefab);
            municionActual = Instantiate(municionPrefab, armaLargaPos);
            municionActual.transform.SetAsFirstSibling();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextShortAttackTime && Input.GetKeyDown(shortAttackKey))
        {
            tp = TipoAtaque.Corto;
            //CambiarArma(armaCortaPrefab);
            //AttackCorto();
            
            animPlayer.SetTrigger("ShortAttack");
            nextShortAttackTime = Time.time + 1 / shortAttackRate;
        }
        if (Time.time >= nextRangeAttackTime && Input.GetKeyDown(rangeAttackKey))
        {
            tp = TipoAtaque.Distancia;

            //CambiarArma(municionPrefab);
            if (stats.munPiedras > 0 )
            {
                animPlayer.SetTrigger("RangeAttack");
                //StartCoroutine(Atacando());
                controller.atacando = true;
                controller.Velocity = Vector3.zero;
            }

            //AtaqueDistancia();
            nextRangeAttackTime = Time.time + 1 / rangeAttackRate;
        }
    }

    void CambiarArma(GameObject armaNueva)
    {
        if (armaCortaPos.childCount > 1)
        {
            Destroy(armaCortaPos.GetChild(0).gameObject);
        }
        switch (tp)
        {
            case TipoAtaque.Corto:
                GameObject armaCorta = Instantiate(armaCortaPrefab, armaCortaPos);
                armaCorta.transform.SetAsFirstSibling();
                animatorArmaCorta = armaCorta.GetComponent<Animator>();
                break;
            case TipoAtaque.Distancia:
                if (stats.munPiedras > 0)
                {
                    municionActual = Instantiate(municionPrefab, armaCortaPos);
                    municionActual.transform.SetAsFirstSibling();
                }
                break;
        }
    }

    void AttackCorto()
    {
        animatorArmaCorta.SetTrigger("attack");
        aud.Play("Whoosh");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        stats.vidaActual -= damage;
        aud.Play("DamagePlayer");
        if (stats.vidaActual > 0)
        {
            vidaFlama.localScale = new Vector3((float)stats.vidaActual / 100f, (float)stats.vidaActual / 100f, 1);
        }
        else
        {
            GameObject.Find("GameController").GetComponent<GameController>().GameOver();
            sp.enabled = false;
        }
        
        
        if (!gettingHurt)
        {
            StartCoroutine(GetHurt());
        }
    }

    bool gettingHurt = false;
    private IEnumerator GetHurt()
    {
        var noise = vCam.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = 2;
        noise.m_FrequencyGain = 2;
        gettingHurt = true;
        float gb = 1;
        while (gb > 0.01f)
        {
            sp.color = new Color(1, gb, gb);
            gb -= 0.02f;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.25f);
        sp.color = new Color(1, 1, 1);
        gettingHurt = false;
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;
    }

    void AtaqueDistancia()
    {
        //verificar si tiene por lo menos uno de municion
        if (stats.munPiedras > 0)
        {
            //disparar
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), municionActual.GetComponent<Collider2D>());
            municionActual.transform.parent = null;
            municionActual.transform.rotation = Quaternion.identity;
            municionActual.GetComponent<Piedra>().enabled = true;
            Rigidbody2D rbAmmo = municionActual.GetComponent<Rigidbody2D>();
            rbAmmo.gravityScale = 1;
            rbAmmo.constraints = RigidbodyConstraints2D.None;
            rbAmmo.velocity += new Vector2(controller.Velocity.x, controller.Velocity.y) + new Vector2(fuerzaDisparo * direccionView.localScale.x, 0);
            
            municionActual = null;
            stats.munPiedras--;
        }
        //Equipar municion si todavia tiene
        if (stats.munPiedras > 0)
        {
            // CambiarArma(municionPrefab);
            municionActual = Instantiate(municionPrefab, armaLargaPos);
            municionActual.transform.SetAsFirstSibling();
        }
        StartCoroutine(DejarDeAtacar());
    }

    IEnumerator DejarDeAtacar()
    {
        //controller.atacando = true;
        //controller.Velocity = Vector3.zero;
        yield return new WaitForSeconds(0.0420f);
        controller.atacando = false;
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
