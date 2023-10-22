using System;

//this means the object can be saved to files in a specific format
[Serializable]
public class Sessions
{
    public int ID;
    public DateTime startTime;
    public DateTime endTime;
    public int numRecipesCompleted;
    public int numIncorrectRecipes;
    public int sessionScore;

    //constructor method that takes in user's username and password
    public Sessions(int id, DateTime st, DateTime et, int numRecComp, int numIncompRec, int score)
    {
        this.ID = id;
        this.startTime = st;    
        this.endTime = et;
        this.numRecipesCompleted = numRecComp;
        this.numIncorrectRecipes = numIncompRec;
        this.sessionScore = score;
        
     
    }
    
    

   
}
