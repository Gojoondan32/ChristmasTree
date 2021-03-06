using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChristmasTreeProgressState : int
{
    NotStarted = 0,
    InProgress,
    Paused,
    Complete,
    Failed,
    //New states here
    NumOfStates
}

public enum ChristmasTreeSize : int
{
    Small = 0,
    Large,
    //New sizes here
    NumOfSizes
}
public class ChristmasTreeOrder
{
    
    private float speed;
    private ChristmasTreeSize christmasTreeSize;
    private List<bool> decorationPlaced = new List<bool>();
    private int points;
    private List<DecorationType> decorationsRequired;
    private ChristmasTreeProgressState progress;
    private int orderDistancePercentage = 0; //holds the percentage of distance travelled between the spawn point and OOB.
    public int Points { get => points; }
    public List<DecorationType> DecorationsRequired { get => decorationsRequired; }
    public ChristmasTreeProgressState Progress { get => progress; }
    public ChristmasTreeSize ChristmasTreeSize { get => christmasTreeSize; }
    public float Speed { get => speed; }
    public int OrderDistancePercentage { get => orderDistancePercentage; set => orderDistancePercentage = value; }

    public ChristmasTreeOrder(List<DecorationType> decorationsRequired, float speed, ChristmasTreeSize christmasTreeSize )
    {
        this.christmasTreeSize = christmasTreeSize;
        this.decorationsRequired = decorationsRequired;
        this.speed = speed;
        points = 0;
        progress = ChristmasTreeProgressState.NotStarted;
        
        if(decorationsRequired != null)
        {
            //populate the decorationPlaced list based on number of decorations in decorationsRequired
            foreach (DecorationType decoType in decorationsRequired)
            {
                decorationPlaced.Add(false);
            }
        }
        else
        {
            Debug.LogError("Class:ChristmasTreeOrder Constructor: decorationsRequired is set to null");
        }
        
    }

    public bool CheckAndPlaceDecoration(int decorationIndex, int decorationPoints)
    {
        bool result = false;

            //check if hasn't been placed yet
            if (!CheckDecorationFulfilled(decorationIndex))
            {
                decorationPlaced[decorationIndex] = true;
                points += decorationPoints;
                result = true;
            }
 
        return result;
    }

    public bool CheckDecorationFulfilled(int decorationIndex)
    {
        bool result = false;

        if(decorationPlaced != null && decorationIndex < decorationPlaced.Count)
        {
            result = decorationPlaced[decorationIndex];
        }
        
        return result;
    }

    public bool CheckAllDecorationsFulfilled()
    {
        bool result = false;

        if (decorationPlaced != null)
        {
            result = true;
            foreach (bool placed in decorationPlaced)
            {
                result &= placed;
            }
        }

        return result;
    }
}
