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

        [SerializeField] ActionChosenEvent actionChosenEvent;
        [SerializeField] float lanesDistance = 1f;
        [SerializeField] RuntimeGameData runtimeGameData;

        public int CurrentLane { get; private set; } = 0;

        private void Awake()
        {
            Assert.IsNotNull(robot);
            Assert.IsNotNull(actionChosenEvent);

            actionChosenEvent.OnEventRaised += OnLaneChanged;
        }

        private void Start()
        {
            OnLaneChanged(Action.Type.Left);
        }

        private void OnDestroy()
        {
            actionChosenEvent.OnEventRaised -= OnLaneChanged;
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

        private void OnLaneChanged(Action.Type actionType)
        {
            if (runtimeGameData.Energy == 0)
                return;

            if (actionType == Action.Type.Left)
                CurrentLane--;
            else if (actionType == Action.Type.Right)
                CurrentLane++;

            CurrentLane = Mathf.Clamp(CurrentLane, 0, MaxLanes - 1);

            robot.transform.position = new Vector3(
                GetLaneX(CurrentLane),
                robot.transform.position.y);
        }
    }
}
