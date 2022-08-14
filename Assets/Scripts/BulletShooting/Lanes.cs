using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Utility.Events;

namespace Wowie4
{
    public class Lanes : MonoBehaviour
    {
        [SerializeField, Range(1,3)] int lanes = 3;
        public int MaxLanes => lanes;

        [SerializeField] ShootingRobot robot;

        [SerializeField] IntEvent laneChangedEvent;
        [SerializeField] float lanesDistance = 1f;

        public int CurrentLane { get; private set; } = 0;

        private void Awake()
        {
            Assert.IsNotNull(robot);
            Assert.IsNotNull(laneChangedEvent);

            laneChangedEvent.OnEventRaised += OnLaneChanged;
        }

        private void Start()
        {
            OnLaneChanged(1);
        }

        private void OnDestroy()
        {
            laneChangedEvent.OnEventRaised -= OnLaneChanged;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            for(int i = 0; i < lanes; i++)
            {
                float xPos = GetLaneX(i);
                Gizmos.DrawLine(new Vector3(xPos, -10), new Vector3(xPos, 10));
            }
        }

        public float GetCurrentLaneX() => GetLaneX(CurrentLane);

        public float GetLaneX(int lane)
        {
            var firstLaneX = transform.position.x - (lanes * lanesDistance) / 2f;
            return firstLaneX + lane * lanesDistance;
        }

        private void OnLaneChanged(int lane)
        {
            if (lane < 0 || lane >= lanes)
                return;

            this.CurrentLane = lane;

            robot.transform.position = new Vector3(
                GetLaneX(lane),
                robot.transform.position.y);
        }
    }
}
