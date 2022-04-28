using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchInfo : MonoBehaviour
{
    public GameObject infoText;
    private float _touchCounter = 0;

    public void Awake()
    {
        infoText.SetActive(false);
    }


    private void Update()
    {
        switch (_touchCounter)
        {
            case 1:
                infoText.SetActive(true);
                break;

            case 2:
                infoText.SetActive(false);
                _touchCounter = 0;
                break;
        }
    }

    public void WhenButtonPressed()
    {
        
    }
}
