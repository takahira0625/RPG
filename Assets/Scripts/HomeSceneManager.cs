using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeSceneManager : MonoBehaviour
{
    [SerializeField]
    SoundManager soundManager;
    [SerializeField]
    AudioClip SE;
    private Fade fade;
    // Start is called before the first frame update
    void Start()
    {
        fade = GameObject.Find("Panel").GetComponent<Fade>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        fade.FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartButton()
    {
        fade.FadeOut();
        soundManager.PlaySe(SE);
        SceneManager.LoadScene("MapScene");
    }
    public void ContinueButton()
    {
        fade.FadeOut();
        soundManager.PlaySe(SE);
        SceneManager.LoadScene("MapScene");
    }
}
