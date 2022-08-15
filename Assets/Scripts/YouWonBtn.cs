using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility.Events;

namespace Wowie4
{
    public class YouWonBtn : MonoBehaviour
    {
        [SerializeField] VoidEvent gameWon;
        [SerializeField] CanvasGroup canvasGroup;
        [SerializeField] UnityEngine.UI.Button button;

        private void Awake()
        {
            gameWon.OnEventRaised += PlayerDied_OnEventRaised;
        }

        private void OnDestroy()
        {
            gameWon.OnEventRaised -= PlayerDied_OnEventRaised;
        }

        private void PlayerDied_OnEventRaised()
        {
            button.enabled = true;
            canvasGroup.DOFade(1f, 2f).SetEase(Ease.InOutSine);
            button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
        }
    }
}
