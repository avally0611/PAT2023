using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


//NOTE FOR MR B - it's a bit tricky to split this into backend and UI 
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

    private Players[] playersArr;
    private int arrSize = 0;
    private string passwordErrorMessage;

    private void Start()
    {
        for (int i = 0; i < arrSize; i++)
        {
            Debug.Log(playersArr[i]);
        }
    }

    private void Awake()
    {
        //Since this is a basic, personal game I think 100 as a size will be enough
         playersArr = new Players[100];
        

        loginButton.onClick.AddListener(LoginClick);
        signupButton.onClick.AddListener(SignupClick);

    }

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


    private void AddUser()
    {

        playersArr[arrSize] = new Players(usernameInputField.text, passwordInputField.text);
        arrSize++;

        usernameInputField.text = "";
        passwordInputField.text = "";
        errorText.text = string.Empty;

        FileHandler.SaveToJSON<Players>(playersArr, fileName);

        errorText.text = "Signup successful!\nWelcome";


    }

    //method searches for username and checks if password matches
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

    public static int GetCurrentPlayerID()
    {
        return currentPlayerID;
    }

    
    


   
}
