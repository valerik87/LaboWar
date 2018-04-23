using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class AttackParabola : IDisposable, ClassReferencer<AttackParabola>
    {
        private GameObject gameObject;
        private LineRenderer lineRenderer;
        private Vector3 origin,target;
        
        public AttackParabola()
        {
            gameObject = null;
            lineRenderer = null;

            origin = Vector3.zero;
            target = new Vector3(100, 100, 0);
        }

        public void Dispose()
        {
            gameObject = null;
        }

        public AttackParabola GetClassReferencer()
        {
            return this;
        }

        public void Setup()
        {
            gameObject = new GameObject();
            gameObject.name = "AttackParabola";
            gameObject.transform.position = Vector3.zero;

            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.useWorldSpace = true;
            lineRenderer.positionCount = 2;
            lineRenderer.sortingOrder = 5;
        }

        public void Draw()
        {
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, target);
        }
    }
}

