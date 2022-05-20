using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municion : MonoBehaviour
{
    SpriteRenderer sp;
    [SerializeField] float tiempoEnDesap;
    public bool sePuedeRecoger;
    [SerializeField] GameObject particleExplosion;

    // Start is called before the first frame update
    public virtual void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        sePuedeRecoger = false;
    }

    public IEnumerator Recoger()
    {
        yield return new WaitForSeconds(0.5f);
        sePuedeRecoger = true;
    }

    public IEnumerator Desaparecer()
    {
        float tiempoLimit = tiempoEnDesap + Time.time;
        while(tiempoLimit > Time.time)
        {
            sp.color -= new Color(0, 0, 0, Time.deltaTime * 1/tiempoEnDesap);
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && sePuedeRecoger)
        {
            Destroy(gameObject);
            PlayerStats.instance.munPiedras++;
        }
        if (collision.gameObject.CompareTag("Enemy") && enabled)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(10);
            Destruir();
        }
    }

    public virtual void Destruir()
    {
        Instantiate(particleExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
