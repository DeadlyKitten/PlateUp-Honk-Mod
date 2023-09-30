using BepInEx;
using HarmonyLib;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

namespace PlateUpHonk
{
    [BepInPlugin("com.steven.plateup.honk", "Honk Mod", "1.0.0")]
    public class HonkPlugin : BaseUnityPlugin
    {
        private static AudioClip _honkClip;
        private static HonkPlugin Instance;

        private void Awake()
        {
            Instance = this;

            LoadAudioClip();

            var harmony = new Harmony("com.steven.plateup.honk");
            harmony.PatchAll();
        }

        private void LoadAudioClip()
        {
            var tempPath = Path.Combine(Path.GetTempPath(), "honk.mp3");

            var assembly = Assembly.GetExecutingAssembly();
            using (var resource = assembly.GetManifestResourceStream("PlateUpHonk.Resources.Honk.mp3"))
            {
                if (resource == null) return;

                using (var file = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
                {
                    try
                    {
                        resource.CopyTo(file);
                    }
                    catch (Exception exception)
                    {
                        Logger.LogError("Error writing audio file");
                        Logger.LogError(exception.Message);
                        Logger.LogError(exception.StackTrace);
                    }
                }
            }

            StartCoroutine(LoadClip(tempPath));
        }

        private IEnumerator LoadClip(string filePath)
        {
            var loader = UnityWebRequestMultimedia.GetAudioClip(filePath, AudioType.MPEG);
            yield return loader.SendWebRequest();

            if (loader.error != null)
            {
                Debug.LogError($"Error loading clip from path: {filePath}\n{loader.error}");
                Debug.LogError(loader.error);
                yield break;
            }

            _honkClip = DownloadHandlerAudioClip.GetContent(loader);

            try
            {
                File.Delete(filePath);
            }
            catch (Exception exception)
            {
                Logger.LogError("Failed to delete temporary file.");
                Logger.LogError(exception.Message);
                Logger.LogError(exception.StackTrace);
            }
        }

        public static void PlayHonk(Vector3 location)
        {
            if (_honkClip == null)
            {
                Instance.Logger.LogError("AudioClip is null!");
                return;
            }
            AudioSource.PlayClipAtPoint(_honkClip, location);
        }
    }
}
