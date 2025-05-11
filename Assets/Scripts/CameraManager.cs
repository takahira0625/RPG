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
        //Playerの位置情報を入れる変数を宣言して、その中にPlayerの位置情報を代入
        Vector3 PlayerPosition = Player.transform.position;
        //Playerの左右移動に合わせてカメラも追従させたいので、PlayerのX座標だけを代入
        this.transform.position = new Vector3(PlayerPosition.x, PlayerPosition.y, this.transform.position.z);
    }
}
