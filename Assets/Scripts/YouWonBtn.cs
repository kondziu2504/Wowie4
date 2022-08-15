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
            var seq = DOTween.Sequence();

            seq.Append(canvasGroup.DOFade(1f, 2f).SetEase(Ease.InOutSine));
            seq.AppendInterval(3f);
            seq.Append(canvasGroup.DOFade(0f, 2f).SetEase(Ease.InOutSine));

            seq.Play();
        }
    }
}
