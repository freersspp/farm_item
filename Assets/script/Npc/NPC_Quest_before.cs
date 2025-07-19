using UnityEngine;
namespace PPman
{
    /// <summary>
    /// NPC_Quest_before : NPC的任務前狀態
    /// </summary>
    public class NPC_Quest_before : NPC_State
    {
        public NPC_Quest_before(NPC _npc, StateMachine _statemachine, string _name) : base(_npc, _statemachine, _name)
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
            // 如果玩家在區域內 且 按下"F"就通知fungus開始流程
            if (npc.playerinarea && Input.GetKeyDown(KeyCode.F))
            {
                npc.flowchart.SendFungusMessage("任務前");
            }
            if(npc.TalkingorNot)
            {
                stateMachine.SwitchState(npc.Questing); // 切換到任務中狀態
            }


        }
    }
}
