using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    Player player;

    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI instructions;
    int rawScore = 0;

    [SerializeField] Canvas gameOverScreen;

    private void Start()
    {
        player = GetComponent<Player>();

        AddPoint();
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
            gameOverScreen.enabled = true;
        }
    }
}
