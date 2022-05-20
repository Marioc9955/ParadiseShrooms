using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject rata_sentada;
    [SerializeField] private GameObject rata_parada;
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text dialogo_texto;
    [SerializeField] private GameObject texto_T;
    [SerializeField, TextArea(4, 6)] private string[] dialogos;


    private bool inicioDialogo;
    private bool jugadorCerca;
    private int numDialogo;
    private float tiempo_esribir=0.07f;
    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.T))
        {
            if (!inicioDialogo)
            {
                InicioDialogo();
            }
            else if (dialogo_texto.text == dialogos[numDialogo])
            {
                SiguienteDialogo();
            }
            else
            {
                StopAllCoroutines();
                dialogo_texto.text = dialogos[numDialogo];

            }
        }
    }
    private void SiguienteDialogo()
    {
        numDialogo++;
        if (numDialogo < dialogos.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            inicioDialogo = false;
            panel.SetActive(false);
        }
    }
    private void InicioDialogo()
    {
        inicioDialogo = true;
        panel.SetActive(true);
        numDialogo = 0;
        StartCoroutine(ShowLine());
    }

    private IEnumerator ShowLine()
    {
        dialogo_texto.text = string.Empty;
        foreach(char ch in dialogos[numDialogo])
        {
            dialogo_texto.text += ch;
            yield return new WaitForSeconds(tiempo_esribir);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            texto_T.SetActive(true);
            rata_parada.SetActive(true);
            rata_sentada.SetActive(false);
            jugadorCerca = true;
            AudioManager.instance.Play("Rata");
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            texto_T.SetActive(false);
            rata_parada.SetActive(false);
            rata_sentada.SetActive(true);
            jugadorCerca = false;
        }
    }
}
