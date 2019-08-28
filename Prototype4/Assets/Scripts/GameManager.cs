﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }

    [SerializeField]
    private Timer timer;
    //private List<Player> players;

    public SpawnPoints spawnPoints;

    public GameObject humanPrefab;
    public GameObject AIPrefab;
    public GameObject playerManagerPrefab;

    public Material normalMaterial;
    public Material shadowRealmMaterial;

    public const int numPlayers = 2;
    public List<GameObject> playerManagers;
   

    private void Awake()
    {
        if (instance != null && instance != this) { Destroy(this.gameObject); }
        else { instance = this; }
        Random.InitState((int)System.DateTime.Now.Ticks);

        // Create player managers
        for (int i = 0; i < numPlayers; i++)
        {
            playerManagers.Add(Instantiate(playerManagerPrefab));
            playerManagers[i].GetComponent<PlayerManager>().SetPlayerID(i);
            playerManagers[i].GetComponent<PlayerManager>().SpawnPlayer();
        }
    }

    public bool IsGameOver()
    {
        // return (timer.GetTime() < 0.0f) && players.Count < 0;
        return false;
    }
}
