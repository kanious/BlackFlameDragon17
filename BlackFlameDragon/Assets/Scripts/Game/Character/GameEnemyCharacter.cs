using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PackageProject.SpecialHelper.CatchHand;

public class GameEnemyCharacter : Character {

    public int index;
    public float fTime;
    
    private void Awake()
    {
        
    }

    private void Update()
    {
        this.transform.LookAt(GameManager.Instance.Player.transform);
        this.transform.position = Vector3.MoveTowards(this.transform.position
            , GameManager.Instance.Player.transform.position, Time.deltaTime * status.fSpeed);

        fTime += Time.deltaTime;
        if(6f < fTime)
        {
            SpawnManager.Instance.RemoveEnemy(gameObject);
            Destroy(this.gameObject);
        }
    }
}
