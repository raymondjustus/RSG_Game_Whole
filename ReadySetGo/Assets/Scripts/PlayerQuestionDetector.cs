using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerQuestionDetector : MonoBehaviour {

    public GameObject qPanel;
    public Text txt;
    public GameObject questions;

    private int qCounter;
    private string[] listOfQs;
    private string[] listOfAs;

    // Use this for initialization
    void Start () {
        qCounter = 0;

        listOfQs = questions.GetComponent<QuestionTaker>().listedQs;

        listOfAs = questions.GetComponent<QuestionTaker>().listedAs;
	}

    private void Update()
    {
        //try to come back and make this more efficient. problem was that the questions were getting used here before they were set in QT
        listOfQs = questions.GetComponent<QuestionTaker>().listedQs;

        listOfAs = questions.GetComponent<QuestionTaker>().listedAs;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("QuestionActivate")) {
            //if the question panel is inactive, assign it a question to display and activate it
            if (!qPanel.activeSelf)
            {
                txt.text = listOfQs[qCounter];
                qCounter++;
                qPanel.SetActive(true);
            }
        } else if (other.gameObject.CompareTag("Deactivate"))
        {
                qPanel.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //if the panel is active, close the door on your way out
        if (other.gameObject.CompareTag("QuestionActivate")) {
            if (qPanel.activeSelf)
            {
                txt.text = "The correct answer was \"" + listOfAs[qCounter - 1].Substring(0, listOfAs[qCounter - 1].Length - 2) + "\"!";
            }
        }
    }
}
