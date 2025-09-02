using Fungus;
using System.Collections;
using UnityEngine;

namespace PPman
{
    /// <summary>
    /// NPC 腳本 : 管理 NPC資料與行為 
    /// </summary>
    public class NPC : MonoBehaviour
    {
        public StateMachine stateMachine;  // 狀態機
        public NPC_Quest_before Quest_before { get; private set; } // 任務前狀態
        public NPC_Questing Questing { get; private set; } // 任務中狀態
        public NPC_Quest_finish Quest_finish { get; private set; } // 任務完成狀態

        public Flowchart flowchart { get; private set; }// Fungus 流程圖

        public bool playerinarea { get; private set; }
        public bool TalkingorNot { get; set; }
        public int 手上任務物品數量 { get; set; } // 玩家手上任務物品數量
        public int 任務物品需要數量 { get; set; } = 5; // 任務物品需要數量

        [SerializeField] private Vector3 offsetinteraction; // 互動介面偏移量

        private CanvasGroup groupinteraction; // 互動介面群組
        private WorktoUIpoint worktoUIpoint; // 世界座標轉介面座標
        private Transform player; // 玩家物件
        private Transform npc;

        protected virtual void Awake()
        {
            flowchart = GetComponent<Flowchart>();
            groupinteraction = GameObject.Find("群組_互動介面").GetComponent<CanvasGroup>();
            worktoUIpoint = GameObject.Find("群組_互動介面").GetComponent<WorktoUIpoint>();
            player = GameObject.Find("主角").transform; // 獲取玩家物件
            npc = GameObject.Find("NPC").transform; // 獲取NPC物件

            // 初始化狀態機
            stateMachine = new StateMachine();
            Quest_before = new NPC_Quest_before(this, stateMachine, "任務前");
            Questing = new NPC_Questing(this, stateMachine, "任務中");
            Quest_finish = new NPC_Quest_finish(this, stateMachine, "任務完成");
            // 設定預設狀態   
            stateMachine.DefaultState(Quest_before);
        }
        protected virtual void Update()
        {
#if UNITY_EDITOR
            Test();
#endif
            // 更新狀態機
            stateMachine.UpdateState();
            // 如果玩家在互動區域內，則更新互動介面位置
            if (playerinarea)
            {
                worktoUIpoint.Updatepoint(transform, offsetinteraction);
            }
            //如果玩家跑到另一側就轉身
            float direction = player.position.x > npc.transform.position.x ? 1 : -1;
            Flip(direction);
        }




        private void OnTriggerEnter2D(Collider2D collision)
        {
            //如果碰到的物件標籤是玩家 就 設定玩家在區域內
            if (collision.CompareTag("Player"))
            {
                playerinarea = true;
                SwitchinteractionUI(playerinarea);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            //如果碰到的物件標籤是玩家 就 設定玩家不在區域內
            if (collision.CompareTag("Player"))
            {
                playerinarea = false;
                SwitchinteractionUI(playerinarea);

            }
        }
        /// <summary>
        /// 切換互動介面(驚嘆號)的顯示狀態
        /// </summary>
        /// <param name="fadein">淡入效果</param>
        public void SwitchinteractionUI(bool fadein)
        {
            //如果此物件不再階層面板上(停止遊戲或是刪除時)就跳出
            if(!gameObject.activeInHierarchy)
            {
                return;
            } 

            StopAllCoroutines(); // 停止所有協程，避免重複啟動淡入淡出效果
            StartCoroutine(FadeSystem.Fade(groupinteraction, fadein));
        }

        public void Flip(float h)
        {
            if (Mathf.Abs(h) < 0.1)
            {
                return;
            }

            float 腳色角度 = h > 0 ? 0 : 180;
            transform.eulerAngles = new Vector3(0, 腳色角度, 0);
        }

        public void GetItem()
        {
            手上任務物品數量++;
            Debug.Log($"{name}手上任務物品數量: {手上任務物品數量}");

            // 顯示 UI 提示
            if (QuestUIManager.Instance != null)
            {
                QuestUIManager.Instance.ShowMessage($"已獲得 {手上任務物品數量} 個任務道具");
            }
        }


#if UNITY_EDITOR
        private void Test()
        {
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                手上任務物品數量++;
            }
        }
#endif
    }
}