using UnityEngine;
using TMPro;
using System.Collections;

namespace PPman
{
    public class QuestUIManager : MonoBehaviour
    {
        public static QuestUIManager Instance;

        [SerializeField] private TextMeshProUGUI messageText; // 任務提示用文字
        [SerializeField] private CanvasGroup messageGroup;    // 控制顯示/隱藏
        [SerializeField] private float fadeDuration = 0.3f;   // 淡入淡出時間
        [SerializeField] private float stayDuration = 1;     // 停留時間

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            messageGroup.alpha = 0; // 預設隱藏
        }

        public void ShowMessage(string msg)
        {
            StopAllCoroutines();
            StartCoroutine(ShowMessageRoutine(msg));
        }

        private IEnumerator ShowMessageRoutine(string msg)
        {
            messageText.text = msg;

            // 淡入
            yield return StartCoroutine(FadeCanvasGroup(messageGroup, 0, 1, fadeDuration));
            // 停留
            yield return new WaitForSeconds(stayDuration);
            // 淡出
            yield return StartCoroutine(FadeCanvasGroup(messageGroup, 1, 0, fadeDuration));
        }

        private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
        {
            float t = 0;
            while (t < duration)
            {
                t += Time.deltaTime;
                cg.alpha = Mathf.Lerp(start, end, t / duration);
                yield return null;
            }
            cg.alpha = end;
        }
    }
}
