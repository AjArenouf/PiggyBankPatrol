using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfAffirmStringPersist : MonoBehaviour
{
    public GameObject newSelfAffirmation; 

    void OnEnable()
    {
        newSelfAffirmation.SetActive(true);
        Debug.Log("Home Awake");
    }
}
