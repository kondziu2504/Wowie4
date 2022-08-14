using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Utility.Events;

namespace Wowie4
{
    public class Score : MonoBehaviour
    {
        [SerializeField] int score;
        [SerializeField] TMPro.TextMeshProUGUI scoreText;

        [Header("Events")]
        [SerializeField] VoidEvent goodCodeEaten;
        [SerializeField] VoidEvent neutralCodePassed;

        private void Awake()
        {
            Assert.IsNotNull(scoreText);

            goodCodeEaten.OnEventRaised += AddScore;
            neutralCodePassed.OnEventRaised += AddScore;
        }

        private void AddScore()
        {
            score++;
            scoreText.text = $"Score: {score}";
        }

    }
}
