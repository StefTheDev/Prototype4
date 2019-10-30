﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormEvent : Event
{
    [SerializeField] private GameObject lightning;

    public bool includeChildren = true;

    public override void OnStart()
    {
        Debug.Log("Storm Event Started.");
        lightning.SetActive(true);
    }

    public override void OnEnd()
    {
        Debug.Log("Storm Event Ended.");
        lightning.SetActive(false);
    }
}