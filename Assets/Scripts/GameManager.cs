using System.Collections;
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

    public Text scoreText; //Accessor for the Text
    public Text multiplierText; // "

    public float totalNotes; //Amount of notes in specific song
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public GameObject resultsScreen;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance = this; //Only one game manager. If multiple scripts, only one instance.

        scoreText.text = "Score: 0";
        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length;
        Debug.Log(totalNotes);
    }

    // Update is called once per frame
    void Update()
    {
        if(!startPlaying) //If music hasn't started
        {
            if(Input.anyKeyDown) // if user has press something
            {
                startPlaying = true;// Music has started, notes start falling.
                theBS.hasStarted = true;

                theMusic.Play(); // Start the music.
            }
        }
        else //if music has finished playing.
        {
            if(!theMusic.isPlaying && !resultsScreen.activeInHierarchy)
            {
                resultsScreen.SetActive(true);
                normalsText.text = "" + normalHits;
                goodsText.text = "" + goodHits;
                perfectsText.text = perfectHits.ToString();
                missesText.text = missedHits.ToString();

                float totalHits = normalHits + goodHits + perfectHits;
                float totalHitPercentage = (totalHits / totalNotes) * 100;

                percentHitText.text = totalHitPercentage.ToString("F2") + " %";

                string rankVal = "F";

                if (totalHitPercentage > 40)
                {
                    rankVal = "D";
                    if(totalHitPercentage > 55)
                    {
                        rankVal = "C";
                        if(totalHitPercentage > 70)
                        {
                            rankVal = "B";
                            if(totalHitPercentage > 85)
                            {
                                rankVal = "A";
                                if(totalHitPercentage > 95)
                                {
                                    rankVal = "S";
                                }
                            }
                        }
                    }

                }

                rankText.text = rankVal;
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
        missedHits++;
        multiplierText.text = "Multiplier: X" + currentMultiplier;
    }
}
