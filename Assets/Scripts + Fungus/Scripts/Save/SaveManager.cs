using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager
{

    // MÉTODOS DE SAVE
    // Puntos
    public static void SavePointsData(PointsManager pointsManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/points.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PointsData data = new PointsData(pointsManager);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    // Jugador
    public static void SavePlayerData(Movimiento movimiento)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(movimiento);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    // Interactables
    public static void SaveInteractableData(InteractablePoints interactablePoints)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/interactable.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        InteractableData data = new InteractableData(interactablePoints);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    // Inventario
    public static void SaveInventoryData(Inventory inventory)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/inventory.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        InventoryData data = new InventoryData(inventory);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    // Puzzles
    public static void SavePuzzlesData(Interactable interactable)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/puzzles.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PuzzlesData data = new PuzzlesData(interactable);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    // MÉTODOS DE LOAD
    // Puntos
    public static PointsData LoadPointsData()
    {
        string path = Application.persistentDataPath + "/points.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PointsData data = (PointsData)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        return null;
    }

    // Jugador
    public static PlayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/player.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = (PlayerData)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        return null;
    }
    // Interactables
    public static InteractableData LoadInteractableData()
    {
        string path = Application.persistentDataPath + "/interactable.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            InteractableData data = (InteractableData)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        return null;
    }
    // Inventario
    public static InventoryData LoadInventoryData()
    {
        string path = Application.persistentDataPath + "/inventory.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            InventoryData data = (InventoryData)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }  
        return null;
    }

    // Puzzles
    public static PuzzlesData LoadPuzzlesData()
    {
        string path = Application.persistentDataPath + "/puzzles.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PuzzlesData data = (PuzzlesData)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        return null;
    }

}
