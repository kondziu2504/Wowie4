using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Events;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Wowie4
{
    public class PlayAgainBtn : MonoBehaviour
    {
        [SerializeField] VoidEvent playerDied;
        [SerializeField] CanvasGroup canvasGroup;
        [SerializeField] UnityEngine.UI.Button button;

        private void Awake()
        {
            playerDied.OnEventRaised += PlayerDied_OnEventRaised;
        }

        private void OnDestroy()
        {
            playerDied.OnEventRaised -= PlayerDied_OnEventRaised;
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
