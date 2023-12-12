using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class TitleButtons : MonoBehaviour
{
    public Image[] buttons;
    public int currentButton;
    int buttonCount = 2;
    bool canMove = true;

    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        if (Mathf.Abs(vertical) > .1f && canMove)
        {
            canMove = false;
            int button = vertical > 0 ? currentButton-- : currentButton++;

            if (currentButton > buttonCount)
            {
                currentButton = buttonCount;
            }
            if (currentButton < 0)
            {
                currentButton = 0;
            }
        }
        else if (vertical == 0)
        {
            canMove = true;
        }
    }
}
