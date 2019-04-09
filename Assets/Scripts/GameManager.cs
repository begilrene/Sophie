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
    public int currentMultiplier;
    public int multiplierTracker;

    public int[] multiplierThresholds; //Making it harder for being able to reach the next multiplier.

    public Text scoreText; //Accessor for the Text
    public Text multiplierText; // "
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance = this; //Only one game manager. If multiple scripts, only one instance.

        scoreText.text = "Score: 0";
        currentMultiplier = 1;
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
    }

    public void NoteHit() // Function to call when note was hit.
    {
        Debug.Log("Hit on Time");
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
        currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");
        currentMultiplier = 1;
        multiplierTracker = 0;

        multiplierText.text = "Multiplier: X" + currentMultiplier;
    }
}
