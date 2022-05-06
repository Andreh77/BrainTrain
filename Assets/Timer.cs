using UnityEngine;

public class Timer
{
    private double startTime = 0;
    private double finishTime = 0;
    public double time;
    public bool running = false;
    
    public void Start()
    {
        startTime = Time.time;
        running = true;
    }

    public double Stop()
    {
        finishTime = Time.time;
        time = finishTime - startTime;
        running = false;

        return time;
    }
    public void Reset()
    {
        startTime = 0;
        finishTime = 0;
        time = 0;
    }
}