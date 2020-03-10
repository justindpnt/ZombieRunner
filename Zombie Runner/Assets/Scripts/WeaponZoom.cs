using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] RigidbodyFirstPersonController fpsController;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 45f;
    [SerializeField] float zoomOutSensitivity = 2f;
    [SerializeField] float zoomInSensitivity = .5f;

    [SerializeField] Canvas crosshair;

    //Vector3 aimDownSights = new Vector3(-.06f, -.045f, .4f); //The vector that makes it show correctly in unity fullscreen
    public Vector3 aimDownSights = new Vector3(0f, -.07f, .4f); //The vector that makes it show correctly in a web build
    public Vector3 hipFire = new Vector3(.158f, -.162f, .533f);
    float aimSpeed = 100f;

    bool aimDownSightsToggle = false;

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(1))
        {
            if (aimDownSightsToggle == false)
            {
                aimDownSightsToggle = true;
                fpsCamera.fieldOfView = zoomedInFOV;
                fpsController.mouseLook.XSensitivity = zoomInSensitivity;
                fpsController.mouseLook.YSensitivity = zoomInSensitivity;
                aimDownSightsToggle = true;
                transform.localPosition = Vector3.Lerp(transform.localPosition, aimDownSights, aimSpeed * Time.deltaTime);
                crosshair.enabled = false;
            }
            else
            {
                aimDownSightsToggle = false;
                fpsCamera.fieldOfView = zoomedOutFOV;
                fpsController.mouseLook.XSensitivity = zoomOutSensitivity;
                fpsController.mouseLook.YSensitivity = zoomOutSensitivity;
                aimDownSightsToggle = false;
                transform.localPosition = Vector3.Lerp(transform.localPosition, hipFire, aimSpeed * Time.deltaTime);
                crosshair.enabled = true;
            }
        } 
    }
}
