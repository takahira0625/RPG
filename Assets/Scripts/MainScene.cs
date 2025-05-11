using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainScene : MonoBehaviour
{

    public GameObject[] GameObjectsTohidden;

    [SerializeField]
    SoundManager soundManager;
    [SerializeField]
    AudioClip SE;
    [SerializeField]
    AudioClip BattleBGM;
    [SerializeField]
    AudioClip MapBGM;
    [SerializeField]
    PlayerScript playerScript;
    private Fade fade;

    // Use this for initialization
    void Start()
    {
        fade = GameObject.Find("Panel").GetComponent<Fade>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        soundManager.PlayBgm(MapBGM);
        //シーンが破棄されたときに呼び出されるようにする
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        fade.FadeIn();

    }

    //サブボタンが押された
    public void SubButton()
    {
        soundManager.PlayBgm(BattleBGM);
        //メインシーンにサブシーンを追加表示する
        /*fade.FadeOut();
        Application.LoadLevelAdditive("BattleScene");
        */
        StartCoroutine(MoveBattleScene());
        //サブシーンを呼び出しているときに非表示にするゲームオブジェクト
        foreach (GameObject obj in GameObjectsTohidden)
        {
            obj.SetActive(false);
        }
        

    }

    private void OnSceneUnloaded(Scene current)
    {
        //シーンが破棄されたときに呼び出される
        //今回の例では、サブシーンが破棄されたら呼び出されるようになっています
        
        Debug.Log("OnSceneUnloaded: " + current.name);
        
        //本当は、どのシーンが破棄されたのか確認してから処理した方が良いかもしれない

        //ゲームオブジェクトを表示する
        foreach (GameObject obj in GameObjectsTohidden)
        {
            obj.SetActive(true);
        }
        soundManager.PlayBgm(MapBGM);
    }

    IEnumerator MoveBattleScene()
    {
        fade.FadeOut();
        yield return new WaitForSeconds(1.0f);
        Application.LoadLevelAdditive("BattleScene");
    }

}
