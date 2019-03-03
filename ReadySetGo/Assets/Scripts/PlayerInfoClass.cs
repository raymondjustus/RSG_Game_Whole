using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInfoClass {

    public static int applesPickedUp, correctAnswers;
    public static float fastestEasyTime, fastestHardTime, fastestMediumTime;
    public static bool hasPerfectEasyGame, hasPerfectHardGame, hasPerfectMediumGame;

    
    public static void CopyToCurrentProfile()
    {

        PlayerInfo.ApplesPicked = applesPickedUp;
        PlayerInfo.CorrAnswers = correctAnswers;
        PlayerInfo.EFastestTime = fastestEasyTime;
        PlayerInfo.HFastestTime = fastestHardTime;
        PlayerInfo.MFastestTime = fastestMediumTime;
        PlayerInfo.EPerfectGame = hasPerfectEasyGame;
        PlayerInfo.HPerfectGame = hasPerfectHardGame;
        PlayerInfo.MPerfectGame = hasPerfectMediumGame;

        // move things from the object created in CreateFromJSON to the static script
    }

    public static PlayerInfoClass CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<PlayerInfoClass>(jsonString);
    }

    public static void SendToCloud()
    {

        applesPickedUp = PlayerInfo.ApplesPicked;
        correctAnswers = PlayerInfo.CorrAnswers;
        fastestEasyTime = PlayerInfo.EFastestTime;
        fastestHardTime = PlayerInfo.HFastestTime;
        fastestMediumTime = PlayerInfo.MFastestTime;
        hasPerfectEasyGame = PlayerInfo.EPerfectGame;
        hasPerfectHardGame = PlayerInfo.HPerfectGame;
        hasPerfectMediumGame = PlayerInfo.MPerfectGame;



        //send to cloud code goes here
    }
}
