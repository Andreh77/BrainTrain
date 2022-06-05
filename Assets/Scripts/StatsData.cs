using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsData
{
    public static List<float> flashCardsScore = new List<float>();
    public static List<float> reactionTimeScore = new List<float>();
    public static List<float> circleClickerTimeScore = new List<float>();
    public static List<float> speedClickerTimeScore = new List<float>();

    public static float Min(List<float> list)
    {
        float num;
        num =  (list.Count > 0) ? list[0] : 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (num > list[i]) num = list[i];
        }

        return num;
    }

    public static float Max(List<float> list)
    {
        float num;
        num = (list.Count > 0) ? list[0] : 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] > num) num = list[i];
        }

        return num;
    }

    public static string GetLastItems(List<float> List, int n, string unit = null)
    {
        string text = null;
        int index = 0;
        for (int i = Mathf.Max(0, List.Count - n); i < List.Count; i++)
        {
            index++;
            text += index + ". " + List[i] + unit +" \n";
        }

        return text;
    }
}
