﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ggj
{ 
    public static class MonoExtensions
    {
        public static T Get<T>(this MonoBehaviour mb) where T : new()
        {
            return GameManager.Instance.Services.Get<T>();
        }

        public static T TryGet<T>(this MonoBehaviour mb) where T : new()
        {
            return GameManager.Instance.Services.TryGet<T>();
        }

        public static void GetAsync<T>(this MonoBehaviour mb, Action<T> onLoad) where T : new()
        {
            GameManager.Instance.Services.GetAsync(onLoad);
        }
    }
}