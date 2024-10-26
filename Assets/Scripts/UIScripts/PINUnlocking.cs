using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PINUnlocking : MonoBehaviour
{
    public TextMeshProUGUI Ans;

    public void Number(int number)
    {
        Ans.text += number.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
