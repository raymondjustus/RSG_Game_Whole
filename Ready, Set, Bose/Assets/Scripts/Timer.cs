using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

    public Text timerText;
    private float startTime;
    private bool timeStop;
    private float finalTime;
    
	void Start () {
        startTime = Time.time;
        timeStop = false;
	}
	
	void Update () {

        if (!timeStop)
        {
            float t = Time.time - startTime;

            string minutes = ((int)t / 60).ToString();
            string seconds;

            if ((t % 60) < 10)
            {
                seconds = "0" + (t % 60).ToString("f3");
            } else
            {
                seconds = (t % 60).ToString("f3");
            }

            timerText.text = minutes + ":" + seconds;

            finalTime = t;

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FinishLine"))
        {
            timeStop = true;

            // checks what the current level difficulty is. 
            // 1 is Hard, 2 is Medium, 3 is Easy

            // if this run on this difficulty was faster than the player's previous record, update that info
            if (SceneManager.GetActiveScene().name == "Level 1")
            {
                TimeHelper(PlayerInfo.HFastestTime);
            } else if (SceneManager.GetActiveScene().name == "Level 2")
            {
                TimeHelper(PlayerInfo.MFastestTime);

            } else if (SceneManager.GetActiveScene().name == "Level 3")
            {
                TimeHelper(PlayerInfo.EFastestTime);
            }
        }
    }

    // helper to compare previous player records with the current run on the given difficulty,
    // and update to a new PR if the player is faster
    private void TimeHelper(float playerInfo)
    {
        if (finalTime < playerInfo)
        {
            playerInfo = finalTime;
        }
    }

}

// personal high score to complete:
// Easy: 
// Medium: 
// Hard: 3:53.549