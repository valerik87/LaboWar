using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour {

    Camera Camera;
    public float SkyLevel;              //Z MAX bound
    public float TerrainLevel;          //Z MIN bound
    float CameraDistance;               //Z POSITION
    public float SkyPosition;       //Y MIN bound 
    public float TerrainPosition;   //Y MAX bound
    float WheelDirection;          //Y POSITION
    public float ScrollSpeed;


    public float RotationSpeedZAxis; //Rotation around z to simulate world rotate
    public float Radius;

    // Use this for initialization
    void Start () {
        Camera = gameObject.GetComponent<Camera>();
        CameraDistance = TerrainLevel;
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            //Zoom In&Out
            CameraDistance += -Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
            CameraDistance = Mathf.Clamp(CameraDistance, TerrainLevel, SkyLevel);
            Camera.orthographicSize = CameraDistance;


            Vector3 CenterHeading = Vector3.zero - this.transform.localPosition;
            CenterHeading.z = 0;
            float CenterDistance = CenterHeading.magnitude;
            Vector3 CenterDirection = CenterHeading / CenterDistance;
            Debug.Log("CenterHeading = " + CenterHeading);
            Debug.Log("CenterDistance = " + CenterDistance);
            Debug.Log("CenterDirection = " + CenterDirection);

            
            //YPosition
            WheelDirection = -Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
            if(WheelDirection > 0)
            {
                if (CenterDistance > SkyPosition)
                {
                    this.transform.Translate(CenterDirection * WheelDirection, Space.World);
                }
            }
            if (WheelDirection < 0)
            {
                if (CenterDistance < TerrainPosition)
                {
                    this.transform.Translate(CenterDirection * WheelDirection, Space.World);
                }
            }

            
        }

        ////Rotation
        if (Input.GetMouseButton(1))
        {
            if(Input.GetAxis("Mouse X") != 0)
            {
                float AxisSign = Mathf.Sign(Input.GetAxis("Mouse X"));
                Camera.transform.RotateAround(Vector3.zero, Vector3.back, AxisSign * RotationSpeedZAxis);
            }            
        }
    }
}
