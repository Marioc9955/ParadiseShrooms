using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGame(PlayerStats stats)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saveGame.fun";
        FileStream file = new FileStream(path, FileMode.Create);

        DatosJuego datos = new DatosJuego(stats);

        formatter.Serialize(file, datos);

        file.Close();
    }

    public static DatosJuego LoadGame()
    {
        string path = Application.persistentDataPath + "/saveGame.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = new FileStream(path, FileMode.Open);

            DatosJuego datos = formatter.Deserialize(file) as DatosJuego;
            file.Close();

            return datos;
        }
        else
        {
            return null;
        }
    }
    public static void DeleteGame()
    {
        string path = Application.persistentDataPath + "/saveGame.fun";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
