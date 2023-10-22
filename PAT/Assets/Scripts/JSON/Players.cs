using System;

//this means the object can be saved to files in a specific format
[Serializable]
public class Players 
{
    public int ID;
    public string username;
    public string password;

    //belongs to class - not specific obj - we want to have a unique constant ID so it must not belong to object - This field is like autoimcrimenting in SQL
    private static int nextID = 0;

    //constructor method that takes in user's username and password
    public Players(string unm, string pw) 
    {
        ID = nextID;
        this.username = unm;
        this.password = pw;
        nextID++;
    }

    //there is a base class in unity which already has a ToString() - use override - for our class specifically
    public override string ToString()
    {
        return ID + "\n" + username + "\n" + password; 
    }
}
