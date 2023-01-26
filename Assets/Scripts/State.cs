using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State")]
public class State : ScriptableObject
{
    //UI
    [TextArea(10, 14)] [SerializeField] string storyText;
    [SerializeField] State[] nextStates;
    [SerializeField] int userInput;

    //Alternate State
    [SerializeField] State altState;
    [SerializeField] State stateDepend;
    [SerializeField] bool isAltState;
    [SerializeField] int altSwitch;

    //Character Stat AddOns
    [SerializeField] int knowledgeValue;
    [SerializeField] int charismaValue;
    [SerializeField] int wellbeingValue;

    //Buttons
    [SerializeField] int buttonsRequired;
    [SerializeField] string[] availableOptions;

    public void SetAvailableOptions()
    {
        
        Debug.Log("Setting available options...");
        
        for (int i = 0; i < nextStates.Length; i++)
        {
            Debug.Log("i = " + i);
            Debug.Log("nextStates Length = " + nextStates.Length);
            var option = i + 1;
            availableOptions[i] = option.ToString();
        }
        
    }
    
    public string GetAvailableOptions(int i)
    {
        for (int a = 0; a < nextStates.Length;  a++)
        {
            availableOptions[a] = (a + 1).ToString();
            Debug.Log("availableOptions["+a+"] = "+availableOptions[a]);
        }

        return availableOptions[i];
    }
    

    public int GetButtonsRequired()
    {
        buttonsRequired = nextStates.Length;
        return buttonsRequired;
    }

    public string GetStateStory()
    {
        return storyText;
    }

    public State[] GetNextState()
    {
        return nextStates;
    }
    
    public int GetUserInput()
    {
        return userInput;
    }
    
    public void SetUserInput(int input)
    {
        userInput = input;
    }

    public bool GetIsAltState()
    {
        return isAltState;
    }
    
    public State GetAltState()
    {
        return altState;
    }

    public int GetAltSwitch()
    {
        return altSwitch;
    }
    
    public State GetStateDepend()
    {
        return stateDepend;
    }
    
    public int GetStateDependInput()
    {
        return stateDepend.GetUserInput();
    }

    public void AddToCharacter(ref int knowledge, ref int charisma, ref int wellbeing)
    {
        knowledge += knowledgeValue;
        charisma += charismaValue;
        wellbeing += wellbeingValue;
    }

}
