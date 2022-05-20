using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espora : MonoBehaviour
{
    [SerializeField] private GameObject particulasExplosion;
    public float tiempoParaExplotar, tiempoExplosion;
    [SerializeField] float rangoExpl;
    [SerializeField] LayerMask whatIsPlayer;
    AudioManager audM;

    private void Start()
    {
        //Invoke(nameof(Explotar), tiempoExplosion);
        audM = AudioManager.instance;
        StartCoroutine(Explotar());
    }

    //private void Explotar()
    IEnumerator Explotar()
    {
        yield return new WaitForSecondsRealtime(tiempoParaExplotar);
        audM.Play("ExplosionEspora");
        Instantiate(particulasExplosion, transform.position, Quaternion.identity);
        tiempoExplosion += Time.time;
        while(tiempoExplosion > Time.time)
        {
            bool playerInExpl = Physics2D.OverlapCircle(transform.position, rangoExpl, whatIsPlayer);
            if (playerInExpl)
            {
                GameObject.Find("Player").GetComponentInChildren<PlayerCombat>().TakeDamage(5);
                break;
            }
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInChildren<PlayerCombat>().TakeDamage(1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoExpl);
    }
}
