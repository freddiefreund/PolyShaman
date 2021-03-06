using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    [SerializeField] private float songBpm;
    [SerializeField] private float latency;

    [SerializeField] private AudioSource leftDrum;
    [SerializeField] private AudioSource rightDrum;

    [SerializeField] private AttackSpells attackSpells;

    private float secPerBeat;
    private float songPosition;
    private float songPositionInBeats;
    private float dspSongTime;
    private AudioSource musicSource;

    int lastBeat = 0;
    int currentBeat = 0;

    bool isInCombo = false;

    float offsetThreshold = 0.1f;

    float lastLeftDrumHit = 0.0f;
    float currentLeftDrumHit = 0.0f;
    int leftHandStreak = 0;

    float lastRightDrumHit = 0.0f;
    float currentRightDrumHit = 0.0f;
    int rightHandStreak = 0;

    Polyrhythm[] polyrhythms = new Polyrhythm[3] {
        new Polyrhythm(0, 4, 2),
        new Polyrhythm(1, 3, 2),
        new Polyrhythm(2, 5, 4),
    };

    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        secPerBeat = 60f / songBpm;

        dspSongTime = (float)AudioSettings.dspTime;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - latency);
        songPositionInBeats = songPosition / secPerBeat;
        /*
        currentbeat = (int)songpositioninbeats;
        if (currentbeat > lastbeat)
        {
            debug.log(currentbeat);
            lastbeat = currentbeat;
        }
        */
        EvaluateCombo();
    }

    private void EvaluateCombo()
    {
        if (!isInCombo && IsHitSimultaneously())
        {
            Debug.Log("START");
            isInCombo = true;
            leftHandStreak = 0;
            rightHandStreak = 0;

            foreach (Polyrhythm polyrhythm in polyrhythms)
            {
                polyrhythm.IsPossible = true;
            }
        }

        if (isInCombo)
        {
            bool finishIsPossible = false;
            // When left drum hit detected
            if (currentLeftDrumHit > lastLeftDrumHit)
            {
                if (leftHandStreak > 0)
                {
                    foreach (Polyrhythm polyrhythm in polyrhythms)
                    {
                        if (polyrhythm.IsPossible)
                        {
                            float lowerLimit = (currentLeftDrumHit - lastLeftDrumHit) - offsetThreshold;
                            if (polyrhythm.LeftHandInterval >= lowerLimit)
                            {
                                finishIsPossible = true;
                            }
                            else
                            {
                                polyrhythm.IsPossible = false;
                            }
                        }
                    }   
                }
                if (finishIsPossible || leftHandStreak == 0) leftHandStreak += 1;
                lastLeftDrumHit = currentLeftDrumHit;
            }
            // When right drum hit detected
            if (currentRightDrumHit > lastRightDrumHit)
            {
                if (rightHandStreak > 0)
                {
                    foreach (Polyrhythm polyrhythm in polyrhythms)
                    {
                        if (polyrhythm.IsPossible)
                        {
                            float lowerLimit = (currentRightDrumHit - lastRightDrumHit) - offsetThreshold;
                            if (polyrhythm.RightHandInterval >= lowerLimit)
                            {
                                finishIsPossible = true;
                            }
                            else
                            {
                                polyrhythm.IsPossible = false;
                            }
                        }
                    }
                }
                if (finishIsPossible || rightHandStreak == 0) rightHandStreak += 1;
                lastRightDrumHit = currentRightDrumHit;
            }
            // Check if combo is finished
            if (finishIsPossible)
            {
                foreach(Polyrhythm polyrhythm in polyrhythms)
                {
                    if (polyrhythm.LeftCountNeeded == leftHandStreak &&
                        polyrhythm.RightCountNeeded == rightHandStreak &&
                        polyrhythm.IsPossible)
                    {
                        switch(polyrhythm.Id)
                        {
                            case 0:
                                attackSpells.SpikeAttack();
                                break;
                            case 1:
                                attackSpells.Heal();
                                break;
                            default:
                                Debug.Log("No spell implemented");
                                break;
                        }
                        Debug.Log("FINISH " + polyrhythm.ToString());
                        isInCombo = false;
                    }
                }
            }
            // Check if Time exceeds hits
            foreach (Polyrhythm polyrhythm in polyrhythms)
            {
                if (polyrhythm.IsPossible)
                {
                    float upperLeftLimit = currentLeftDrumHit + polyrhythm.LeftHandInterval + offsetThreshold;
                    float upperRightLimit = currentRightDrumHit + polyrhythm.RightHandInterval + offsetThreshold;

                    if (songPositionInBeats > upperLeftLimit || songPositionInBeats > upperRightLimit)
                    {
                        Debug.Log($"Deactivate {polyrhythm.ToString()}");
                        polyrhythm.IsPossible = false;
                    }   
                }
            }
            // Check if any polyrythm can be finished
            bool exitCombo = true;
            foreach (Polyrhythm polyrhythm in polyrhythms)
            {
                if (polyrhythm.IsPossible)
                {
                    exitCombo = false;
                    break;
                }
            }

            if (exitCombo)
            {
                Debug.Log("C-C-C-COMBO BREAKER");
                isInCombo = false;
            }
        }
    }

    public void HitDrumLeft()
    {
        currentLeftDrumHit = songPositionInBeats;
        leftDrum.Play();
    }

    public void HitDrumRight()
    {
        currentRightDrumHit = songPositionInBeats;
        rightDrum.Play();
    }

    public bool IsHitSimultaneously()
    {
        float drumDifference = Mathf.Abs(currentLeftDrumHit - currentRightDrumHit);
        bool hitSimultaneously = drumDifference <= offsetThreshold && currentLeftDrumHit > 0 && currentRightDrumHit > 0;
        return hitSimultaneously;
    }
}
