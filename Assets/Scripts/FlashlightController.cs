using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightController : MonoBehaviour
{
    [Header("Flashlight Settings")]
    public KeyCode toggleKey = KeyCode.F;  
    public Light flashlight;                
    public float batteryLife = 100f;        
    public float drainRate = 7f;           
    public bool isFlashlightOn = false;     

    private float currentBatteryLife;
     public Scrollbar flashlightScrollbar;  

    void Start()
    {
        if (flashlight == null)
        {
            Debug.LogError("Flashlight is not assigned!");
            return;
        }

        flashlight.enabled = isFlashlightOn; 
        currentBatteryLife = batteryLife;
    }
    private void UpdateFlashBar()
    {
        if (flashlightScrollbar != null)
        {
            flashlightScrollbar.size = (float)currentBatteryLife/batteryLife;
        }
    }

    void Update()
    {
        HandleFlashlightToggle();
        DrainBattery();
        UpdateFlashBar();
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