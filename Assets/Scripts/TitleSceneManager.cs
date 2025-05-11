using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class TitleSceneManager : MonoBehaviour
{
    [SerializeField]
    SoundManager soundManager;
    [SerializeField]
    AudioClip clip;
    [SerializeField]
    AudioClip SE;
    private Fade fade;

    // Start is called before the first frame update
    void Start()
    {
        fade = GameObject.Find("Panel").GetComponent<Fade>();
        soundManager.PlayBgm(clip);
        fade.FadeIn();
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetMouseButton(0)) 
        {
            fade.FadeOut();
            soundManager.PlaySe(SE);
            SceneManager.LoadScene("HomeScene");
        }
    }
}
