using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGenerator : MonoBehaviour
{
    State state;

    public GameObject buttonPrefab;
    [SerializeField] GameObject buttonContainer;
    //GameObject buttonContainerChild;
    [SerializeField] Text buttonText;

    int buttonNum;

    public void Start()
    {
        //buttonText = button.GetComponentInChildren<Text>();
    }

    public void GenerateButtons(int optionNum)
    {
        //buttonNum = state.GetButtonsRequired();
        Instantiate(buttonPrefab, transform.position, transform.rotation, this.transform);

        //change the button text to the available user inputs
        var buttonContainerChild = buttonContainer.transform.GetChild(optionNum).gameObject;
        buttonText = buttonContainerChild.GetComponentInChildren<Text>();
        buttonText.text = (optionNum + 1).ToString();
    }

    public void DestroyButtons()
    {
        foreach (Transform child in buttonContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
