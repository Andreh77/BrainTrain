using UnityEngine;

public class GameModeTest : MonoBehaviour
{
    public Camera cam;
    float worldWidth;
    float worldHeight;
    Vector2 dir;
    public float speed = 10f;
    bool hasNewTopDir, hasNewBottomDir, hasNewLeftDir, hasNewRightDir;

    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        float aspect = (float)Screen.width / Screen.height;
        worldWidth = (cam.orthographicSize * 2);
        worldHeight = (worldHeight * aspect);

        //Debug.Log(worldHeight + " " + worldWidth);

        dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        transform.Translate(dir.normalized * speed * Time.deltaTime);
        if(transform.position.y > 5 - .5f && !hasNewTopDir)
        {
            dir = ReflectVector(dir, new Vector2(0, 1));
            hasNewTopDir = true;
            hasNewBottomDir = false;
            hasNewLeftDir = false;
            hasNewRightDir = false;
        }     
        else if(transform.position.y < -5 + .5f && !hasNewBottomDir)
        {
            dir = ReflectVector(dir, new Vector2(0, -1));
            hasNewTopDir = false;
            hasNewBottomDir = true;
            hasNewLeftDir = false;
            hasNewRightDir = false;
        }
        
        if(transform.position.x > 5 - .5f && !hasNewRightDir)
        {
            dir = ReflectVector(dir, new Vector2(-1, 0));
            hasNewTopDir = false;
            hasNewBottomDir = false;
            hasNewLeftDir = false;
            hasNewRightDir = true;
        }    
        else if(transform.position.x < -5 + .5f && !hasNewLeftDir)
        {
            dir = ReflectVector(dir, new Vector2(1, 0));
            hasNewTopDir = false;
            hasNewBottomDir = false;
            hasNewLeftDir = true;
            hasNewRightDir = false;
        }
    }

    Vector2 ReflectVector(Vector2 dir, Vector2 normal)
    {
        //r=d−2(d⋅n)n Equation to reflect a vector
        Vector2 d = dir; // current direction
        Vector2 n = normal; //screen edge normal
        Vector2 reflectVector = d-2 * (d*n) * n;

        return  reflectVector;       
    }
}