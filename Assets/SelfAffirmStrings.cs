using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelfAffirmStrings : MonoBehaviour
{
    public List<string> stringList;
    public Image imageComponent;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure image component is assigned
        if (imageComponent == null)
        {
            imageComponent = GetComponent<Image>();
        }
    }
}