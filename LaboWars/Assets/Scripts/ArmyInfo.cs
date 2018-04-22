using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyInfo : MonoBehaviour {

    [SerializeField]
    [Tooltip("Range of Army")]
    [Range(1, 100)]
    float range = 10;

    [SerializeField]
    [Tooltip("If a CircleRenderer is attached force it to adapt to this range.")]
    bool forceCircleRendererRange = false;
    AttackAreaInfo attackAreaInfo  = null;

    private float previousRange;

    // Use this for initialization
    void Start () {
	}

    // Update is called once per frame
    void Update()
    {
        if (forceCircleRendererRange)
        {
            if( previousRange != range)
            {
                this.gameObject.GetComponentInChildren<AttackAreaInfo>().SetupRange(range);
            }

            UpdateValuesChanged();
            
        }
    }

    private void UpdateValuesChanged()
    {
        previousRange = range;
    }
}
