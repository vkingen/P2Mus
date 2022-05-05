using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] // make tooltip
    private bool _animateOnStart = false;

    [SerializeField] // make tooltip
    private float _delaySpeed = 1.5f;

    public GameObject youFoundAnObjectInfoText;

    private void Awake()
    {
        if(youFoundAnObjectInfoText)
            youFoundAnObjectInfoText.SetActive(false);
        _animator = GetComponent<Animator>();
        if (_animateOnStart)
            ResetAnimation();
    }
   
    public void PlayAnimation()
    {
        _animator.SetBool("IsOpen", true);
        StartCoroutine(StartSceneWithDelay());
    }
    public void ResetAnimation()
    {
        _animator.SetBool("IsClose", true);
    }
    public void EnableFoundObjectInfoText()
    {
        if(youFoundAnObjectInfoText != null)
            youFoundAnObjectInfoText.SetActive(true);
    }

    IEnumerator StartSceneWithDelay()
    {
        yield return new WaitForSeconds(_delaySpeed);
        SceneManager.LoadScene("InspectorScene");
    }
}
