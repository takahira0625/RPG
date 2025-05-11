using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float MoveSpeed = -10.0f;
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
        opponentObj = GameObject.Find("Player");
        BulletPoint_Player = GameObject.FindGameObjectWithTag("Enemy");
        CharacterBattleObj = GameObject.FindGameObjectWithTag("Enemy");
        CharacterBattleSc = CharacterBattleObj.gameObject.GetComponent<CharacterBattle>();
        base_distance = (opponentObj.transform.position - BulletPoint_Player.transform.position).sqrMagnitude;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = (BulletPoint_Player.transform.position - transform.position).sqrMagnitude;
        // 位置の更新
        transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);
        if (distance > base_distance && !flag)
        {
            CharacterBattleSc.AnimationDone();
            CharacterBattleSc.AttackTrue();
            flag = true;

        }
        if (!sr.isVisible)
        {
            Destroy(gameObject);
        }
    }
}
