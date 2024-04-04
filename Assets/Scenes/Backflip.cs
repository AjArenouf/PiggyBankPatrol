using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Backflip : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>(); // Corrected line
        button.onClick.AddListener(PlayAnimation);
    }

    void PlayAnimation()
    {
        animator.SetTrigger("Jump!");
    }
}