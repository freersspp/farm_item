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
        }
    }
}