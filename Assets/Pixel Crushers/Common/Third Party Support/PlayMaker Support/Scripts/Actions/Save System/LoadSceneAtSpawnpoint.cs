// Copyright (c) Pixel Crushers. All rights reserved.

using UnityEngine;
using HutongGames.PlayMaker;

namespace PixelCrushers.PlayMakerSupport
{

    [ActionCategory("Pixel Crushers Common")]
    [HutongGames.PlayMaker.Tooltip("Loads a scene and moves the player to a specified spawnpoint.")]
    public class LoadSceneAtSpawnpoint : FsmStateAction
    {

        [HutongGames.PlayMaker.Tooltip("Scene to load.")]
        public FsmString sceneName = new FsmString();

        [HutongGames.PlayMaker.Tooltip("Optional name of GameObject where player should appear.")]
        public FsmString spawnpoint = new FsmString();

        public override void Reset()
        {
            sceneName.Value = string.Empty;
            spawnpoint.Value = string.Empty;
        }

        public override void OnEnter()
        {
            var s = sceneName.Value;
            if (!string.IsNullOrEmpty(spawnpoint.Value)) s += "@" + spawnpoint.Value;
            SaveSystem.LoadScene(s);
            Finish();
        }

    }

}