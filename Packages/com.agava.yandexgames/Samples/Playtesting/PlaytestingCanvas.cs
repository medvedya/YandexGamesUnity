#pragma warning disable

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YandexGames.Samples
{
    public class PlaytestingCanvas : MonoBehaviour
    {
        [SerializeField]
        private Text _authorizationStatusText;

        [SerializeField]
        private Text _profileDataPermissionStatusText;

        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield break;
#endif

            // Always wait for it if invoking something immediately in the first scene.
            yield return YandexGamesSdk.WaitForInitialization();

            //// Avoid unexpected authorization window popup that will freak out the user.
            //if (PlayerAccount.IsAuthorized)
            //{
            //    // Authenticate silently without requesting photo and real name permissions.
            //    PlayerAccount.Authenticate(false);
            //}

            while (true)
            {
                _authorizationStatusText.color = PlayerAccount.Authorized ? Color.green : Color.red;

                if (PlayerAccount.Authorized)
                    _profileDataPermissionStatusText.color = PlayerAccount.HasProfileDataPermission ? Color.green : Color.red;

                yield return new WaitForSecondsRealtime(0.25f);
            }
        }

        public void OnShowInterestialButtonClick()
        {
            InterestialAd.Show();
        }

        public void OnShowVideoButtonClick()
        {
            VideoAd.Show();
        }

        public void OnRequestProfileDataPermissionButtonClick()
        {
            PlayerAccount.RequestProfileDataPermission();
        }

        public void OnSetLeaderboardScoreButtonClick()
        {
            Leaderboard.SetScore("PlaytestBoard", Random.Range(1, 100));
        }

        public void OnGetLeaderboardEntriesButtonClick()
        {
            Leaderboard.GetEntries("PlaytestBoard", (result) =>
            {
                // Use it
                Debug.Log($"My rank = {result.userRank}");
                foreach (var entry in result.entries)
                {
                    Debug.Log(entry.player.publicName + " " + entry.score);
                }
            });
        }
    }
}
