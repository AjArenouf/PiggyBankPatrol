using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnimTest : MonoBehaviour
{
    public Animator animator;
    public Button uiButton;

    // Start is called before the first frame update
    void Start()
    {
        uiButton.onClick.AddListener(PlayAnimation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayAnimation()
    {
        animator.SetTrigger("PlayAnimation");
    }
}
