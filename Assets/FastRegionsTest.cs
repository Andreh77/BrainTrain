using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastRegionsTest : MonoBehaviour
{
    Vector2 dir;
    public float sdrr = 10f;
    bool tDir, bDir, lDir, rDir;
    public float offset = 0.5f;
    public ContactFilter2D me;
    float height;
    float width;
    void Start()
    {
        height = Camera.main.orthographicSize * 2;
        width = height * Camera.main.aspect;
        float aspect = (float)Screen.width / Screen.height;
        dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        transform.Translate(dir.normalized * sdrr * Time.deltaTime);
        if (transform.position.y > (height / 2) - offset && !tDir)
        {
            dir = ReflectVector(dir, new Vector2(0, 1));tDir = true;bDir = false; lDir = false;rDir = false;
        }
        if (transform.position.y < -(height / 2) + offset && !bDir)
        {
            dir = ReflectVector(dir, new Vector2(0, -1));tDir = false;bDir = true;lDir = false;rDir = false;
        }
        if (transform.position.x > (width / 2) - offset && !rDir)
        {
            dir = ReflectVector(dir, new Vector2(-1, 0));tDir = false;bDir = false;lDir = false;rDir = true;
        }
        if (transform.position.x < -(width / 2) + offset && !lDir)
        {
            dir = ReflectVector(dir, new Vector2(1, 0));tDir = false;bDir = false;lDir = true;rDir = false;
        }
    }

    Vector2 ReflectVector(Vector2 dir, Vector2 normal)
    {
        Vector2 d = dir; 
        Vector2 n = normal;
        Vector2 reflectVector = d - 2 * (d * n) * n;
        return reflectVector;
    }
}
