using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int municionActual;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
