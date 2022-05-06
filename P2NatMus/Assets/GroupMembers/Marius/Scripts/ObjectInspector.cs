﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Source: https://forum.unity.com/threads/mobile-touch-to-orbit-pan-and-zoom-camera-without-fix-target-in-one-script.522607/

//This script should be placed on the Main Camera.
public class ObjectInspector : MonoBehaviour
{
    public Transform target; //Target for orbit and zoom

    public float distance = 5.0f; //Distance from target if no target is assigned
    public float maxDistance = 20; //Max zoom out distance
    public float minDistance = 1f; //Max zoom in distance
    //public float xSpeed = 40f; //x rotation speed
    //public float ySpeed = 40f; //y rotation speed
    public int yMinLimit = -90; //y(vertical) rotation limitation
    public int yMaxLimit = 90; //y(vertical) rotation limitation
    public float zoomRate = 40f; //zoom speed
    public float zoomDampening = 5.0f; //Drag when zooming or rotating
    public float fineTuning = 0.0025f;
    public float panSpeed = 40f; //Should maybe replace x and y speed in case of these values always being the same 

    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float currentDistance;
    private float desiredDistance;
    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    private Quaternion rotation;
    private Vector3 position;


    void LateUpdate()
    {
        // If two fingers on the screen - ZOOM!
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPreviousPosition = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePreviousPosition = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPreviousPosition - touchOnePreviousPosition).magnitude;
            float TouchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagDiff = prevTouchDeltaMag - TouchDeltaMag;

            desiredDistance += deltaMagDiff * Time.deltaTime * fineTuning * zoomRate * Mathf.Abs(desiredDistance); //0.0025f allows for finetuning of zoomRate
        }

        // If one or two fingers on the screen and they are moving - ORBIT!
        if (Input.touchCount == 1 || (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            Vector2 touchposition = Input.GetTouch(0).deltaPosition; //Gets position of touch movement
            xDeg += touchposition.x * panSpeed * fineTuning; //x rotation xSpeed * 0.002f
            yDeg -= touchposition.y * panSpeed * fineTuning; //y rotation ySpeed * 0.002f
            yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit); //Limits the y rotation
        }
        desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);
        currentRotation = transform.rotation;
        rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * zoomDampening);
        transform.rotation = rotation;        

        ////////Orbit Position
        desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
        currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * zoomDampening);

        position = target.position - (rotation * Vector3.forward * currentDistance);

        transform.position = position;
    }
    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
