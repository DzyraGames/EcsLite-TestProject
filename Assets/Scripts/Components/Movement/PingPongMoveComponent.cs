using System;
using UnityEngine;

namespace EcsLiteTestProject
{
    [Serializable]
    public struct PingPongMoveComponent
    {
        public Vector3 StartPosition;
        public Vector3 EndPosition;
    }
}