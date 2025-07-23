using Fungus;
using UnityEngine;
namespace PPman
{

    public class PlayerProximityUI : MonoBehaviour
    {
        public CanvasGroup targetGroup;              // 要顯示的 UI 群組
        public Vector3 offset;                       // UI 偏移
        public bool autoFollowUI = true;             // 是否自動更新位置
        private bool playerInRange = false;
        private WorktoUIpoint worktoUIpoint;         // 世界轉 UI 的工具
        public Flowchart flowchart; // Fungus 流程圖



        private void Start()
        {
            if (targetGroup == null)
            {
                targetGroup = GameObject.Find("群組_互動介面")?.GetComponent<CanvasGroup>();
            }
            flowchart = GetComponent<Flowchart>();
            worktoUIpoint = targetGroup?.GetComponent<WorktoUIpoint>();

        }

        private void Update()
        {
            if (playerInRange && autoFollowUI && worktoUIpoint != null)
            {
                worktoUIpoint.Updatepoint(transform, offset);
            }

            if (playerInRange && Input.GetKeyDown(KeyCode.F))
            {
                flowchart.SendFungusMessage("觸發對話");
            }

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                playerInRange = true;
                StopAllCoroutines();
                StartCoroutine(FadeSystem.Fade(targetGroup, true));
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                playerInRange = false;
                StopAllCoroutines();
                StartCoroutine(FadeSystem.Fade(targetGroup, false));
            }
        }
    }
}