using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour {

    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    private string description;

    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    private bool unlocked;

    public bool Unlocked
    {
        get { return unlocked; }
        set { unlocked = value; }
    }

    private int points;

    public int Points
    {
        get { return points; }
        set { points = value; }
    }

    private int spriteIndex;

    public int SpriteIndex
    {
        get { return spriteIndex; }
        set { spriteIndex = value; }
    }

    private GameObject achievementRef;
    private List<Achievement> dependencies = new List<Achievement>();
    private string child;

    public string Child
    {
        get
        {
            return child;
        }

        set
        {
            child = value;
        }
    }

    public Achievement(string name, string description, int points, int spriteIndex, GameObject achievementRef)
    {
        this.name = name;
        this.description = description;
        this.unlocked = false;
        this.points = points;
        this.spriteIndex = spriteIndex;
        this.achievementRef = achievementRef;
        LoadAchievement();
    }

    public void AddDependency(Achievement dependency)
    {
        dependencies.Add(dependency);
    }

	public bool EarnAchievement()
    {
        if (!unlocked && !dependencies.Exists(x => x.unlocked == false))
        {
            achievementRef.GetComponent<Image>().sprite = AchievementManger.Instance.unlockedSprite;
            SaveAchievment(true);

            if (child != null)
            {
                AchievementManger.Instance.EarnAchievement(child);
            }

            return true;
        }
        return false;
    }

    public void SaveAchievment(bool value)
    {
        unlocked = value;

        int tmpPoints = PlayerPrefs.GetInt("Points");

        PlayerPrefs.SetInt("Points", tmpPoints += points);

        PlayerPrefs.SetInt(name, value ? 1 : 0);

        PlayerPrefs.Save();
    }

    public void LoadAchievement()
    {
        unlocked = PlayerPrefs.GetInt(name) == 1 ? true : false;

        if (unlocked)
        {
            AchievementManger.Instance.textPoints.text = "Points: " + PlayerPrefs.GetInt("Points");
            achievementRef.GetComponent<Image>().sprite = AchievementManger.Instance.unlockedSprite;
        }
    }
}
;