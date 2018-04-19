using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://forum.unity.com/threads/linerenderer-to-create-an-ellipse.74028/ by Eidor

[RequireComponent(typeof(LineRenderer))]
public class CircleRendererTest : MonoBehaviour {
    public enum Axis { X, Y, Z };
 
    [SerializeField]
    [Tooltip("The number of lines that will be used to draw the circle. The more lines, the more the circle will be \"flexible\".")]
    [Range(0, 1000)]
    private int _segments = 60;

    [SerializeField]
    [Tooltip("The number of degree that will be draw from.")]
    [Range(-360, 360)]
    private float _AngleFrom = 0;

    [SerializeField]
    [Tooltip("The number of degree that will be draw to.")]
    [Range(-360, 360)]
    private float _AngleTo = 360;

    [SerializeField]
    [Tooltip("The radius.")]
    private float _Radius = 10;

    [SerializeField]
    [Tooltip("Enable to use world coord")]
    private bool _isWorldCoord = false;

    [SerializeField]
    [Tooltip("The offset will be applied in the direction of the axis.")]
    private float _offset = 0;
 
    [SerializeField]
    [Tooltip("The axis about which the circle is drawn.")]
    private Axis _axis = Axis.Z;
 
    [SerializeField]
    [Tooltip("If checked, the circle will be rendered again each time one of the parameters change.")]
    private bool _checkValuesChanged = true;
 
    private int _previousSegmentsValue;
    private float _previousHorizRadiusValue;
    private float _previousVertRadiusValue;
    private float _previousOffsetValue;
    private Axis _previousAxisValue;
    private float _previousAngleFrom;
    private float _previousAngleTo;

    private LineRenderer _line;
 
    void Start()
    {
        _line = gameObject.GetComponent<LineRenderer>();
 
        _line.positionCount = _segments + 1;
        _line.useWorldSpace = _isWorldCoord;
 
        UpdateValuesChanged();
 
        CreatePoints();
    }
 
    void Update()
    {
        if (_checkValuesChanged)
        {
            if (_previousSegmentsValue != _segments ||
                _previousHorizRadiusValue != _Radius ||
                _previousOffsetValue != _offset ||
                _previousAxisValue != _axis ||
                _line.useWorldSpace != _isWorldCoord ||
                _previousAngleFrom!= _AngleFrom ||
                _previousAngleTo != _AngleTo
                )
            {
                CreatePoints();
            }
 
            UpdateValuesChanged();
        }
    }
 
    void UpdateValuesChanged()
    {
        _previousSegmentsValue = _segments;
        _previousHorizRadiusValue = _Radius;
        _previousOffsetValue = _offset;
        _previousAxisValue = _axis;
        _line.useWorldSpace = _isWorldCoord;
        _previousAngleFrom = _AngleFrom;
        _previousAngleTo = _AngleTo;
    }
 
    void CreatePoints()
    {
 
        if (_previousSegmentsValue != _segments)
        {
            _line.positionCount = _segments + 1;
        }
 
        float x;
        float y;
        float z = _offset;
 
        float angle = _AngleFrom;
 
        for (int i = 0; i < (_segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * _Radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * _Radius;
 
            switch(_axis)
            {
                case Axis.X: _line.SetPosition(i, new Vector3(z, y, x));
                    break;
                case Axis.Y: _line.SetPosition(i, new Vector3(y, z, x));
                    break;
                case Axis.Z: _line.SetPosition(i, new Vector3(x, y, z));
                    break;
                default:
                    break;
            }
 
            angle += ((_AngleTo-_AngleFrom) / _segments);
            Debug.Log("angle " + angle);
        }
    }
}
