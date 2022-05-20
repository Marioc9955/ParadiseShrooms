using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_movil : MonoBehaviour
{
    [SerializeField] List<Transform> wayPoints;
    private GameObject player;
    public float velocidad = 2;
    float distanciaCambio = 0.2f;
    byte siguientePos = 0;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
       
 
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.transform.SetParent(null);
        }
    }
}
