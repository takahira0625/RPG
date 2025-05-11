using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float MoveSpeed = -10.0f;
    private SpriteRenderer sr = null;
    private GameObject opponentObj; //�x�[�X�I�u�W�F�N�g�̈ʒu
    private GameObject BulletPoint_Player; //Player�̒e���ˈʒu
    private float base_distance;
    private float distance;
    private GameObject CharacterBattleObj;//CharacterBattle�̃I�u�W�F�N�g
    private CharacterBattle CharacterBattleSc;//CharacterBattle�̃X�N���v�g
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
        // �ʒu�̍X�V
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
