﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ButtonPress
{
    public int Level { get; set; }
    public int TrialNumber{ get; set; }
    public float ProbabilityGreen { get; set; }
    public float ProbabilityRed { get; set; }
    public string Choice{ get; set; }
    public float TimeToPressFireButton { get; set; }
    public string Transition { get; set; }
    public string BallColour { get; set; }
    public int Reward { get; set; }
    public int HitTarget { get; set; }
    public int TimeToHitTaget{ get; set; }
    public int NumTimesBouncedOffWall { get; set; }
    public float DegreeMovementOfCannon { get; set; }
}

public class Session 
{
   public string UserId { get; set; }
   public string Timestamp { get; set; }
   public List<ButtonPress> ButtonPress = new List<ButtonPress>();
}