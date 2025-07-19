using UnityEngine;
namespace PPman
{
    /// <summary>
    /// NPC_Quest_finish : NPC的任務完成狀態
    /// </summary>

    public class NPC_Quest_finish : NPC_State
    {
        public NPC_Quest_finish(NPC _npc, StateMachine _statemachine, string _name) : base(_npc, _statemachine, _name)
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
                npc.flowchart.SendFungusMessage("任務完成");
            }
        }
    }
}
