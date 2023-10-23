using System;

//this means the object can be saved to files in a specific format
[Serializable]
public class Players
{
    public int ID;
    public string username;
    public string password;

  
    //constructor method that takes in user's username and password
    public Players(int id,string unm, string pw)
    {
        this.ID = id;
        this.username = unm;
        this.password = pw;
        
    }

}
