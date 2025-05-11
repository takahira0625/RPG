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
        //�V�[�����j�����ꂽ�Ƃ��ɌĂяo�����悤�ɂ���
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        fade.FadeIn();

    }

    //�T�u�{�^���������ꂽ
    public void SubButton()
    {
        soundManager.PlayBgm(BattleBGM);
        //���C���V�[���ɃT�u�V�[����ǉ��\������
        /*fade.FadeOut();
        Application.LoadLevelAdditive("BattleScene");
        */
        StartCoroutine(MoveBattleScene());
        //�T�u�V�[�����Ăяo���Ă���Ƃ��ɔ�\���ɂ���Q�[���I�u�W�F�N�g
        foreach (GameObject obj in GameObjectsTohidden)
        {
            obj.SetActive(false);
        }
        

    }

    private void OnSceneUnloaded(Scene current)
    {
        //�V�[�����j�����ꂽ�Ƃ��ɌĂяo�����
        //����̗�ł́A�T�u�V�[�����j�����ꂽ��Ăяo�����悤�ɂȂ��Ă��܂�
        
        Debug.Log("OnSceneUnloaded: " + current.name);
        
        //�{���́A�ǂ̃V�[�����j�����ꂽ�̂��m�F���Ă��珈�����������ǂ���������Ȃ�

        //�Q�[���I�u�W�F�N�g��\������
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
