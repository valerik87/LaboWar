using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Assets.Scripts;

public class AttackAreaInfo : MonoBehaviour {

    public CircleCollider2D attackAreaCollider;
    public CircleRenderer circleRenderer;

    private float radius = 0;
    private List<KeyValuePair<GameObject, AttackParabola>> parabolasList;

    [SerializeField]
    private Material _attackMaterial = null;

    // Use this for initialization
    void Start ()
    {
        parabolasList = new List<KeyValuePair<GameObject, AttackParabola>>();
    }

    public void SetupRange(float NewRadius)
    {
        circleRenderer.checkValuesChanged = true;
        circleRenderer.radius = NewRadius;
        attackAreaCollider.radius = NewRadius;
        radius = NewRadius;
    }

    // Update is called once per frame
    void Update () {
        if(parabolasList.Count > 0)
        {
            foreach(KeyValuePair<GameObject, AttackParabola> target in parabolasList)
            {
                target.Value.Draw(this.gameObject, target.Key ,radius);
            }            
        }        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if(other.gameObject.CompareTag("Army"))
        {
            bool found = false;
            foreach (KeyValuePair<GameObject, AttackParabola> KV in parabolasList)
            {
                if (KV.Key == other.gameObject)
                {
                    found = true;
                }
            }

            if (!found)
            {
                AttackParabola attackParabola = SingletonAllocator<AttackParabola>.GetInstance().GetFromPool();
                parabolasList.Add(new KeyValuePair<GameObject, AttackParabola>(other.gameObject, attackParabola));

                attackParabola.Setup(this.gameObject.transform.position,other.gameObject.transform.position,_attackMaterial);
                attackParabola.EnableGameObject();
                attackParabola.Draw(this.gameObject, other.gameObject, radius);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        foreach(KeyValuePair<GameObject,AttackParabola> KV in parabolasList)
        {
            if(KV.Key == other.gameObject)
            {
                parabolasList.Remove(KV);
                KV.Value.DisableGameObject();
                SingletonAllocator<AttackParabola>.GetInstance().Free(KV.Value);
                break;
            }                
        } 
    }
    
}
