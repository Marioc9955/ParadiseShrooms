using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    PlayerStats stats;
    public GameObject luz;
    
    private void Start()
    {
        stats = PlayerStats.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            luz.SetActive(true);
            stats.Checkpoint= transform.position;
            stats.SaveStats();
            AudioManager.instance.Play("Checkpoint");
        }
    }
}