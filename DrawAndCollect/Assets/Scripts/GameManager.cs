using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DrawLine drawLine;
    [SerializeField] private ThrowBall throwBall;
    [Header("Audio Sources")]
    [SerializeField] private AudioSource[] Sounds;
    [Header("Particles")]
    public ParticleSystem[] Particles;
    [Header("UI Panels")]
    [SerializeField] private GameObject[] Panels;
    [SerializeField] private TextMeshProUGUI[] BestScoreTexts;

    int ScoredBall;
    void Start()
    {
        ScoredBall = 0;
        if(PlayerPrefs.HasKey("BestScore"))
        {
            BestScoreTexts[0].text = PlayerPrefs.GetInt("BestScore").ToString();
            BestScoreTexts[1].text = PlayerPrefs.GetInt("BestScore").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("BestScore",0);
            BestScoreTexts[0].text = "0";
            BestScoreTexts[1].text = "0";
        }
    }
    public void Return()
    {
        ScoredBall++;
        throwBall.Return();
        drawLine.Return();
    }
    public void GameOver()
    {
        Sounds[2].Play();
        Panels[1].SetActive(true);
        Panels[2].SetActive(false);
        BestScoreTexts[1].text = PlayerPrefs.GetInt("BestScore").ToString();
        BestScoreTexts[2].text = ScoredBall.ToString();

        if(ScoredBall > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore",ScoredBall);
            Particles[1].gameObject.SetActive(true);
            Particles[1].Play();
        }
        drawLine.NotDraw();
        
    }
    public void PlaySounds(int Index)
    {
        Sounds[Index].Play();
    }
    public void GameStart()
    {
        throwBall.GameStart();
        drawLine.Drawing();
        Panels[0].SetActive(false); 
        Panels[2].SetActive(true);
    }
    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
    }

}
