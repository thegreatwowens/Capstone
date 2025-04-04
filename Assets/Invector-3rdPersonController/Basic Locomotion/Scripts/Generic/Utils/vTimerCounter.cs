﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Invector.Utils
{
    public class vTimerCounter : MonoBehaviour
    {
        public float targetTime;
        public bool normalizeResult;
        [SerializeField,vReadOnly]
        protected float timerResult = 0;

        public UnityEngine.Events.UnityEvent onStart, onPause, onStop, onFinish;
        public UnityEngine.UI.Slider.SliderEvent onTimerUpdated;

       public float currentTime;
        protected Coroutine timerRoutine;
        public virtual void StartTimer()
        {
            if (timerRoutine != null) StopCoroutine(timerRoutine);
            timerRoutine = StartCoroutine(TimerRoutiner());
        }

        public void StopTimer()
        {
            PauseTimer();
            currentTime = 0;
            onStop.Invoke();
            timerResult = 0;
            onTimerUpdated.Invoke(0);
        }

        public void PauseTimer()
        {
            if (timerRoutine != null) StopCoroutine(timerRoutine);
            timerRoutine = null;
            onPause.Invoke();
        }

        IEnumerator TimerRoutiner()
        {
            onStart.Invoke();
          
            while (currentTime < targetTime)
            {               
                currentTime += Time.deltaTime;
                timerResult = normalizeResult? currentTime/targetTime: currentTime;
                onTimerUpdated.Invoke(timerResult);
                yield return null;
            }
            timerRoutine = null;
            timerResult = normalizeResult ? 1 : targetTime;
            onTimerUpdated.Invoke(timerResult);
            onFinish.Invoke();
          
        }
    }
}