﻿using UnityEngine;

namespace EcsLiteTestProject
{
    public static class Constants
    {
        public const float MaxRaycastDistance = 100f;
        
        public const float DefaultMovementSpeed = 5f;
        public const float DefaultTurnSpeed = 10f;

        public static class Tags
        {
            public const string Player = "Player";
            public const string Button = "Button";
        }

        public static class LayerMasks
        {
            public const string Ground = "Ground";
        }
        
        public static class AnimatorHashes
        {
            public static readonly int SpeedHash = Animator.StringToHash("Speed");
        }
    }
}