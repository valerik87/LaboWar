using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

    [SerializeField]
    [Tooltip("Color of the player")]
    public Color Color = Color.white;

    [SerializeField]
    [Tooltip("Name of the player")]
    public string PlayerName = "Battezzami cazzo!!";

    [SerializeField]
    [Tooltip("GameObjectOfCity")]
    private GameObject GOCity = null;

    void Awake ()
    {
    }


    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
