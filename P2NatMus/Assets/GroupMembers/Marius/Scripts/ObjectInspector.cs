using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float currentDistance;
    private float desiredDistance;
    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    private Quaternion rotation;
    private Vector3 position;

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
        currentDistance = distance;
        desiredDistance = distance;

        //be sure to grab the current rotations as starting points.
        position = transform.position;
        rotation = transform.rotation;
        currentRotation = transform.rotation;
        desiredRotation = transform.rotation;

        xDeg = Vector3.Angle(Vector3.right, transform.right);
        yDeg = Vector3.Angle(Vector3.up, transform.up);
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

            desiredDistance += deltaMagDiff * Time.deltaTime * zoomRate * 0.0025f * Mathf.Abs(desiredDistance);
        }
        // If one or two fingers on the screen and they are moving - ORBIT!
        if (Input.touchCount == 1 || (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved))
        {
            Vector2 touchposition = Input.GetTouch(0).deltaPosition; //Gets position of touch movement
            xDeg += touchposition.x * xSpeed * 0.002f; //x rotation
            yDeg -= touchposition.y * ySpeed * 0.002f; //y rotation
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
