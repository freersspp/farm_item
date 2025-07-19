using UnityEngine;
namespace PPman
{
    /// <summary>
    /// NPC_Questing : NPC的任務中狀態
    /// </summary>

    public class NPC_Questing : NPC_State
    {
        public NPC_Questing(NPC _npc, StateMachine _statemachine, string _name) : base(_npc, _statemachine, _name)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (npc.playerinarea && Input.GetKeyDown(KeyCode.F))
            {
                npc.flowchart.SendFungusMessage("任務中");
            }
            //如果玩家 手上任務物品數量 >= 任務物品需要數量 就切換到任務完成
            if (npc.手上任務物品數量 >= npc.任務物品需要數量)
            {
                stateMachine.SwitchState(npc.Quest_finish); // 切換到任務完成狀態
            }

        }
    }
}