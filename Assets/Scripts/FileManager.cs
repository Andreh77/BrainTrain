using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class FileManager
{
    public static void SaveScores(List<Player> scores)
    {
        if (scores.Count == 0) return;

        foreach (Player p in scores)
        {
            Debug.Log("SAVING"  +p.name);
        }
        string path = "scores.dat";
        Debug.Log(path);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        FileStream fs = new FileStream(path, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, scores);
        fs.Close();
    }

    public static List<Player> ReadScores()
    {
        List<Player> scores = new List<Player>();
        string path = "scores.dat";
        if(File.Exists(path))
        {
            using (Stream stream = File.Open(path, FileMode.Open))
            {
                var bformatter = new BinaryFormatter();

                scores = (List<Player>)bformatter.Deserialize(stream);
            }
        }

        foreach(Player p in scores)
        {
            Debug.Log("READING" + p.name);
        }
        return scores;
    }
}