using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10f;
    public float offset = 0.5f;

    private Vector2 dir;
    private float height;
    private float width;
    private bool hasNewTopDir, hasNewBottomDir, hasNewLeftDir, hasNewRightDir;

    void Start()
    {
        height = Camera.main.orthographicSize * 2;
        width = height * Camera.main.aspect;
        float aspect = (float)Screen.width / Screen.height;

        dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        if (transform.position.y > (height / 2) - offset && !hasNewTopDir)
        {
            dir = Vector2.Reflect(dir, new Vector2(0, 1));
            hasNewTopDir = true;
            hasNewBottomDir = false;
            hasNewLeftDir = false;
            hasNewRightDir = false;
        }
        else if (transform.position.y < -(height / 2) + offset && !hasNewBottomDir)
        {
            dir = Vector2.Reflect(dir, new Vector2(0, -1));
            hasNewTopDir = false;
            hasNewBottomDir = true;
            hasNewLeftDir = false;
            hasNewRightDir = false;
        }
        else if (transform.position.x > (width / 2) - offset && !hasNewRightDir)
        {
            dir = Vector2.Reflect(dir, new Vector2(-1, 0));
            hasNewTopDir = false;
            hasNewBottomDir = false;
            hasNewLeftDir = false;
            hasNewRightDir = true;
        }
        else if (transform.position.x < -(width / 2) + offset && !hasNewLeftDir)
        {
            dir = Vector2.Reflect(dir, new Vector2(1, 0));
            hasNewTopDir = false;
            hasNewBottomDir = false;
            hasNewLeftDir = true;
            hasNewRightDir = false;
        }

        transform.Translate(dir.normalized * speed * Time.deltaTime);
    }

    Vector2 ReflectVector(Vector2 dir, Vector2 normal)
    {
        //r=d−2(d⋅n)n Equation to reflect a vector
        Vector2 d = dir; // current direction
        Vector2 n = normal; //screen edge normal
        Vector2 reflectVector = d - 2 * (d * n) * n;

        return reflectVector;
    }
}