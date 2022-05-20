using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int munPiedras;
    public Vector2 Checkpoint;
    public int vidaMax = 100;
    public int vidaActual;

    public bool videoInicialPlayed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SaveStats()
    {
        SaveSystem.SaveGame(instance);
    }

    public bool LoadStats()
    {
        DatosJuego datos = SaveSystem.LoadGame();

        if (datos != null)
        {
            Checkpoint = new Vector2(datos.checkpointActualX, datos.checkpointActualY);
            videoInicialPlayed = datos.videoInicialPlayed;
            munPiedras = datos.municionPiedra;

            instance = this;
        }
        else
        {
            videoInicialPlayed = false;
        }

        return datos != null;
    }

    [ContextMenu("Reinicar datos de juego")]
    public void DeteteStats()
    {
        SaveSystem.DeleteGame();
        instance.Checkpoint = new Vector2(600, 17);
    }

}
