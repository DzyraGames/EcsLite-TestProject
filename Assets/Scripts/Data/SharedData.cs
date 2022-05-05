﻿using EcsLiteTestProject.Data;
using SevenBoldPencil.EasyEvents;
using UnityEngine;

namespace EcsLiteTestProject
{
    public class SharedData
    {
        public EventsBus EventsBus;
        
        public Transform PlayerTransform;
        public Animator PlayerAnimator;
        public PlayerData PlayerData;
    }
}