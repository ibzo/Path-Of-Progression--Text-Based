using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGenerator : MonoBehaviour
{
    State state;

    public GameObject button;
    Text buttonText;

    int buttonNum;

    public void Start()
    {
        buttonText = button.GetComponentInChildren<Text>();
    }

    public void GenerateButtons(int i)
    {
        //buttonNum = state.GetButtonsRequired();
        Instantiate(button, transform.position, transform.rotation, this.transform);
        //change the button text to the available user inputs
        buttonText.text = state.GetAvailableOptions(i);
    }
}
