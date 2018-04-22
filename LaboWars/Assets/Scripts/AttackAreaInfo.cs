using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Assets.Scripts;

public class AttackAreaInfo : MonoBehaviour {

    public CircleCollider2D attackAreaCollider;
    public CircleRenderer circleRenderer;

    private float radius = 0;
    private List<GameObject> targets;

    //Not enough, i need to get an istance and use untill targe it's in area, then release. PoolAllocator should be a static singleton where to ask instance
    //of AttackObj
    private PoolAllocator<LineRenderer> lineRendererPool;
    
    // Use this for initialization
    void Start () {
        targets = new List<GameObject>();
        lineRendererPool = new PoolAllocator<LineRenderer>();
    }

    // Update is called once per frame
    void Update () {
        if(targets.Count > 0)
        {
            foreach(GameObject target in targets)
            {
                ParabolaTrajectoryTo(target);
            }            
        }        
    }

    public void SetupRange(float NewRadius)
    {
        circleRenderer.checkValuesChanged = true;
        circleRenderer.radius = NewRadius;
        attackAreaCollider.radius = NewRadius;
        radius = NewRadius;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if(!targets.Contains(other.gameObject))
        {
            targets.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (targets.Contains(other.gameObject))
        {
            targets.Remove(other.gameObject);
        }
    }

    void ParabolaTrajectoryTo(GameObject other)
    {
        float x = 0;
        float cosx = 0;
        float y = 0;
        float angle = 0;
        //Target coord in local view
        Vector3 targetCoordInLocal = other.transform.InverseTransformPoint(this.transform.position);
        cosx = (targetCoordInLocal.x/2) / radius;
        angle = Mathf.Acos(cosx);
        x = this.transform.localPosition.x + Mathf.Cos(Mathf.PI - angle) * radius;   
        y = this.transform.localPosition.y + Mathf.Sin(Mathf.PI - angle) * radius;
                                            
        //Debug.Log("TargetCoordInLocal = " + targetCoordInLocal);
        //Debug.Log("cosx = " + cosx);
        //Debug.Log("angle = " + angle*Mathf.Rad2Deg);
        //Debug.Log("x = " + x);
        //Debug.Log("y = " + y);
        
        //Move in world coord
        Vector3 ParabolaVertexInLocalWorld = this.transform.TransformPoint(new Vector3(x, y, this.transform.localPosition.z));
        //Debug.Log("ParabolaVertexInLocalWorld " + ParabolaVertexInLocalWorld + " to obj " + other.name);

        Vector3 PositionZero = ParabolaVertexInLocalWorld - this.transform.position;
        Vector3 ZeroTarget = other.transform.position - ParabolaVertexInLocalWorld;

        Debug.DrawLine(this.transform.position, ParabolaVertexInLocalWorld, Color.red);
        Debug.DrawLine(ParabolaVertexInLocalWorld, other.transform.position, Color.green);

        LineRenderer linerenderer = this.gameObject.GetComponent<LineRenderer>();
        Assert.IsTrue(linerenderer == null);
    }
}
