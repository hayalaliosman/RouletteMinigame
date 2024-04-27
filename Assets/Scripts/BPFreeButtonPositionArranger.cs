using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BPFreeButtonPositionArranger : MonoBehaviour
{
    [SerializeField] private RectTransform lowerLimit, upperLimit;
    
    private void Start()
    {
        ArrangePosition();
    }

    private void ArrangePosition()
    {
        // Aligning position between lowerLimit and upperLimit so that button is always between bottom of rewards and bottom of canvas. This is to avoid this object to stay on top of reward buttons in some resolutions such as iPads
        var rectTransform = GetComponent<RectTransform>();
        var lowerLimitPosition = lowerLimit.position;
        rectTransform.position = new Vector3(lowerLimitPosition.x, (lowerLimitPosition.y + upperLimit.position.y) * 0.5f, lowerLimitPosition.z);
    }
}
