using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectTouchInformation : MonoBehaviour
{
    public GameObject textBoxToToggle;

    private bool _isActive = false;


    private void Awake()
    {
        textBoxToToggle.SetActive(false);
    }
    public void ToggleInfoBox()
    {
        if(!_isActive)
        {
            textBoxToToggle.SetActive(true);
            _isActive = true;
        }
        else
        {
            textBoxToToggle.SetActive(false);
            _isActive = false;
        }
            
    }
}
