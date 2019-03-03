public static class PlayerInfo
{
    private static int racesRan, applesPicked, corrAnswers;
    private static float eFastestTime, hFastestTime, mFastestTime;
    private static bool ePerfectGame, hPerfectGame, mPerfectGame;

    public static int RacesRan
    {
        get
        {
            return racesRan;
        }
        set
        {
            racesRan = value;
        }
    }

    public static int ApplesPicked
    {
        get
        {
            return applesPicked;
        }
        set
        {
            applesPicked = value;
        }
    }

    public static int CorrAnswers
    {
        get
        {
            return corrAnswers;
        }
        set
        {
            corrAnswers = value;
        }
    }

    public static float EFastestTime
    {
        get
        {
            return eFastestTime;
        }
        set
        {
            eFastestTime = value;
        }
    }

    public static float HFastestTime
    {
        get
        {
            return hFastestTime;
        }
        set
        {
            hFastestTime = value;
        }
    }

    public static float MFastestTime
    {
        get
        {
            return mFastestTime;
        }
        set
        {
            mFastestTime = value;
        }
    }

    public static bool EPerfectGame
    {
        get
        {
            return ePerfectGame;
        }
        set
        {
            ePerfectGame = value;
        }
    }

    public static bool HPerfectGame
    {
        get
        {
            return hPerfectGame;
        }
        set
        {
            hPerfectGame = value;
        }
    }

    public static bool MPerfectGame
    {
        get
        {
            return mPerfectGame;
        }
        set
        {
            mPerfectGame = value;
        }
    }

}