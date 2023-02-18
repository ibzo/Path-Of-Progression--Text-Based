using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureGame : MonoBehaviour
{
    [SerializeField] Text storyText;
    [SerializeField] State startingState;
    [SerializeField] State quittingState;

    State state;
    State previousState;
    bool isReturning;

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
        storyText.text = state.GetStateStory();

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
    }

    private void manageState()
    {
        //manages the quitting state
        if (Input.GetKeyDown(KeyCode.Q))
            state = quittingState;

        if (state == quittingState)
            Quit();

        //sets the next available states
        var nextStates = state.GetNextState();

        //sets the buttons on screen
        if (!buttonVisible)
        {
            for (int i = 0; i < nextStates.Length; i++)
            {
                buttonGen.GenerateButtons(i);
            }
            buttonVisible = true;
        }

        //manages other states
        if (!isReturning)
        {
            for (int i = 0; i < nextStates.Length; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i) || Input.GetKeyDown(KeyCode.Q))
                {
                    updateStateScreen(nextStates, i);
                    updateStats();
                    break;
                }
            }
        }
        else
        {
            isReturning = false;
        }

        //reset values if on the starting state
        if (state == startingState)
        {
            resetStats();
            updateStats();
            previousState = state;
        }

        storyText.text = state.GetStateStory();
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

    private void updateStateScreen(State[] nextStates, int i)
    {
        //set the state
        if (state == quittingState)
        {
            Debug.Log("Quitting");
        }
        else
        {
            Debug.Log("Not Quitting");

            state.SetUserInput(i + 1);
            state = nextStates[i];
            if (state != quittingState)
            {
                previousState = state;
                Debug.Log("Previous state = "+state.name);
            }
        }

        //remove buttons
        buttonGen.DestroyButtons();
        buttonVisible = false;
    }

    private void updateStats()
        {
            state.AddToCharacter(ref knowledge, ref charisma, ref wellbeing);
        
            //updateStatUI
            knowledgeUI.text = knowledge.ToString();
            charismaUI.text = charisma.ToString();
            wellbeingBar.value = wellbeing;
        }

    private void Quit()
    {       
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
            isReturning = true;
        }
    }

    private void resetStats()
    {
        knowledge = 0;
        charisma = 0;
        wellbeing = 0;
    }

    
}

