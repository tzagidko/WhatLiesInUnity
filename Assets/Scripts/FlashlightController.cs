using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [Header("Flashlight Settings")]
    public KeyCode toggleKey = KeyCode.F;  // Key to toggle flashlight
    public Light flashlight;                // Reference to the flashlight Light component
    public float batteryLife = 100f;        // Total battery life in seconds
    public float drainRate = 10f;           // Battery drain rate per second
    public bool isFlashlightOn = false;     // Initial state of the flashlight

    private float currentBatteryLife;

    void Start()
    {
        if (flashlight == null)
        {
            Debug.LogError("Flashlight is not assigned!");
            return;
        }

        flashlight.enabled = isFlashlightOn; // Set initial state
        currentBatteryLife = batteryLife;
    }

    void Update()
    {
        HandleFlashlightToggle();
        DrainBattery();
    }

    void HandleFlashlightToggle()
    {
        if (Input.GetKeyDown(toggleKey) && currentBatteryLife > 0)
        {
            isFlashlightOn = !isFlashlightOn;
            flashlight.enabled = isFlashlightOn;
        }
    }

    void DrainBattery()
    {
        if (isFlashlightOn && currentBatteryLife > 0)
        {
            currentBatteryLife -= drainRate * Time.deltaTime;

            // Turn off flashlight if battery is depleted
            if (currentBatteryLife <= 0)
            {
                currentBatteryLife = 0;
                isFlashlightOn = false;
                flashlight.enabled = false;
            }
        }
    }

    public void RechargeBattery(float amount)
    {
        currentBatteryLife = Mathf.Clamp(currentBatteryLife + amount, 0, batteryLife);
    }
}