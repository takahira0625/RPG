using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    private SpriteRenderer sr = null;
    private GameObject PlayerObject; //プレイヤーオブジェクト
    private Vector3 PlayerPosition; //プレイヤーの位置情報
    private Vector3 EnemyPosition; //敵の位置情報
    //private bool eventFlag = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        PlayerObject = GameObject.FindWithTag("Player"); //Playerタグを持ったオブジェクトを見つける
        //敵、Playerそれぞれの位置情報をVector3型の変数に格納
        PlayerPosition = PlayerObject.transform.position;
        EnemyPosition = transform.position;
        
    }

    void Update()
    {
        //Enemyが画面に映ったときの処理
        if (sr.isVisible)
        {

            PlayerPosition = PlayerObject.transform.position;
            EnemyPosition = transform.position;

            if (PlayerPosition.x > EnemyPosition.x)
            {
                EnemyPosition.x = EnemyPosition.x + 0.01f;
            }
            else if (PlayerPosition.x < EnemyPosition.x)
            {
                EnemyPosition.x = EnemyPosition.x - 0.01f;
            }
            if (PlayerPosition.y > EnemyPosition.y)
            {
                EnemyPosition.y = EnemyPosition.y + 0.01f;
            }
            else if (PlayerPosition.y < EnemyPosition.y)
            {
                EnemyPosition.y = EnemyPosition.y - 0.01f;
            }
            transform.position = EnemyPosition;
        }

    }

    public void EventActive()
    {
        this.gameObject.SetActive(true);
    }
    public void EventNotActive()
    {
        this.gameObject.SetActive(false);
    }
}
