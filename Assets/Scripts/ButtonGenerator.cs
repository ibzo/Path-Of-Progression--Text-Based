using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGenerator : MonoBehaviour
{
    State state;

    public GameObject buttonPrefab;
    [SerializeField] GameObject buttonContainer;
    [SerializeField] Text buttonText;

    int buttonNum;

    public void Start()
    {
        //buttonText = button.GetComponentInChildren<Text>();
    }

    public void GenerateButtons(int i)
    {
        Debug.Log("Generating button");

        //buttonNum = state.GetButtonsRequired();
        Instantiate(buttonPrefab, transform.position, transform.rotation, this.transform);

        //change the button text to the available user inputs

        //buttonText = buttonContainer.GetComponentInChildren<Text>();
        //buttonText.text = "Good";
    }
}
