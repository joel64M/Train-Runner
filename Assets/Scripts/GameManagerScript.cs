﻿
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;
   // public float speed=5f;
    public bool isGameStart=false, isGameComplete=false, isGameOver=false;
    public float raceStartLength = 30f;
    public float speed = 5f;
    public float trainSpeed = 10;
    public float trainNormalSpeed = 5;
    public bool isPlayerGameStart = false;
    public bool DeletePlayerPrefs;
    public int level = 0;

    int reachedGoalIndex = 0;

    public List<Statistics> stats = new List<Statistics>();

    public class Completion : IComparer<Statistics>
    {
        public int Compare(Statistics x, Statistics y)
        {
            return x.completion.CompareTo(y.completion);
        }
    }
    void SortStats()
    {
        stats.Sort(new Completion());
        stats.Reverse();
        for (int i = 0; i < stats.Count; i++)
        {
            stats[i].rank = i;
        }
    }

    int nameIndex = 0;
    bool nameIndexed;
    public string[] names = { "Bob" ,"Leo","Kim","Ron","Tim","Sal","Nat","Sam"};

    public void AddToStats(Statistics s)
    {
        stats.Add(s);
        if (!nameIndexed)
        {
            nameIndexed = true;
            nameIndex = SceneManager.GetActiveScene().buildIndex % names.Length;
        }
        if (s.isPlayer)
        {
            s.playerName = "You";
        }
        else
        {
            s.playerName = names[nameIndex];
            nameIndex++;
        }

    }
    private void Awake()
    {
        instance = this;
        level = PlayerPrefs.GetInt("LEVEL", 1);
        Application.targetFrameRate = 60;

        if (DeletePlayerPrefs)
            PlayerPrefs.DeleteAll();
    }

    void Start()
    {
        // isGameStart = true;
      LoadLevel();
    }

    void LoadLevel()
    {

        if (SceneManager.GetActiveScene().name != "Level" + level.ToString())
        {
            if (Application.CanStreamedLevelBeLoaded("Level" + level.ToString()))
            {
                SceneManager.LoadScene("Level" + level.ToString());

            }
            else
            {
                //  PlayerPrefs.DeleteAll();
                if (SceneManager.GetActiveScene().name != "FinalLevel")
                {
                    if (Application.CanStreamedLevelBeLoaded("FinalLevel"))
                        SceneManager.LoadScene("FinalLevel");
                    else
                    {
                        PlayerPrefs.DeleteAll();
                        SceneManager.LoadScene("Level1");
                    }

                }
            }
        }
        else
        {
            //  UIManagerScript.instance.SetUI();
            //     GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, Application.version, level.ToString());
            // print("progression start" + level.ToString());

        }
    }
    public void GameStart()
    {
       isGameStart = true;
        isGameOver = false;
        isGameComplete = false;
        InvokeRepeating("SortStats", 2, 0.1f);
      Time.timeScale = 9;
    }
    public void GameOver()
    {

      //  isGameStart = false;
        isGameOver = true;
        isGameComplete = false;
        Handheld.Vibrate();


    }
    public void Died()
    {
     //   isGameStart = false;
        isGameOver = true;
        isGameComplete = false;
        UIManagerScript.instance.ShowDeathUI();
        Handheld.Vibrate();
    }
    public void GameComplete()
    {
        if (isGameComplete)
        {
            return;
        }
        Handheld.Vibrate();

        isGameComplete = true;
      //  isGameStart = false;
        isGameOver = false;
       
        if (SceneManager.GetActiveScene().name == "Level" + level.ToString())
        {
            PlayerPrefs.SetInt("LEVEL", level + 1);
        }

        //   StartCoroutine(ShowUIAfterSomeTime());
      
    }

    void ShowUIAfterSomeTime()
    {
       // yield return new WaitForSeconds(1f);
        UIManagerScript.instance.ShowUI();
    }
    public void CharacterReachedGoal(Statistics s)
    {
        reachedGoalIndex++;  ///start with 1 
        UIManagerScript.instance.SetUI(s,reachedGoalIndex);
        if (s.isPlayer)
        {
            if (reachedGoalIndex == 1)
            {
               // Debug.Log("first");
                GameComplete();
            }
            else
            {
                GameOver();
            }
            Invoke("ShowUIAfterSomeTime", 0.5f);
        }

        //   Invoke("GameComplete", .5f);



    }

}
