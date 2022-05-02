using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public string startScene, scanScene, runeInspectorScene, shieldInspectorScene, hornInspectorScene;

    public void LoadStartScene()
    {
        SceneManager.LoadScene(startScene);
    }

    public void LoadScanScene()
    {
        SceneManager.LoadScene(scanScene);
    }

    public void LoadRuneInspectorScene()
    {
        SceneManager.LoadScene(runeInspectorScene);
    }

    public void LoadShieldInspectorScene()
    {
        SceneManager.LoadScene(shieldInspectorScene);
    }

    public void LoadHornInspectorScene()
    {
        SceneManager.LoadScene(hornInspectorScene);
    }
}
