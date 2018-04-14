using UnityEngine;
using System.Collections;

public class DebugCircle : MonoBehaviour
{
    public int segments;
    private float CenterX;
    private float CenterY;
    public float Radius;
    Vector3[] Coords;

    void Start()
    {
        if (segments > 2)
        {
            CenterX = transform.position.x;
            CenterY = transform.position.y;
            Coords = new Vector3[segments];
            CreatePoints();
        }
        else
        {
            Debug.Log("3 segments al least are required to draw a circle");
        }
    }

    void OnEnable()
    {
        Start();
    }


    void CreatePoints()
    {
        float x;
        float y;
        float z = 0f;

        float radians = 0;

        for (int i = 0; i < segments; i++)
        {
            x = CenterX + Mathf.Cos(radians) * Radius;
            y = CenterY + Mathf.Sin(radians) * Radius;

            Coords[i] = new Vector3(x, y, z);

            radians += ((2 * Mathf.PI) / segments);
        }
    }

    void Update()
    {
        if (segments > 2)
        {
            for (int i = 0; i < segments - 1; ++i)
            {
                Debug.DrawLine(Coords[i], Coords[i + 1], Color.red);
            }
            Debug.DrawLine(Coords[Coords.Length - 1], Coords[0], Color.red);
        }

    }
}