using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;



//NOTE FOR MR B - it's a bit tricky to split this into backend and UI 

//USES JSON to read and write user details
public class LoginManager : MonoBehaviour
{
    [SerializeField] private Button loginButton;
    [SerializeField] private Button signupButton;
    [SerializeField] private InputField usernameInputField;
    [SerializeField] private InputField passwordInputField;
    [SerializeField] private string fileName;
    [SerializeField] private TextMeshProUGUI errorText;
    
    //this field will be used in other classes such as statistics class
    private static int currentPlayerID;
    private static int autoIncrimentingID = 0;

    private Players[] playersArr = new Players[100];

    private string passwordErrorMessage;


    

    private void Awake()
    {   

        loginButton.onClick.AddListener(LoginClick);
        signupButton.onClick.AddListener(SignupClick);

    }

    //when login button clicked
    private void LoginClick()
    {
        if (CheckUser())
        {
            errorText.text = "Login successful!\nWelcome";
            Loader.Load(Loader.Scene.MainMenuScene);
        }
        else
        {
            errorText.text = "Login details are incorrect!";
            usernameInputField.text = "";
            passwordInputField.text = "";
        }

        
    }

    //when signup button clicked
    private void SignupClick()
    {
        if (IsStrongPassword())
        {
            AddUser();
            Loader.Load(Loader.Scene.MainMenuScene);
        }
        else
        {
            errorText.text = passwordErrorMessage;
            passwordInputField.text = "";
        }
        
    }

    //creates a new player object, gets existing objects and combines with new object in array, then parsed to SaveToJson method which creates a file and saves data there
    private void AddUser()
    {


        Players newPlayer = new Players(autoIncrimentingID, usernameInputField.text, passwordInputField.text);
        autoIncrimentingID++;

        // Read existing player data from the JSON file
        Players[] existingPlayers = FileHandler.ReadFromJSON<Players>(fileName);


        // Create a new array to hold the combined data (including the new player)
        Players[] combinedPlayers;

        if (existingPlayers != null)
        {
            // Append the new player to the existing data
            combinedPlayers = new Players[existingPlayers.Length + 1];
            existingPlayers.CopyTo(combinedPlayers, 0);
            combinedPlayers[existingPlayers.Length] = newPlayer;
        }
        else
        {
            // If there is no existing data, create a new array with just the new player
            combinedPlayers = new Players[] { newPlayer };
        }

        // Save the combined data to the JSON file
        FileHandler.SaveToJSON<Players>(combinedPlayers, fileName);

        usernameInputField.text = "";
        passwordInputField.text = "";
        errorText.text = string.Empty;

        errorText.text = "Signup successful!\nWelcome";





    }

    //method searches for username within JSON file and checks if password matches the one from JSON file
    public bool CheckUser()
    {
        bool correctUserDetails = false;

 
        string playerUsername = usernameInputField.text;
        string playerPassword = passwordInputField.text;

        playersArr = FileHandler.ReadFromJSON<Players>(fileName);

        if (playersArr != null)
        {
            //I dont use arrCount here because this is the array that we receive from the JSON files
            for (int i = 0; i < playersArr.Length; i++)
            {
                if (playersArr[i].username == playerUsername)
                {
                    //this is saved for other classes that will need it
                    currentPlayerID = playersArr[i].ID;

                    if (playersArr[i].password == playerPassword) 
                    { 
                        correctUserDetails = true; 
                        break;
                    }
                }
            }

        }



        return correctUserDetails;


    }

    //checks if password is strong and returns a bool - also sets a class string to a specific text based on what the password is missing 
    private bool IsStrongPassword()
    {
        string pass = passwordInputField.text;

        bool IsStrongPass = true;

  
        bool isUpper = false;
        bool isLower = false;
        bool hasNumber = false;
        bool hasSpecial = false;

        if (pass.Length >= 8)
        {

            for (int i = 0; i < pass.Length; i++)
            {
                //unity doesn't use charAt
                char let = pass[i];

                if (char.IsUpper(let))
                {
                    isUpper = true;
                }
                else if (char.IsLower(let))
                {
                    isLower = true;
                }
                else if (char.IsNumber(let))
                {
                    hasNumber = true;
                }
                else if ((let == '!' || let == '@' || let == '#' || let == '$' || let == '%' || let == '&' || let == '*') && !char.IsWhiteSpace(let))
                {
                    hasSpecial = true;
                }

            }
        }
        else
        {
            passwordErrorMessage = "Your password be 8 lettters or more";
            return false;
            
        }

        if (isUpper && isLower && hasNumber && hasSpecial)
        {
            
            IsStrongPass = true;
        }
        else if (!isUpper)
        {
            passwordErrorMessage = "Your password must have a capital letter";
            IsStrongPass = false;
        }
        else if (!isLower)
        {
            passwordErrorMessage = "Your password must have a lowercase letter";
            IsStrongPass = false;
        }
        else if (!hasNumber)
        {
            passwordErrorMessage = "Your password must have a number";
            IsStrongPass = false;
        }
        else if (!hasSpecial)
        {
            passwordErrorMessage = "Your password must have a special letter (!,@, #, $, %, &, *)";
            IsStrongPass = false;
        }

        return IsStrongPass;


    }

    //gets current ID - used in other screens - returns int
    public static int GetCurrentPlayerID()
    {
        return currentPlayerID;
    }

    
    


   
}
