﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Accessing UI

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic; //Music File

    public bool startPlaying; //Starts the music

    public BeatScroller theBS; //Controls the starting of the scrolling of the music.

    public static GameManager instance; // Instance of the game manager, static.

    public int currentScore; //Holder for current score.
    public int scorePerNote = 1000;
    public int scorePerGoodNote = 1200;
    public int scorePerPerfectNote = 1500;
    public int currentMultiplier;
    public int multiplierTracker;

    public int[] multiplierThresholds; //Making it harder for being able to reach the next multiplier.

    public Text scoreText, multiplierText;
    public Text percentHitText, normalHitText, goodHitText, perfectHitText, missesHitText, rankText, finalScoreText;

    public float totalNotes, normalHits, goodHits, perfectHits, missedHits;

    public GameObject resultsScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance = this; //Only one game manager. If multiple scripts, only one instance.

        scoreText.text = "Score: 0";
        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying) //If music hasn't started
        {
            if (Input.anyKeyDown) // if user has press something
            {
                startPlaying = true;// Music has started, notes start falling.
                theBS.hasStarted = true;

                theMusic.Play(); // Start the music.
            }
        }
        else
        {
            if(!theMusic.isPlaying && !resultsScreen.activeInHierarchy)
            {
                theBS.hasEnded = true;
                resultsScreen.SetActive(true);

                normalHitText.text = normalHits.ToString();
                goodHitText.text = goodHits.ToString();
                perfectHitText.text = perfectHits.ToString();
                missesHitText.text = missedHits.ToString();

                float totalHits = normalHits + goodHits + perfectHits;
                float totalHitPercentage = (totalHits / totalNotes) * 100;

                percentHitText.text = totalHitPercentage.ToString("F2") + "%";

                char rankVal = 'F';
                if(totalHitPercentage > 40f)
                {
                    rankVal = 'D';
                    if(totalHitPercentage > 55f)
                    {
                        rankVal = 'C';
                        if(totalHitPercentage > 70)
                        {
                            rankVal = 'B';
                            if(totalHitPercentage > 85f)
                            {
                                rankVal = 'A';
                                if(totalHitPercentage > 95f)
                                {
                                    rankVal = 'S';
                                }
                            }
                        }
                    }
                }

                rankText.text = rankVal.ToString();

                finalScoreText.text = currentScore.ToString();
                
            }
        }
    }

    public void NoteHit() // Function to call when note was hit.
    {
       // Debug.Log("Hit on Time");
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }
        theMusic.Stop();
        multiplierText.text = "Multiplier: X" + currentMultiplier;
        /*
        currentScore += scorePerNote * currentMultiplier;
        */
        scoreText.text = "Score: " + currentScore;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
        normalHits++;
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
        goodHits++;
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        perfectHits++;
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");
        currentMultiplier = 1;
        multiplierTracker = 0;

        multiplierText.text = "Multiplier: X" + currentMultiplier;
        missedHits++;
    }
}
