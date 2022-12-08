using System;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace TigerFrogGames
{
    public static class AudioPreviewer
    {
        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceId, int line)
        {
            UnityEngine.Object obj = EditorUtility.InstanceIDToObject(instanceId);

            Debug.Log("Asset was oppened");

            if (obj is AudioClip audioClip)
            {
                PlayPreviewClip(audioClip);
                return true;
            }

            return false;
        }

        public static void PlayPreviewClip(AudioClip audioClip)
        {
            Assembly unityAssembly = typeof(AudioImporter).Assembly;
            Type audioUtil = unityAssembly.GetType("UnityEditor.AudioUtil");
            MethodInfo methodInfo = audioUtil.GetMethod(
                "PlayPreviewClip",
                BindingFlags.Static | BindingFlags.Public,
                null,
                new System.Type[] { typeof(AudioClip), typeof(Int32), typeof(Boolean) },
                null);
            methodInfo.Invoke(null, new object[] { audioClip, 0, false });
        }

    }
}
 
