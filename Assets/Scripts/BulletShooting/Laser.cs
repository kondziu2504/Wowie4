using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Assertions;

namespace Wowie4
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] LineRenderer lineRenderer;

        float originalWidth;
        Tween laserTween;

        private void Awake()
        {
            Assert.IsNotNull(lineRenderer);
            originalWidth = lineRenderer.startWidth;
            gameObject.SetActive(false);
        }

        public void Shoot(Vector2 startPoint, Vector2 endPoint, Color color)
        {
            gameObject.SetActive(true);

            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, endPoint);

            SetColor(color);
            SetWidth(originalWidth);

            float width = lineRenderer.startWidth;
            laserTween?.Kill();
            laserTween = DOTween.To(() => width, (val) => width = val, 0f, 0.5f)
                .OnUpdate(() => SetWidth(width))
                .OnComplete(() => gameObject.SetActive(false));
        }

        private void SetWidth(float width)
        {
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
        }

        private void SetColor(Color color)
        {
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
        }
    }
}
