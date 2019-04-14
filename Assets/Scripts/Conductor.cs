using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    int crochetsPerBar = 8;
    public float bpm;
    float dspTimeSong;
    public float crotchet, songPosition, deltaSongPosition, lastHit, actualLastHit;
    float nextBeatTime = 0.0f;
    float nextBarTime = 0.0f;
    public float offset = 0.2f;
    public float addOffset;
    public static float offsetStatic = 0.40f;
    public static bool hasOffsetAdjusted = false;
    public int beatNumber = 0;
    public int barnUmber = 0;

    public GameObject test;

    public float lastBeat;
    // Use this for initialization
    void Start()
    {
        bpm = 125;
        crotchet = 60 / bpm;
        GetComponent<AudioSource>().Play();
        dspTimeSong = (float)AudioSettings.dspTime;

    }

    // Update is called once per frame
    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dspTimeSong) * GetComponent<AudioSource>().pitch - offset;
        if (songPosition > lastBeat + crotchet)
        {
            Flash();
            lastBeat += crotchet;
        }
    }

    void Flash()
    {
        Instantiate(test, transform.position, test.transform.rotation);
    }

}