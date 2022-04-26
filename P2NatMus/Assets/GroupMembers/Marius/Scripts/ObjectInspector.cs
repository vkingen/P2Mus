using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Source: //Source: https://forum.unity.com/threads/mobile-touch-to-orbit-pan-and-zoom-camera-without-fix-target-in-one-script.522607/

//THIS SCRIPT SHOULD BE PLACED ON THE MAIN CAMERA IN UNITY

public class ObjectInspector : MonoBehaviour
{
    public Transform target; //Target for orbit and zoom

    public float distance = 5.0f; //Distance from target if no target is assigned
    public float maxDistance = 20; //Max zoom out distance
    public float minDistance = 1f; //Max zoom in distance
    public float xSpeed = 40f; //x rotation speed
    public float ySpeed = 40f; //y rotation speed
    public int yMinLimit = -90; //y(vertical) rotation limitation
    public int yMaxLimit = 90; //y(vertical) rotation limitation
    public float zoomRate = 40f; //zoom speed
    public float zoomDampening = 5.0f; //Drag when zooming or rotating

    //public float panSpeed = 0.3f; Should maybe replace x and y speed in case of these values always being the same 

    private float _xDeg = 0.0f;
    private float _yDeg = 0.0f;
    private float _currentDistance;
    private float _desiredDistance;
    private Quaternion _currentRotation;
    private Quaternion _desiredRotation;
    private Quaternion _rotation;
    private Vector3 _position;

    void Start() 
    { 
        Init(); 
    }

    void OnEnable() 
    { 
        Init(); 
    }

    public void Init()
    {
        //If there is no target, create a temporary target at 'distance' from the cameras current viewpoint
        if (!target)
        {
            GameObject go = new GameObject("Cam Target");
            go.transform.position = transform.position + (transform.forward * distance);
            target = go.transform;
        }

        distance = Vector3.Distance(transform.position, target.position);
        _currentDistance = distance;
        _desiredDistance = distance;

        //be sure to grab the current rotations as starting points.
        _position = transform.position;
        _rotation = transform.rotation;
        _currentRotation = transform.rotation;
        _desiredRotation = transform.rotation;

        _xDeg = Vector3.Angle(Vector3.right, transform.right);
        _yDeg = Vector3.Angle(Vector3.up, transform.up);
    }

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

            _desiredDistance += deltaMagDiff * Time.deltaTime * zoomRate * 0.0025f * Mathf.Abs(_desiredDistance);
        }
        // If one or two fingers on the screen and they are moving - ORBIT!
        if (Input.touchCount == 1 || (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            Vector2 touchposition = Input.GetTouch(0).deltaPosition; //Gets position of touch movement
            _xDeg += touchposition.x * xSpeed * 0.002f; //x rotation
            _yDeg -= touchposition.y * ySpeed * 0.002f; //y rotation
            _yDeg = ClampAngle(_yDeg, yMinLimit, yMaxLimit); //Limits the y rotation

        }
        _desiredRotation = Quaternion.Euler(_yDeg, _xDeg, 0);
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
