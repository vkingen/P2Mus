using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAndHideObjects : MonoBehaviour
{
    ScanningManager scanningManager;
    public GameObject[] objects;
    private void Start()
    {
        scanningManager = FindObjectOfType<ScanningManager>();

        foreach (GameObject item in objects)
        {
            item.SetActive(false);
        }
        objects[scanningManager.currentScannedObject].SetActive(true);
    }
}
