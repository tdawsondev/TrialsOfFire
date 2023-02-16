using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusEffect
{
    public float maxDuration = 20f;
    public float timeLeft;
    public bool running = false;
    public bool finished = false;
    public Character effectedCharacter;

    public StatusEffect(float duration, Character character)
    {
        maxDuration = duration;
        effectedCharacter = character;
    }
    public StatusEffect(StatusEffect effect, Character character)
    {
        maxDuration = effect.maxDuration;
        effectedCharacter = character;
    }

    public virtual void StartRun()
    {
        running = true;
        timeLeft = maxDuration;
    }

    public virtual void Pause()
    {
        running = false;
    }
    public virtual void Stop()
    {
        running = false;
        timeLeft = maxDuration;
        finished = true;
    }

    public virtual void Run()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {
            Stop();
        }
    }
}
