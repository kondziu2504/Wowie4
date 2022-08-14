using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wowie4
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Elevator : MonoBehaviour
    {
        [SerializeField] float maxHeight = 2f;
        [SerializeField] float descendingSpeed = 1f;
        [SerializeField] float ascendingSpeed = 1f;

        private float originalHeight;

        private float BottomHeight => Application.isPlaying ? originalHeight : transform.position.y;
        private float TopHeight => BottomHeight + maxHeight;

        private bool descending = false;

        private void Awake()
        {
            originalHeight = transform.position.y;
        }

        private void Update()
        {
            if (!descending)
                Ascend();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawLine(
                new Vector3(transform.position.x, BottomHeight),
                new Vector3(transform.position.x, TopHeight));
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.transform.position.y > transform.position.y)
            {
                Descend();
                descending = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            descending = false;
        }

        private void Descend()
        {
            transform.position += Vector3.down * descendingSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, BottomHeight, TopHeight));
        }

        private void Ascend()
        {
            transform.position += Vector3.up * ascendingSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, BottomHeight, TopHeight));
        }
    }
}
