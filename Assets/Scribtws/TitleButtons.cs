using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class TitleButtons : MonoBehaviour
{
    public Image[] buttons;
    public int currentButton;
    int buttonCount = 2;
    bool canMove = true;

    public GameObject beanut;

    private void Start()
    {
        beanut.transform.localPosition = buttons[currentButton].gameObject.transform.localPosition + new Vector3(450, 0, 0);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].color = Color.gray;
        }
        buttons[currentButton].color = Color.white;
    }

    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        if (Mathf.Abs(vertical) > .1f && canMove)
        {
            canMove = false;

            buttons[currentButton].color = Color.grey;
            int button = vertical > 0 ? currentButton-- : currentButton++;
            
            if (currentButton > buttonCount)
            {
                currentButton = buttonCount;
            }
            if (currentButton < 0)
            {
                currentButton = 0;
            }

            beanut.transform.localPosition = buttons[currentButton].gameObject.transform.localPosition + new Vector3(450, 0, 0);
            buttons[currentButton].color = Color.white;
        }
        else if (vertical == 0)
        {
            canMove = true;
        }

        if (Input.GetButtonDown("Submit"))
        {
            switch (currentButton)
            {
                case 0: SceneManager.LoadScene("1Player");
                    break;
                case 1: SceneManager.LoadScene("2Player");
                    break;
                case 2: Application.Quit();
                    break;
            }
        }
    }
}
