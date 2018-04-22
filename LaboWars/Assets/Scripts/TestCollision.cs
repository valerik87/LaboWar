using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("collision " + other.collider.name);
    }

}
