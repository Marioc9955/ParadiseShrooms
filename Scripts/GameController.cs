using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] Animator animPlayer;
    [SerializeField] Collider2D collPlayer;
    [SerializeField] TarodevController.PlayerController controllerPlayer;

    AudioManager aud;

    private void Start()
    {
        aud = AudioManager.instance;
    }

    public void GameOver()
    {
        aud.StopAll();
        aud.Play("GameOver");
        collPlayer.enabled = false;
        controllerPlayer.enabled = false;
        animPlayer.SetTrigger("muere");
        gameOverMenu.SetActive(true);
        Time.timeScale = 0.1f;
    }

    public void Reiniciar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SalirMenuPrincipal()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void CambiarVolumenMusica(float vol)
    {
        aud.SetVolumenMusica(vol);
    }

    public void CambiarVolumenSFX(float vol)
    {
        aud.SetVolumenSFX(vol);
    }
}
