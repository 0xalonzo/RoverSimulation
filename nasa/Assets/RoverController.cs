using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoverController : MonoBehaviour
{
    public ArticulationBody[] wheels;
    public float targetSpeedKmh = 1.0f;

    public void Start()
    {
        UpdateVelocity();
    }

    private void UpdateVelocity()
    {
        // 1 = -46.3
        var speed = targetSpeedKmh * -46.3f;

        foreach (var wheel in wheels)
        {
            wheel.SetDriveTargetVelocity(ArticulationDriveAxis.X, speed);
        }
    }
}