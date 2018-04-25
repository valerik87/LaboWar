using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace Assets.Scripts
{
    class AttackParabola : IDisposable, ClassReferencer<AttackParabola>
    {
        private GameObject gameObject = null;
        private LineRenderer lineRenderer = null;
        private Vector3 origin,target;
        private int Segments = 10;
        
        public AttackParabola()
        {
            gameObject = null;
            lineRenderer = null;

            origin = Vector3.zero;
            target = Vector3.zero;
        }
        public void EnableGameObject()
        {
            this.gameObject.SetActive(true);
        }
        public void DisableGameObject()
        {
            this.gameObject.SetActive(false);
        }
        public void Dispose()
        {
            gameObject = null;
        }

        public AttackParabola GetClassReferencer()
        {
            return this;
        }

        public void Setup(Vector3 Start, Vector3 Target,Material RendererMaterial)
        {
            if(gameObject == null)
            {
                gameObject = new GameObject();
            }            
            gameObject.name = "AttackParabola";
            gameObject.transform.position = Vector3.zero;

            target = Target;
            origin = Start;

            if (lineRenderer == null )
            {
                lineRenderer = gameObject.AddComponent<LineRenderer>();
            }            
            lineRenderer.useWorldSpace = true;
            lineRenderer.positionCount = Segments;
            lineRenderer.sortingOrder = 5;
            lineRenderer.material = RendererMaterial;
        }

        public void Draw(GameObject Start, GameObject Target, float radius)
        {
            ParabolaTrajectoryTo(Start, Target, radius);
        }
        List<Vector3> ParabolaTrajectoryTo(GameObject start, GameObject target, float radius)
        {
            float x = 0;
            float cosx = 0;
            float y = 0;
            float angle = 0;
            //Target coord in local view
            Vector3 targetCoordInLocal = target.transform.InverseTransformPoint(start.transform.position);
            cosx = (targetCoordInLocal.x / 2) / radius;
            angle = Mathf.Acos(cosx);
            x = start.transform.localPosition.x + Mathf.Cos(Mathf.PI - angle) * radius;
            y = start.transform.localPosition.y + Mathf.Sin(Mathf.PI - angle) * radius;

            //Debug.Log("TargetCoordInLocal = " + targetCoordInLocal);
            //Debug.Log("cosx = " + cosx);
            //Debug.Log("angle = " + angle*Mathf.Rad2Deg);
            //Debug.Log("x = " + x);
            //Debug.Log("y = " + y);

            //Move in world coord
            Vector3 ParabolaVertexInLocalWorld = start.transform.TransformPoint(new Vector3(x, y, start.transform.localPosition.z));
            //Debug.Log("ParabolaVertexInLocalWorld " + ParabolaVertexInLocalWorld + " to obj " + other.name);

            Debug.DrawLine(start.transform.position, ParabolaVertexInLocalWorld, Color.red);
            Debug.DrawLine(ParabolaVertexInLocalWorld, target.transform.position, Color.green);

            Assert.IsFalse(lineRenderer == null);

            //with Parabola Vertex it's possible to make interpolations
            float i = 0.0f;
            int counter = 0;
            List<Vector3> points = new List<Vector3>();
            for (; i <= 1 && counter < Segments ; ++counter)
            {
                Vector3 TangentOriginPos = Vector3.Lerp(start.transform.position, ParabolaVertexInLocalWorld, i);
                Vector3 TangentTargetPos = Vector3.Lerp(ParabolaVertexInLocalWorld, target.transform.position, i);

                points.Add(Vector3.Lerp(TangentOriginPos, TangentTargetPos, i));
                lineRenderer.SetPosition(counter, points[counter]);
                i = i + (1.0f / Segments);
            }
            //TODO save the points and then use for make animation moving the material, for example: arrow starting and reaching the target
            lineRenderer.material.mainTextureOffset = new Vector2(lineRenderer.material.mainTextureOffset.x - Time.deltaTime, lineRenderer.material.mainTextureOffset.y);

            return points;
        }
    }
}

