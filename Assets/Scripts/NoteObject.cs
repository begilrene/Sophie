﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public KeyCode keyToPress;

    // Start is called before the first frame update
    void Start()
    {
        //GameManager.Instantiate();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyToPress))
        {
            if(canBePressed)
            {
                gameObject.SetActive(false);

                GameManager.instance.NoteHit(); //If pressed (correct timing), you hit correctly.
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = false;

            GameManager.instance.NoteMissed(); //Missed the note.
        }
    }
}
