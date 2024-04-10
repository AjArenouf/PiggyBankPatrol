using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnabledandDisabled : MonoBehaviour
{
    public GameObject gameobject;

    public void Trigger()
    {
        if (!gameobject.activeInHierarchy)
        {
            gameobject.SetActive(true);
        }
        else
        {
            gameobject.SetActive(false);
        }
    }
}

