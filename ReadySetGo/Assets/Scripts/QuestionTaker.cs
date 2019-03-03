using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;

public class QuestionTaker : MonoBehaviour
{
    public string[] listedQs;
    public string[] listedAs;

    public IDictionary<int, string[]> qs = new Dictionary<int, string[]>();
    public int[] scrambledQuestionIndeces;
    
    void Start()
    {
        //counter that keeps track of the number of all the questions (and indexes each)
        int qCounter = 0;
        //counter that keeps track of whether the read line is a question, or answer 1 2 or 3
        int index = 0;
        //the path to the txt doc with the questions and answers

        TextAsset qText = Resources.Load<TextAsset>("qsToImport1");
        var arrayQText = qText.text.Split('\n');
        //string path = "Assets/Resources/qsToImport.txt";
        
        
        //array of indeces for questions that were randomly chosen
        int[] questionIndeces;
        //a temporary holder for the read question and answers
        string[] temp = new string[4];
        //a dictionary to hold all the questions and answers from the txt doc
        qs = new Dictionary<int, string[]>();
        //Read the text from directly from the test.txt file
        //StreamReader reader = new StreamReader(path);
        //the list of just the questions
        listedQs = new string[7];
        //the list of just the correct answers to the chosen questions
        listedAs = new string[7];


        //while (!reader.EndOfStream)
        foreach (var line in arrayQText)
        {
            //if the inbound line is a question
            if (index == 0)
            {
                temp[0] = line;
                //temp[0] = reader.ReadLine();
                index++;
            //otherwise its an answer
            } else
            {
                temp[index] = line;
                //temp[index] = reader.ReadLine();
                index++;
                //if this is the final piece of a question, reset and adjust everything
                if (index == 4)
                {
                    index = 0;
                    qs.Add(qCounter, temp);
                    qCounter++;
                    temp = new string[4];
                }
            }
        }

        questionIndeces = new int[qCounter];
        //creates a list with the length and numbers of how many questions there are
        for (int i = 0; i < qCounter; i++)
        {
            questionIndeces[i] = i;
        }
        //scrambles the list of numbers for questions
        System.Random rnd = new System.Random();
        int[] scrambledQuestionIndeces = questionIndeces.OrderBy(x => rnd.Next()).ToArray();


        /*
        print("HERE ARE THE RANDOM NUMBERS");
        for (int i = 0; i < 10; i++)
        {
            print(scrambledQuestionIndeces[i]);
        }
        */


        //for doublechecking all is well
        /*
        foreach (string[] value in qs.Values)
        {
            Debug.Log(value[0]);
            Debug.Log(value[1]);
            Debug.Log(value[2]);
            Debug.Log(value[3]);
        }
        Debug.Log("Next Item is number of values:");
        Debug.Log(qs.Values.Count);
        print("qCounter: " + qCounter);
        */

        //stop the reader
        //reader.Close();
        //pulls a list of only the questions that are going to be in this level- no more
        for (int i = 0; i < 7; i++)
        {
            listedQs[i] = qs[scrambledQuestionIndeces[i]][0];
        }

        /*
        print("THIS ONE IS IN QT:");
        for (int i = 0; i < 10; i++)
        {
            print(listedQs[i]);
        }
        */

        //keeps track of which question we are setting answers for
        int qNumber = 0;
        //going through each question area
        Transform t = this.gameObject.transform;
        int[] ansNums = new int[3];

        foreach (Transform child in t)
        {
            string[] specificQS = qs[scrambledQuestionIndeces[qNumber]];

            for (int k = 0; k < 3; k++)
            {
                ansNums[k] = k + 1;
            }
            int[] scramAnswers = ansNums.OrderBy(x => rnd.Next()).ToArray();

            //going through each of the answers within a question area
            foreach (Transform gChild in child)
            {
                if (gChild.tag == "Answer1")
                {
                    //sets the text of the first lane to the first answer
                    if (specificQS[scramAnswers[0]].Contains("*"))
                    {
                        gChild.GetComponentInChildren<SpriteText>().GetComponent<TextMesh>().text = ResolveTextSize(specificQS[scramAnswers[0]].Substring(0, (specificQS[scramAnswers[0]].Length - 2)), 14);

                        listedAs[qNumber] = specificQS[scramAnswers[0]];

                        getSetTag(gChild, "AnsBar1", "CorrectAns");
                        
                    }
                    else
                    {
                        gChild.GetComponentInChildren<SpriteText>().GetComponent<TextMesh>().text = ResolveTextSize(specificQS[scramAnswers[0]], 14);

                        getSetTag(gChild, "AnsBar1", "IncorrectAns");

                    }
                }
                else if (gChild.tag == "Answer2")
                {
                    //sets the text of the second lane to the second answer
                    if (specificQS[scramAnswers[1]].Contains("*")) {
                        gChild.GetComponentInChildren<SpriteText>().GetComponent<TextMesh>().text = ResolveTextSize(specificQS[scramAnswers[1]].Substring(0, (specificQS[scramAnswers[1]].Length - 2)), 14);

                        listedAs[qNumber] = specificQS[scramAnswers[1]];

                        getSetTag(gChild, "AnsBar2", "CorrectAns");
                        
                    }
                    else
                    {
                        gChild.GetComponentInChildren<SpriteText>().GetComponent<TextMesh>().text = ResolveTextSize(specificQS[scramAnswers[1]], 14);

                        getSetTag(gChild, "AnsBar2", "IncorrectAns");

                    }

                }
                else if (gChild.tag == "Answer3")
                {
                    //sets the text of the third lane to the third answer
                    if (specificQS[scramAnswers[2]].Contains("*"))
                    {
                        gChild.GetComponentInChildren<SpriteText>().GetComponent<TextMesh>().text = ResolveTextSize(specificQS[scramAnswers[2]].Substring(0, (specificQS[scramAnswers[2]].Length - 2)), 14);

                        listedAs[qNumber] = specificQS[scramAnswers[2]];

                        getSetTag(gChild, "AnsBar3", "CorrectAns");
                        
                    }
                    else
                    {
                        gChild.GetComponentInChildren<SpriteText>().GetComponent<TextMesh>().text = ResolveTextSize(specificQS[scramAnswers[2]], 14);

                        getSetTag(gChild, "AnsBar3", "IncorrectAns");
                        
                    }
                }
                else if (gChild.tag == "AnswerSign")
                {
                    //gChild.GetComponentInChildren<SpriteText>().GetComponent<TextMesh>().text = "The correct answer was \"" + ResolveTextSize(correctAnswerToPost.Substring(0, (correctAnswerToPost.Length - 1)), 14) + "\"!";
                    print("Did it.");
                }
            }
            qNumber++;
        }
    }

    // Wrap text by line height
    private string ResolveTextSize(string input, int lineLength)
    {

        // Split string by char " "         
        string[] words = input.Split(" "[0]);

        // Prepare result
        string result = "";

        // Temp line string
        string line = "";

        // for each all words        
        foreach (string s in words)
        {
            // Append current word into line
            string temp = line + " " + s;

            // If line length is bigger than lineLength
            if (temp.Length > lineLength)
            {

                // Append current line into result
                result += line + "\n";
                // Remain word append into new line
                line = s;
            }
            // Append current word into current line
            else
            {
                line = temp;
            }
        }

        // Append last line into result        
        result += line;

        // Remove first " " char
        return result.Substring(1, result.Length - 1);
    }


    void getSetTag(Transform gChild, string tagToGet, string tagToSet)
    {
        foreach (Transform tr in gChild)
        {
            if (tr.tag == tagToGet)
            {
                tr.tag = tagToSet;
            }
        }
    }


}
