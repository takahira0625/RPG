using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject Player;
    private void Start()
    {
        
    }
    void Update()
    {
        //Player�̈ʒu��������ϐ���錾���āA���̒���Player�̈ʒu������
        Vector3 PlayerPosition = Player.transform.position;
        //Player�̍��E�ړ��ɍ��킹�ăJ�������Ǐ]���������̂ŁAPlayer��X���W��������
        this.transform.position = new Vector3(PlayerPosition.x, PlayerPosition.y, this.transform.position.z);
    }
}
