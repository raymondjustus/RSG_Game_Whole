using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManger : MonoBehaviour {

    public GameObject achievementPrefab;
    public Sprite[] sprites;
    public ScrollRect scrollRect;
    public GameObject achievementMenu;
    public GameObject visualAchievement;
    public Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();
    public Sprite unlockedSprite;
    public Text textPoints;

    private AchievementButton activeButton;
    private static AchievementManger instance;
    private int fadeTime = 2;
    private bool windowActivate = false;

    //Variables that hold all of the achievements
    private bool hasDoneRace = false;

    public static AchievementManger Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<AchievementManger>();
            }
            return instance;
        }
    }

    // Use this for initialization
    void Start () {

        //THIS DELETES ALL SAVED PROGRESS BY A PLAYER(S)
        PlayerPrefs.DeleteAll();

        activeButton = GameObject.Find("GeneralBtn").GetComponent<AchievementButton>();

        CreateAchievement("General", "Apple Apprentice", "Pick up 10 Apples", 5, 0);
        CreateAchievement("General", "Apple Padawan", "Pick up 50 Apples", 15, 0);
        CreateAchievement("General", "Apple Master", "Pick up 100 Apples", 25, 0);
        CreateAchievement("General", "Johnny Appleseed", "Pick up 150 Apples", 50, 0);

        CreateAchievement("General", "Beginning Student", "Answer 5 Questions Correctly", 5, 0);
        CreateAchievement("General", "Studious", "Answer 25 Questions Correctly", 15, 0);
        CreateAchievement("General", "Master", "Answer 75 Questions Correctly", 25, 0);
        CreateAchievement("General", "Brain Boss", "Answer 100 Questions Correctly", 40, 0);
        CreateAchievement("General", "Living Encyclopedia", "Answer 150 Questions Correctly", 75, 0);

        CreateAchievement("General", "King of Games", "Beat the Developer's best time on Hard", 100, 0);

        //CreateAchievement("General", "Press W", "Press W to unlock this achievement", 5, 0);
        //CreateAchievement("General", "Press S", "Press S to unlock this achievement", 5, 0);
        CreateAchievement("General", "Do a Race", "Complete 1 race to unlock this achievement", 5, 0);
        CreateAchievement("General", "Do 2 Races", "Complete 2 races to unlock this achievement", 15, 0);
        //CreateAchievement("General", "All Keys", "Press all keys to unlock", 10, 0, new string[] { "Press W", "Press S" });



        foreach (GameObject achievementList in GameObject.FindGameObjectsWithTag("AchievementList"))
        {
            achievementList.SetActive(false);
        }

        activeButton.Click();

        achievementMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if (windowActivate)
        //if (Input.GetKeyDown(KeyCode.I))
        {
            achievementMenu.SetActive(!achievementMenu.activeSelf);
        }
        */
        print("Checking");
        print(PlayerPrefs.GetInt("Races"));

        if (Input.GetKeyDown(KeyCode.W))
        {
            EarnAchievement("Press W");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            EarnAchievement("Press S");
        }
        
        if (PlayerInfo.RacesRan == 1)
        {
            print("WEEEEEEEEEEEEWOOOOOOOOOOOOOOOOO");
            EarnAchievement("Do a Race");
        }

        if (PlayerInfo.RacesRan == 2)
        {
            print("WEEEEWOOOOOO");
            EarnAchievement("Do 2 Race");
        }

    }

    public void EarnAchievement(string title)
    {
        if (achievements[title].EarnAchievement())
        {
            GameObject achievement = (GameObject)Instantiate(visualAchievement);
            SetAchievementInfo("EarnCanvas", achievement, title);
            textPoints.text = "Points: " + PlayerPrefs.GetInt("Points");
            StartCoroutine(FadeAchievement(achievement));
        }
    }

    public IEnumerator HideAchievement(GameObject achievement)
    {
        yield return new WaitForSeconds(3);
        Destroy(achievement);
    }

    public void CreateAchievement(string parent, string title, string description, int points, int spriteIndex, string[] dependencies = null)
    {
        
        GameObject achievement = (GameObject)Instantiate(achievementPrefab);

        Achievement newAchievment = new Achievement(name, description, points, spriteIndex, achievement);

        achievements.Add(title, newAchievment);

        SetAchievementInfo(parent, achievement, title);

        if (dependencies != null)
        {
            foreach (string achievementTitle in dependencies)
            {
                Achievement dependency = achievements[achievementTitle];
                dependency.Child = title;
                newAchievment.AddDependency(dependency);

                //Dependency = Press Space <-- Child = Press W
                //NewAchievement = Press W --> Press Space
            }
        }
    }
    
    public void SetAchievementInfo(string parent, GameObject achievement, string title)
    {
        achievement.transform.SetParent(GameObject.Find(parent).transform, false);
        achievement.transform.localScale = new Vector3(1, 1, 1);
        achievement.transform.GetChild(0).GetComponent<Text>().text = title;
        achievement.transform.GetChild(1).GetComponent<Text>().text = achievements[title].Description;
        achievement.transform.GetChild(2).GetComponent<Text>().text = achievements[title].Points.ToString();
        //achievement.transform.GetChild(3).GetComponent<Image>().sprite = sprites[achievements[title].SpriteIndex];
    }

    public void ChangeCategory(GameObject button)
    {
        AchievementButton achievementButton = button.GetComponent<AchievementButton>();

        scrollRect.content = achievementButton.achievementList.GetComponent<RectTransform>();

        achievementButton.Click();
        activeButton.Click();
        activeButton = achievementButton;
    }

    public void AchieveWindowCheck()
    {
        achievementMenu.SetActive(!achievementMenu.activeSelf);
    }

    private IEnumerator FadeAchievement(GameObject achievement)
    {
        CanvasGroup canvasGroup = achievement.GetComponent<CanvasGroup>();

        float rate = 1.0f / fadeTime;

        int startAlpha = 0;
        int endAlpha = 1;


        for (int i = 0; i < 2; i++)
        {
            float progress = 0.0f;

            while (progress < 1.0)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, progress);

                progress += rate * Time.deltaTime;

                yield return null;
            }
            yield return new WaitForSeconds(2);
            startAlpha = 1;
            endAlpha = 0;
        }
        Destroy(achievement);

    }
}
 