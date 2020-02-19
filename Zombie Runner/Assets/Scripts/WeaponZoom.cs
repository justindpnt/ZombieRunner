using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script controls the camera zoom on the scope in. Ideally, this and AimScript would be one class,
// however that proved to be a struggle for many hours until I learned the difference between 
// transform.position, and transform.localposition. For now, it stays

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 45f;

    bool aimDownSightsToggle = false;

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(1))
        {
            if (aimDownSightsToggle == false)
            {
                aimDownSightsToggle = true;
                fpsCamera.fieldOfView = zoomedInFOV;
            }
            else
            {
                aimDownSightsToggle = false;
                fpsCamera.fieldOfView = zoomedOutFOV;
            }
        } 
    }
}
