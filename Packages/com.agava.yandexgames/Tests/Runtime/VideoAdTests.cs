using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace YandexGames.Tests
{
    public class VideoAdTests
    {
        [UnityTest]
        public IEnumerator VideoAdShouldInvokeErrorCallback()
        {
            while (!YandexGamesSdk.VerifyInitialization())
                yield return null;

            bool callbackInvoked = false;
            VideoAd.Show(onErrorCallback: (message) => {
                callbackInvoked = true;
            });

            yield return new WaitForSecondsRealtime(1);

            Assert.IsTrue(callbackInvoked);
        }
    }
}
