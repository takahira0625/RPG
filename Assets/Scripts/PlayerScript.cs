using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;

public class PlayerScript : MonoBehaviour
{
    public FloatingJoystick inputMove; //移動用JoyStick
    float moveSpeed = 70.0f; //移動する速度
    Rigidbody2D playerRb; //PlayerのRigidbody

    public GameObject controllerObj;//SceneManagerのオブジェクト
    private MainScene MainSceneSc;//SceneManagerのスクリプト
    public GameObject JoyStickObj; //ジョイスティック

    private float axisX;
    private float axisY;
    private int enemyNumber;
    private bool eventFlag = false;
    private bool stickFlag = false;
    private float axisX_save;
    private float axisY_save;
    Flowchart flowChart;
    public GameObject[] enemys; //敵たち
    private EnemyScript[] enemySc;
    [SerializeField]
    string message = "";
    private bool isTalking = false;


    void OnEnable()
    {
        var ab = JoyStickObj.GetComponent<FloatingJoystick>();
        ab.OnPointerUp(null);
        Debug.Log("シーン開始");
    }
    // Start is called before the first frame update

    void Awake()
    {
        playerRb = this.gameObject.GetComponent<Rigidbody2D>();
        MainSceneSc = controllerObj.gameObject.GetComponent<MainScene>();
        flowChart = GetComponent<Flowchart>();
        enemySc = new EnemyScript[enemys.Length];

        for (int i = 0; i < enemys.Length; i++)
        {
            enemySc[i] = enemys[i].gameObject.GetComponent<EnemyScript>();
        }
        
    }
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
        if (eventFlag == false)
        {
            //スティックでの移動
            axisX = inputMove.Horizontal; //水平方向の向き
            axisY = inputMove.Vertical;   //垂直方向の向き

            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(new Vector3(axisX, axisY) * moveSpeed);
            
            
        }
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D other)
    {
        if(eventFlag == false)
        {
            
            if (other.gameObject.CompareTag("Enemy"))
            {
                
                enemyNumber = 0;
                PlayerPrefs.SetInt("enemy", enemyNumber);
                Debug.Log("当たった");
                MainSceneSc.SubButton();
                
                Destroy(other.gameObject);
            }

            else if (other.gameObject.CompareTag("Enemy1"))
            {
                enemyNumber = 1;
                PlayerPrefs.SetInt("enemy", enemyNumber);
                Debug.Log("当たった");
              
                MainSceneSc.SubButton();
               
                Destroy(other.gameObject);
            }

            else if (other.gameObject.CompareTag("Enemy2"))
            {
                enemyNumber = 2;
                PlayerPrefs.SetInt("enemy", enemyNumber);
                Debug.Log("当たった");
               
                MainSceneSc.SubButton();
                
                Destroy(other.gameObject);
            }

            else if (other.gameObject.CompareTag("MoveMap1"))
            {
                
                Debug.Log("当たった");
                SceneManager.LoadScene("MapScene2");

            }
            else if (other.gameObject.CompareTag("MapEvent1"))
            {

                Debug.Log("当たった");
                StartCoroutine(Talk("event1"));

            }
            else if (other.gameObject.CompareTag("MapEvent2"))
            {

                Debug.Log("当たった");
                StartCoroutine(Talk("event2"));

            }
        }
    }

    public void ResetAxis()
    {
        axisX = 0.0f; //水平方向の向き
        axisY = 0.0f;   //垂直方向の向き
    }

    public void StickFlag()
    {
        stickFlag = true;
        inputMove.HandleRange = 0;
        axisX_save = axisX;
        axisY_save = axisY;
        
    }

    public void CompareAxis()
    {
        if(axisX != axisX_save || axisY != axisY_save)
        {
            stickFlag = false;
            inputMove.HandleRange = 1;
        }
    }

    public void EventTrue()
    {
        playerRb.velocity = Vector2.zero;
        moveSpeed = 0;
        eventFlag = true;
    }

    public void EventFalse()
    {
        moveSpeed = 100;
        eventFlag = false;
    }

    IEnumerator Talk(string word)
    {
        message = word;
        EventTrue();
        for (int i = 0; i < enemys.Length; i++)
        {
            enemySc[i].EventNotActive();
        }
        if (isTalking)
        {
            yield break;
        }

        isTalking = true;


        flowChart.SendFungusMessage(message);
        yield return new WaitUntil(() => flowChart.GetExecutingBlocks().Count == 0);
        
        isTalking = false;
        EventFalse();
        for (int i = 0; i < enemys.Length; i++)
        {
            enemySc[i].EventActive();
        }
        if(message == "event2")
        {
            SceneManager.LoadScene("EndingScene");
        }

    }
}
