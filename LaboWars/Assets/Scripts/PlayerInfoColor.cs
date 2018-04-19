using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoColor : MonoBehaviour {

    public PlayerInfo  PlayerInfo;
    public SpriteRenderer Sprite;

    void Awake()
    {
        PlayerInfo = this.gameObject.GetComponentInParent<PlayerInfo>();
        if(PlayerInfo == null)
        {
            Debug.Log("Missing PlayerInfo!!!!");
        }
        
        if(Sprite == null)
        {
            Debug.Log("Sprite Missing!");
        }
    }

    // Use this for initialization
    void Start () {
        Sprite.color = PlayerInfo.Color;
	}
}
