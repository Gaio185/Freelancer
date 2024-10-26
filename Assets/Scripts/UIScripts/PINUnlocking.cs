using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PINUnlocking : MonoBehaviour
{
    public TextMeshProUGUI ans;
    public TextMeshProUGUI placeHolder;

    private string correctPIN = "1234";

    public void Number(int number)
    {
        if (placeHolder.text != "")
        {
            placeHolder.text = "";
        }

        if(ans.text.Length < 4)
        {
            ans.text += number.ToString();
        }else Debug.Log("PIN is 4 digits long");
    }

    public void Delete()
    {
        if(ans.text.Length > 0)
        {
            ans.text = ans.text.Remove(ans.text.Length - 1);
        }
    }

    public void Clear()
    {
        ans.text = "";
    }

    public void Enter()
    {
        if (ans.text == correctPIN)
        {
            Debug.Log("Correct PIN");
            Clear();
        }
        else
        { 
           Debug.Log("Incorrect PIN, try again");
            Clear();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
