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
    public static void SaveInteractableData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/interactable.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        InteractablePoints[] allInteractables = Object.FindObjectsOfType<InteractablePoints>();
        InteractableData data = new InteractableData(allInteractables);
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

    public static void SaveGlobal(
    PointsManager pointsManager,
    Movimiento movimiento,
    Inventory inventory,
    Interactable interactable,
    InteractablePoints interactablePoints)
    {
        SavePointsData(pointsManager);
        SavePlayerData(movimiento);
        SaveInventoryData(inventory);
        SavePuzzlesData(interactable);
        SaveInteractableData();  // Busca todos los interactables automáticamente
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

    // Restaurar estado de todos los interactables
    public static void RestoreAllInteractables(InteractableData data)
    {
        if (data == null) return;

        InteractablePoints[] allInteractables = Object.FindObjectsOfType<InteractablePoints>();
        
        foreach (InteractableStateData savedState in data.interactables)
        {
            foreach (InteractablePoints interactable in allInteractables)
            {
                if (interactable.gameObject.name == savedState.objectName)
                {
                    interactable.usado = savedState.usado;
                    break;
                }
            }
        }
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

    // LOAD GLOBAL
    public static void LoadGlobal(
    PointsManager pointsManager,
    Movimiento movimiento,
    Inventory inventory,
    Interactable interactable,
    InteractablePoints interactablePoints)
    {
        PointsData pointsData = LoadPointsData();
        if (pointsData != null)
            pointsManager.LoadFromData(pointsData);

        PlayerData playerData = LoadPlayerData();
        if (playerData != null)
            movimiento.LoadFromData(playerData);

        InventoryData inventoryData = LoadInventoryData();
        if (inventoryData != null)
            inventory.LoadFromData(inventoryData);

        PuzzlesData puzzlesData = LoadPuzzlesData();
        if (puzzlesData != null)
            interactable.LoadFromData(puzzlesData);

        InteractableData interactableData = LoadInteractableData();
        if (interactableData != null)
            RestoreAllInteractables(interactableData);
    }

}
