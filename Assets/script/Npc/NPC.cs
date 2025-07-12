using Fungus;
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

        public bool playerinarea;

        private void Awake()
        {
            flowchart = GetComponent<Flowchart>();
            // 初始化狀態機
            stateMachine = new StateMachine();
            Quest_before = new NPC_Quest_before(this, stateMachine, "任務前");
            Questing = new NPC_Questing(this, stateMachine, "任務中");
            Quest_finish = new NPC_Quest_finish(this, stateMachine, "任務完成");
            // 設定預設狀態   
            stateMachine.DefaultState(Quest_before);
        }
        private void Update()
        {
            // 更新狀態機
            stateMachine.UpdateState();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //如果碰到的物件標籤是玩家 就 設定玩家在區域內
            if (collision.CompareTag("Player"))
            {
                playerinarea = true;
                 
               
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            //如果碰到的物件標籤是玩家 就 設定玩家不在區域內
            if (collision.CompareTag("Player"))
            {
                playerinarea = false;
                
            }
        }
    }
}