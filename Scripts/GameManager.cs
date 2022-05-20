using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    AudioManager aud;

    private void Start()
    {
        aud = AudioManager.instance;
        Time.timeScale = 1;
        //AudioManager.instance.Play("Nivel1");
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
