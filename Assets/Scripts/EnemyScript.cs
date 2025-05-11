using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    private SpriteRenderer sr = null;
    private GameObject PlayerObject; //�v���C���[�I�u�W�F�N�g
    private Vector3 PlayerPosition; //�v���C���[�̈ʒu���
    private Vector3 EnemyPosition; //�G�̈ʒu���
    //private bool eventFlag = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        PlayerObject = GameObject.FindWithTag("Player"); //Player�^�O���������I�u�W�F�N�g��������
        //�G�APlayer���ꂼ��̈ʒu����Vector3�^�̕ϐ��Ɋi�[
        PlayerPosition = PlayerObject.transform.position;
        EnemyPosition = transform.position;
        
    }

    void Update()
    {
        //Enemy����ʂɉf�����Ƃ��̏���
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
