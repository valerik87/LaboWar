using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AttackAreaInfo : MonoBehaviour {

    public CircleCollider2D attackAreaCollider;
    public CircleRenderer circleRenderer;

    private List<GameObject> targets; 
    // Use this for initialization
    void Start () {
        targets = new List<GameObject>();
    }

    // Update is called once per frame
    void Update () {
        if(targets.Count > 0)
        {
            foreach(GameObject target in targets)
            {
                BezierCurve(target);
            }            
        }        
    }

    public void SetupRange(float NewRadius)
    {
        circleRenderer.checkValuesChanged = true;
        circleRenderer.radius = NewRadius;
        attackAreaCollider.radius = NewRadius;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if(!targets.Contains(other.gameObject))
        {
            targets.Add(other.gameObject);
        }
    }

    void BezierCurve(GameObject other)
    {
        // Circompherence x2 + y2 + ax + by +c = 0
        Vector3 PositionZero = Vector3.zero - this.transform.position;
        Vector3 ZeroTarget = other.transform.position - Vector3.zero;

        Debug.DrawLine(this.transform.position, Vector3.zero, Color.red);
        Debug.DrawLine(Vector3.zero, other.transform.position, Color.green);
    }
}
