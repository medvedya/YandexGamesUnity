using System.Collections;
using System.Runtime.InteropServices;
#if UNITY_WEBGL && !UNITY_EDITOR
using UnityEngine;
using UnityEngine.Scripting;

[assembly: AlwaysLinkAssembly]
#endif
namespace YandexGames
{
    public static class YandexGamesSdk
    {
        /// <summary>
        /// Enable it to log SDK callbacks in the console.
        /// </summary>
        public static bool CallbackLogging = false;

        /// <summary>
        /// Think of this as a static constructor.
        /// </summary>
#if UNITY_WEBGL && !UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
#endif
        [DllImport("__Internal")]
        private static extern bool Initialize();

        /// <summary>
        /// Coroutine waiting for <see cref="IsInitialized"/> to return true.
        /// </summary>
        public static IEnumerator WaitForInitialization()
        {
            while (!IsInitialized)
                yield return null;
        }

        /// <summary>
        /// Leaderboard is initialized automatically on load.
        /// If either something fails or called way too early, this will return false.
        /// </summary>
        public static bool IsInitialized => VerifySdkInitialization();

        [DllImport("__Internal")]
        private static extern bool VerifySdkInitialization();
    }
}
