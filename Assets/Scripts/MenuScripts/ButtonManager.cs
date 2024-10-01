using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject text;
    public GameObject buttonVariant;


    void Update()
    {
        if(text.activeSelf)
        {
            this.gameObject.SetActive(false);
            buttonVariant.SetActive(true);
        }else 
        {
            this.gameObject.SetActive(true);
            buttonVariant.SetActive(false);
        }
    }
}
