﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum EventState
{
    START,
    END
}

public abstract class Event : MonoBehaviour
{
    [SerializeField] private new string name;
    [SerializeField] private Sprite sprite;

    public void Call(EventState eventState)
    {
        switch(eventState)
        {
            case EventState.START: OnStart();
                break;
            case EventState.END: OnEnd();
                break;
        }
    }

    public abstract void OnStart();

    public abstract void OnEnd();

    public Sprite GetSprite()
    {
        return sprite;
    }
}