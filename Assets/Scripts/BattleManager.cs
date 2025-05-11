using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleManger : MonoBehaviour
{
    public GameObject player; //プレイヤー
    public GameObject[] enemys; //敵たち

    private CharacterBattle playerBattle;
    private CharacterBattle[] enemyBattle;

    private int currentEnemyIndex = 0; //何番目の敵か
    private bool playerTurn; //true:プレイヤーターン false:敵のターン
    private bool comandTurn; //true:コマンド入力受付 false:戦闘中
    private bool batteleTurn; //true:行動中 false:次の行動へ


    public GameObject[] atkButton;//攻撃ボタンを入れる箱

    public GameObject AvoidButton; //回避ボタン

    public GameObject SceneText; //テキスト

    private Text scene_text; //テキストの内容
    private Fade fade;

    private void Start()
    {
        fade = GameObject.Find("Panel").GetComponent<Fade>();
        fade.FadeIn();
        GetChildren();
        //最初はプレイヤーのターン
        playerTurn = true;
        comandTurn = true;
        batteleTurn = false;


        scene_text = SceneText.GetComponent<Text>();
        StartCoroutine(TextManager("プレイヤーのターン","プレイヤーのターン",0.0f));
        playerBattle = player.gameObject.GetComponent<CharacterBattle>();

        enemyBattle = new CharacterBattle[enemys.Length];

        for (int i = 0; i < enemys.Length; i++)
        {
            enemyBattle[i] = enemys[i].gameObject.GetComponent<CharacterBattle>();
        }
        
    }

    private void Update()
    {
        //プレイヤーのターン
        if (playerTurn && !comandTurn && !batteleTurn)
        {
            
            //プレイヤーのスクリプトから行動させる
            playerBattle.CharacterAction();
            
            batteleTurn = true;

            Debug.Log("プレイヤーの攻撃！");
        }
        //敵のターン
        else if (!playerTurn && !comandTurn && !batteleTurn)
        {
            //敵のスクリプトから行動させる

            //配列分の敵が行動したら終わり
            if (currentEnemyIndex == enemys.Length)
            {

                comandTurn = true;
                playerTurn = true;
                currentEnemyIndex = 0;
            }
            //各敵のターン
            else
            {
                enemyBattle[currentEnemyIndex].CharacterAction();
                batteleTurn = true;

                Debug.Log("エネミーの攻撃！");
            }

        }

        //コマンド選択時にだけボタンを表示
        if (comandTurn)
        {
            StartCoroutine(TextManager("", "プレイヤーのターン", 0.0f));

            for (int x = 0; x < atkButton.Length; x++)
            {
                atkButton[x].SetActive(true);
            }
        }
        else
        {
            for (int x = 0; x < atkButton.Length; x++)
            {
                atkButton[x].SetActive(false);
            }
        }

    }

    void GetChildren()
    {
        Transform children = this.gameObject.GetComponentInChildren<Transform>();
        enemys = new GameObject[children.childCount];
        //子要素がいなければ終了
        for(int i = 0; i < children.childCount; i++)
        {
            enemys[i] = children.transform.GetChild(i).gameObject;
        }

    }
    //コマンド選択の終了
    public void ComandDone()
    {
        comandTurn = false;
        Debug.Log("コマンド入力終了");
    }

    //プレイヤーの行動終了
    public IEnumerator playerDone()
    {
        playerTurn = false;
        StartCoroutine(TextManager("敵のターン", "", 3.0f));
        yield return new WaitForSeconds(1.0f);
        batteleTurn = false;

        Debug.Log("プレイヤーの攻撃終わり");
        
    }

    //敵の行動
    public void enemyDone()
    {
        currentEnemyIndex += 1;
        batteleTurn = false;

        Debug.Log("エネミーの攻撃終わり");
        
    }

    public bool returnPlayerTurn()
    {
        return playerTurn;
    }

    //text関連
    IEnumerator TextManager(string Btext,string Atext, float wait)
    {
        if (playerTurn)
        {
            scene_text.text = Btext;
            yield return new WaitForSeconds(wait);
            scene_text.text = Atext;
        }
        if (!playerTurn)
        {
            scene_text.text = Btext;
            yield return new WaitForSeconds(wait);
            scene_text.text = Atext;
        }
        
    }

    //シーン上に指定したタグが付いたオブジェクトを数えて返す。
    public int checkTag(string tagName)
    {
        GameObject[] tagObjects;
        tagObjects = GameObject.FindGameObjectsWithTag(tagName);
        if (tagObjects.Length == 0)
        {
            Debug.Log(tagName + "タグがついたオブジェクトはありません");
        }
        return tagObjects.Length;
    }
}