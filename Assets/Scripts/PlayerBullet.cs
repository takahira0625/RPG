using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private float MoveSpeed = 10.0f;
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
        // �ʒu�̍X�V
        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();  // rigidbody���擾
        Vector3 now = rb.position;            // ���W���擾
        now += new Vector3(MoveSpeed* Time.deltaTime, 0.0f, 0.0f);  // �O�ɏ������ړ�����悤�ɉ��Z
        rb.position = now; // �l��ݒ�
        // ��ʊO�ɏo�����ǂ����𔻒�
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
