using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This script controls the gun model transform change. Ideally, this and WeaponZoom would be one class,
// however that proved to be a struggle for many hours until I learned the difference between 
// transform.position, and transform.localposition. For now, it stays

public class AimScript : MonoBehaviour
{
    [SerializeField] Canvas crosshair;

    //Vector3 aimDownSights = new Vector3(-.06f, -.045f, .4f); //The vector that makes it show correctly in unity fullscreen
    Vector3 aimDownSights = new Vector3(0f, -.07f, .4f); //The vector that makes it show correctly in a web build
    Vector3 hipFire = new Vector3(.158f, -.162f, .533f); 


    float aimSpeed = 100f;

    bool aimDownSightsToggle = false;

    private void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            if (aimDownSightsToggle == false)
            {
                aimDownSightsToggle = true;
                transform.localPosition = Vector3.Lerp(transform.localPosition, aimDownSights, aimSpeed * Time.deltaTime);
                crosshair.enabled = false;
            }
            else
            {
                aimDownSightsToggle = false;
                transform.localPosition = Vector3.Lerp(transform.localPosition, hipFire, aimSpeed * Time.deltaTime);
                crosshair.enabled = true;
            }
        }
        
    }
}
