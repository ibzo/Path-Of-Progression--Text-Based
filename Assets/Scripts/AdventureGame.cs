using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureGame : MonoBehaviour
{
    [SerializeField] Text textComponent;
    [SerializeField] State startingState;
    [SerializeField] State quittingState;

    State state;
    State previousState;

    ButtonGenerator buttonGen;
    bool buttonVisible;

    //Character Stats
    int knowledge;
    int charisma;
    int wellbeing;
    [SerializeField] Text knowledgeUI;
    [SerializeField] Text charismaUI;
    [SerializeField] Slider wellbeingBar;

    // Start is called before the first frame update
    void Start()
    {
        //set the starting story state
        state = startingState;
        textComponent.text = state.GetStateStory();

        //button gen
        buttonVisible = false;
        buttonGen = GameObject.Find("ButtonGenerator").GetComponent<ButtonGenerator>();

        //set the original character values
        knowledge = 0;
        charisma = 0;
        wellbeing = 0;
    }

    // Update is called once per frame
    void Update()
    {
        manageState();
        altState();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            state = quittingState;
        }
    }

    private void manageState()
    {
        //sets the next available states and updates previous state
        var nextStates = state.GetNextState();

        //sets the buttons on screen
        if (!buttonVisible)
        {
            Debug.Log("No button available");
            //state.SetAvailableOptions();
            for (int i = 0; i < nextStates.Length; i++)
            {
                //Generate buttons
                //buttonGen.GenerateButtons(i);
            }

            buttonVisible = true;
            Debug.Log("Button available");
        }


        //reset values if on the starting state
        if (state == startingState)
        {
            Reset();
            updateStats();
            previousState = state;
        }

        //manages the quitting state
        if(state == quittingState)
        {
            Debug.Log("Quitting...");
            Quit();
        }

        //manages other states
        for (int i = 0; i < nextStates.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                state.SetUserInput(i + 1);
                state = nextStates[i];
                previousState = state;
                buttonVisible = false;
                updateStats();
            }
        }

        textComponent.text = state.GetStateStory();
    }

    private void Quit()
    {
        Debug.Log("Check input");
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Quit");
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif

            Application.Quit();
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Go back");
            state = previousState;
        }
    }

    private void Reset()
    {
        knowledge = 0;
        charisma = 0;
        wellbeing = 0;
    }

    private void altState()
    {
        if (state.GetIsAltState())
        {
            var dependInput = state.GetStateDependInput();

            if (dependInput == state.GetAltSwitch())
            {
                state = state.GetAltState();
            }
            
        }
    }

    private void updateStats()
    {
        state.AddToCharacter(ref knowledge, ref charisma, ref wellbeing);
        
        //updateStatUI
        knowledgeUI.text = knowledge.ToString();
        charismaUI.text = charisma.ToString();
        wellbeingBar.value = wellbeing;
    }
}

