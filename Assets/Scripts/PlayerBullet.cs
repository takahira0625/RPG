using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private float MoveSpeed = 10.0f;
    private SpriteRenderer sr = null;
    private GameObject opponentObj; //ベースオブジェクトの位置
    private GameObject BulletPoint_Player; //Playerの弾発射位置
    private float base_distance;
    private float distance;
    private GameObject CharacterBattleObj;//CharacterBattleのオブジェクト
    private CharacterBattle CharacterBattleSc;//CharacterBattleのスクリプト
    private bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        opponentObj = GameObject.FindGameObjectWithTag("Enemy");
        BulletPoint_Player = GameObject.Find("Player");
        CharacterBattleObj = GameObject.Find("Player");
        CharacterBattleSc = CharacterBattleObj.gameObject.GetComponent<CharacterBattle>();
        base_distance = (opponentObj.transform.position - BulletPoint_Player.transform.position).sqrMagnitude;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = (BulletPoint_Player.transform.position - transform.position).sqrMagnitude;
        // 位置の更新
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();  // rigidbodyを取得
        Vector3 now = rb.position;            // 座標を取得
        now += new Vector3(MoveSpeed* Time.deltaTime, 0.0f, 0.0f);  // 前に少しずつ移動するように加算
        rb.position = now; // 値を設定
        // 画面外に出たかどうかを判定
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        bool isOutsideScreen = (screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1);
        if (distance > base_distance && !flag)
        {
            CharacterBattleSc.AnimationDone();
            flag = true;
            
        }
        if (isOutsideScreen)
        {
            Destroy(gameObject);
        }
    }
}
