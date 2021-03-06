﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public KeyCode keyToPress;

    public GameObject normalEffect, goodEffect, perfectEffect, missEffect;
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

                if (Mathf.Abs(transform.position.y) > 0.3f)
                {
                    GameManager.instance.NormalHit();
                    Instantiate(normalEffect, transform.position, normalEffect.transform.rotation);
                    Debug.Log("Normal Hit");
                }
                else if (Mathf.Abs(transform.position.y) > 0.15f)
                {
                    Debug.Log("Good Hit");
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                    GameManager.instance.GoodHit();
                }
                else
                {
                    Debug.Log("PERFECT!!!");
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                    GameManager.instance.PerfectHit();
                }
                   
               // GameManager.instance.NoteHit(); //If pressed (correct timing), you hit correctly.
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
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            GameManager.instance.NoteMissed(); //Missed the note.
        }
    }
}
