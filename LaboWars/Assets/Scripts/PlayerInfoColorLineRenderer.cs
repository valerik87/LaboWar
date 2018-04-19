using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoColorLineRenderer : MonoBehaviour
{

    public PlayerInfo PlayerInfo;
    public LineRenderer LineRenderer;

    void Awake()
    {
        PlayerInfo = this.gameObject.GetComponentInParent<PlayerInfo>();
        if (PlayerInfo == null)
        {
            Debug.Log("Missing PlayerInfo!!!!");
        }

        if (LineRenderer == null)
        {
            Debug.Log("Sprite Missing!");
        }
    }

    // Use this for initialization
    void Start()
    {
        LineRenderer.material.color = PlayerInfo.Color;
        LineRenderer.material.SetColor("_EmissionColor", PlayerInfo.Color);
    }
}
