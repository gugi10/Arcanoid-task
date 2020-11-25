using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveData {

    public static void SaveGame(StoredData data) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/arkanoid_save.ms";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static StoredData LoadGame() {
        string path = Application.persistentDataPath + "/arkanoid_save.ms";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            StoredData data = (StoredData)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        else {
            return null;
        }
    }

    public static void SaveHighetScore(HighestScore data) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/highestScore.ms";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static HighestScore LoadScore() {
        string path = Application.persistentDataPath + "/highestScore.ms";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            HighestScore data = (HighestScore)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        else {
            HighestScore zeroScore = new HighestScore();
            zeroScore.highestScore = 0;
            return zeroScore;
        }
    }
}
