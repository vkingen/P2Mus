using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationCanvas : MonoBehaviour
{
    public GameObject informationText;

    private int _touchCount = 0;

    private float _touchTime;

    

    private void Awake()
    {
        informationText.SetActive(false);
    }

    /*private void Update()
    {
        switch (_touchCount)
        {
            case 1:
                informationText.SetActive(true);
                break;

            case 2:
                informationText.SetActive(false);

                _touchCount = 0;
                break;
        }
    }*/

    public void OnTouch()
    {
        Touch touch = Input.touches[0];

        if (touch.phase == TouchPhase.Began)
        {
            _touchTime = Time.time​;
        }

        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            if (Time.time - _touchTime <= 0.5)
            {
                Debug.Log(_touchTime);
            }
        }
    }
}
