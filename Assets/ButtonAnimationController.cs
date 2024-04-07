using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimationController : MonoBehaviour
{
    public GameObject animatedObject;
    public Animator animatorController;
    public string animationTriggerName;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(TriggerAnimation);
    }

    void TriggerAnimation()
    {
        // Check if the object and animator are assigned
        if (animatedObject != null && animatorController != null)
        {
            // Trigger the animation using the specified trigger parameter
            animatorController.SetTrigger(animationTriggerName);
        }
        else
        {
            Debug.LogError("Animated object or animator controller not assigned.");
        }
    }
}
