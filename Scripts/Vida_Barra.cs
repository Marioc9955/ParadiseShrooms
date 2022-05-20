using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Vida_Barra : MonoBehaviour
{
    public Image barraVida;
    public TextMeshProUGUI municion;
    PlayerStats stats;
    void Start()
    {
        stats = PlayerStats.instance;
    }

    
    void Update()
    {

        barraVida.fillAmount = (float) stats.vidaActual / stats.vidaMax;
        municion.text = stats.munPiedras.ToString();
    }
}
