using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager1 : MonoBehaviour
{
    public AudioSource theMusic;

    public bool startPlaying;

    public BeatScroller theBS;

    public static GameManager1 instance;

    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public Text scoreText;
    public Text multiText;

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public GameObject resultsScreen;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;

    public Animator animator;
    public Animator drmike;
    public Animator life;

    public float healthcount; 
    public GameObject healthBar;
    public GameObject reach;
    public GameObject threshold;
 
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        scoreText.text = "Score: 0";

        currentMultiplier = 1;

        //totalNotes = FindObjectsOfType<NoteObject>().Length;
        totalNotes = 1035;
    }

    // Update is called once per frame
    void Update()
    {
        if(!startPlaying)
        {
            if(Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;
                drmike.SetBool("GameStart", true);
                theMusic.Play();
            }
        }
        else if(!theMusic.isPlaying && !resultsScreen.activeInHierarchy && healthcount > 725)
        {

            animator.SetBool("Game_end", true);
            resultsScreen.SetActive(true);
            healthBar.SetActive(false);
            reach.SetActive(false);
            threshold.SetActive(false);

            normalsText.text = "" + normalHits; //different for fun lol
            goodsText.text = goodHits.ToString();
            perfectsText.text = perfectHits.ToString();
            missesText.text = missedHits.ToString();

            float totalHit = normalHits + goodHits + perfectHits;
            float percentHit = (totalHit/totalNotes) * 100f;

            percentHitText.text = percentHit.ToString("F1") + "%";

            string rankVal = "F";

                if(percentHit > 40)
                {
                    rankVal = "D";
                    if (percentHit > 55)
                    {
                        rankVal = "C";
                        if (percentHit > 70)
                        {
                            rankVal = "B";
                            if (percentHit > 80)
                            {
                                rankVal = "A";
                                if (percentHit > 95)
                                {
                                    rankVal = "S";
                                }
                            }
                        }
                    }
                }

                rankText.text = rankVal;

                finalScoreText.text = currentScore.ToString();
                drmike.SetBool("GameEnd", true);
                life.SetBool("Win", true);
        }else if(!theMusic.isPlaying && !resultsScreen.activeInHierarchy && healthcount < 725)
        {
            animator.SetBool("Game_end", true);
            resultsScreen.SetActive(true);
            healthBar.SetActive(false);
            reach.SetActive(false);
            threshold.SetActive(false);

            normalsText.text = "" + normalHits; //different for fun lol
            goodsText.text = goodHits.ToString();
            perfectsText.text = perfectHits.ToString();
            missesText.text = missedHits.ToString();

            float totalHit = normalHits + goodHits + perfectHits;
            float percentHit = (totalHit/totalNotes) * 100f;

            percentHitText.text = percentHit.ToString("F1") + "%";

            string rankVal = "F";

                if(percentHit > 40)
                {
                    rankVal = "D";
                    if (percentHit > 55)
                    {
                        rankVal = "C";
                        if (percentHit > 70)
                        {
                            rankVal = "B";
                            if (percentHit > 80)
                            {
                                rankVal = "A";
                                if (percentHit > 95)
                                {
                                    rankVal = "S";
                                }
                            }
                        }
                    }
                }

                rankText.text = rankVal;

                finalScoreText.text = currentScore.ToString();
                drmike.SetBool("BadGameEnd", true);
        }

    }

    public void NoteHit()
    {
        Debug.Log("Hit On Time");

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        multiText.text = "Multiplier: x" + currentMultiplier;

        //currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();

        normalHits++;
        healthcount++;
        healthBar.GetComponent<FHealth>().Heal();
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();

        perfectHits++;
        healthcount++;
        healthBar.GetComponent<FHealth>().Heal();
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();

        goodHits++;
        healthcount++;
        healthBar.GetComponent<FHealth>().Heal();
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");

        currentMultiplier = 1;
        multiplierTracker = 0;

        multiText.text = "Multiplier: x" + currentMultiplier;
        missedHits++;
        healthcount--;
        healthBar.GetComponent<FHealth>().Damage();
    }
}
