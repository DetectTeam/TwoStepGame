using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ButtonPress
{
    public int TrialNumber;
    public float ProbabilityGreen;
    public float ProbabilityRed;
    public string Choice;
    public float TimeToPressFireButton;
    public string Transition;
    public string BallColour;
    public int Reward;
    public int HitTarget;
    public int TimeToHitTarget;
    public int NumTimesBouncedOffWall;
    public float DegreeMovementOfCannon;
    public string StartTimeOfButtonPress;
}

[System.Serializable]
public class Session 
{
   public string SessionID;
   public string UserID;
   public string TimeStamp;
   
   
   public int Level;
   public List<ButtonPress> ButtonPress = new List<ButtonPress>();
}
