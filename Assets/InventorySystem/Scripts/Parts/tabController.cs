using System;
using System.Collections;
using System.Collections.Generic;
using Nova;
using UnityEngine;

public class TabController : MonoBehaviour
{
    /// <summary>
    /// this is the point that this tab indicator is trying to be at
    /// 0 is left
    /// 1 is middle
    /// 2 is right
    /// </summary>
    [SerializeField] public int targetIndex;
    
    /// <summary>
    /// this is an array of the 3 target positions
    /// </summary>
    [SerializeField] private Transform[] targetPositions;

    [SerializeField] private TextBlock tb;
    [SerializeField] private float targetOpacity = 1;
    private static readonly Color visible = new Color(1, 1, 1, 1);
    private static readonly Color hidden = new Color(1, 1, 1, 0);

    private void Start()
    {
        transform.position = targetPositions[targetIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        //clamp the index and then lerp to its position
        ClampIndex();
        transform.position = MyMath.SmoothLerp(transform.position, targetPositions[targetIndex].position, 25f);
        AdjustOpacity();
    }

    /// <summary>
    /// make sure to keep the target index in range
    /// </summary>
    void ClampIndex()
    {
        if (targetIndex < 0)
        {
            targetIndex = 2;
        }

        if (targetIndex > 2)
        {
            targetIndex = 0;
        }
    }

    void AdjustOpacity()
    {
        if (targetIndex is 1)
        {
            tb.Color = MyMath.SmoothLerp(tb.Color, visible, 25f);
        }
        else
        {
            tb.Color = MyMath.SmoothLerp(tb.Color, hidden, 25f);
        }
    }
}

