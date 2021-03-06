﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public static SpawnManager Instance = null;
    public GameObject[] EnemyPrefab;
    LinkedList<GameObject> EnemyList;

    float fSpawnDist = 15f;
    float fSpawnAngle = 15f;
    float fTime = 0f;

    private void Awake()
    {
        if (null == Instance)
            Instance = this;
        else if (this != Instance)
            Destroy(Instance);

        EnemyList = new LinkedList<GameObject>();
    }

    private void Update()
    {
        if (180f > fSpawnAngle)
        {
            fSpawnAngle += GameManager.Instance.fProgressTime * 0.002f;
            if (180f <= fSpawnAngle)
                fSpawnAngle = 180f;
        }

        if (17 <= EnemyList.Count)
            return;

        fTime += Time.deltaTime;
        if(2f < fTime && GameManager.Instance.Player)
        {
            fTime = 0f;

            float fRandom = Random.Range(-fSpawnAngle, fSpawnAngle);
            EnemyList.AddLast(Instantiate(EnemyPrefab[Random.Range(0, EnemyPrefab.Length)], GameManager.Instance.Player.transform.position, GameManager.Instance.Player.transform.rotation) as GameObject);
            GameObject obj = EnemyList.Last.Value;
            obj.transform.Rotate(0, fRandom, 0);
            obj.transform.Translate(obj.transform.forward * fSpawnDist);
            obj.GetComponent<GameEnemyCharacter>().index = EnemyList.Count - 1;
        }

    }

    public void RemoveEnemy(GameObject me)
    {
        EnemyList.Remove(me);
        Destroy(me);
        DataManager.Instance.AddScore(1);
    }
}
