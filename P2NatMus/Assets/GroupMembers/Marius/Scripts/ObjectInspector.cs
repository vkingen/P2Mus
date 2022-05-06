using System.Collections;
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
    public int yMinLimit = -90; //y(vertical) rotation limitation
    public int yMaxLimit = 90; //y(vertical) rotation limitation
    public float zoomRate = 40f; //zoom speed
    public float zoomDampening = 5.0f; //Drag when zooming or rotating
    public float fineTuning = 0.0025f; //0.0025f allows for finetuning of zoomRate and panSpeed
    public float panSpeed = 40f; //x and y rotation speed

    private float _xDeg = 0.0f;
    private float _yDeg = 0.0f;
    private float _currentDistance;
    private float _desiredDistance;
    private Quaternion _currentRotation;
    private Quaternion _desiredRotation;
    private Quaternion _rotation;
    private Vector3 _position;

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

            _desiredDistance += deltaMagDiff * Time.deltaTime * fineTuning * zoomRate * Mathf.Abs(_desiredDistance); 
        }

        // If one or two fingers on the screen and they are moving - ORBIT!
        if (Input.touchCount == 1 || (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            Vector2 touchposition = Input.GetTouch(0).deltaPosition; //Gets position of touch movement
            _xDeg += touchposition.x * panSpeed * fineTuning; //x rotation
            _yDeg -= touchposition.y * panSpeed * fineTuning; //y rotation
            _yDeg = ClampAngle(_yDeg, yMinLimit, yMaxLimit); //Limits the y rotation
        }

        _desiredRotation = Quaternion.Euler(_yDeg, _xDeg, 0); //0 because there's no rotation around the z axis
        _currentRotation = transform.rotation;
        _rotation = Quaternion.Lerp(_currentRotation, _desiredRotation, Time.deltaTime * zoomDampening);
        transform.rotation = _rotation;        

        ////////Orbit Position
        _desiredDistance = Mathf.Clamp(_desiredDistance, minDistance, maxDistance);
        _currentDistance = Mathf.Lerp(_currentDistance, _desiredDistance, Time.deltaTime * zoomDampening);

        _position = target.position - (_rotation * Vector3.forward * _currentDistance);

        transform.position = _position;
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

