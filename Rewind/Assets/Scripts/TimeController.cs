﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using static DG.Tweening.DOTweenCYInstruction;

public class TimeController : MonoBehaviour
{
    public GameObject playerClonePrefab;
    public List<Vector3> playerPositions = new List<Vector3>();
    private GameObject player;

    public static TimeController instance;

    public float recordTime;
    private float timeSinceStart;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        TryGetPlayer();
    }

    void FixedUpdate()
    {
        timeSinceStart += Time.fixedDeltaTime;

        if (player == null)
            TryGetPlayer();


        playerPositions.Insert(0, player.transform.position);
        ////prevent clone to stay still when player stood still (should I prevent this?)
        //if (playerPositions.Count > 0)
        //{
        //    if(player.transform.position != (Vector3)playerPositions[Mathf.Max(playerPositions.Count-1, 0)])
        //    {
        //        playerPositions.Add(player.transform.position);
        //    }
        //} else
        //{
        //    playerPositions.Add(player.transform.position);
        //}
        
        //50 fixed updates per second
        if(playerPositions.Count > recordTime * 50)
        {
            //gets only last range
            playerPositions = playerPositions.GetRange((int)(playerPositions.Count - 1 - (recordTime * 50)), playerPositions.Count - 1);
        }
    }

    public void SpawnPlayerAndReverse()
    {
        StartCoroutine(ReverseCoroutine(playerPositions.ToArray()));
        this.playerPositions.Clear();
        timeSinceStart = 0;
    }

    private IEnumerator ReverseCoroutine(Vector3[] positions)
    {
        GameObject playerClone = Instantiate(playerClonePrefab);
        playerClone.GetComponent<PlayerClone>().Reverse(positions, Mathf.Min(timeSinceStart, recordTime));
        yield return new WaitForEndOfFrame();
    }

    private void TryGetPlayer()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");

        if (this.player == null)
        {
            Debug.LogError("did not find player in scene!");
        }
    }
}
