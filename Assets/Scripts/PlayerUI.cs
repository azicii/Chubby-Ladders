using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    Player player;

    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI instructions;
    [SerializeField] Slider timerSlider;

    public int rawScore = 0;

    [SerializeField] GameObject gameOverScreen;

    // Timer
    float timerSecLeft;
    float timerMaxSec;

    private void Start()
    {
        player = GetComponent<Player>();
        timerSlider.value = 1;
    }

    private void Update()
    {
        HandleTimer();
    }

    public void RemoveInstructions()
    {
        if (instructions != null)
        {
            instructions.enabled = false;
        }
    }

    public void AddPoint()
    {
        if (player.isGrounded)
        {
            rawScore += 1;
        }

        score.text = rawScore.ToString();
    }

    public void ShowGameOverScreen()
    {
        if (player.gameOver)
        {
            gameOverScreen.SetActive(true);
        }
    }

    private void HandleTimer()
    {
        if (timerMaxSec == 0) return;

        timerSecLeft -= Time.deltaTime;

        if (timerSecLeft <= 0)
        {
            player.SetGameOver();

            timerMaxSec = 0;
            timerSecLeft = 0;
            timerSlider.value = 0;

            return;
        }

        timerSlider.value = timerSecLeft / timerMaxSec;
    }

    public void StartTimer(float sec)
    {
        timerMaxSec = sec;
        timerSecLeft = sec;
    }
}
