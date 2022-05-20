using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DatosJuego
{
    public float checkpointActualX, checkpointActualY;

    public int municionPiedra;

    public bool videoInicialPlayed;

    public DatosJuego(PlayerStats stats)
    {
        checkpointActualX = stats.Checkpoint.x;
        checkpointActualY = stats.Checkpoint.y;
        municionPiedra = stats.munPiedras;
        videoInicialPlayed = stats.videoInicialPlayed;
    }
}
