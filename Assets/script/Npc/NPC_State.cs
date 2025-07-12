using UnityEngine;

namespace PPman
{
    /// <summary>
    /// 儲存NPC資料與基本功能
    /// </summary>

    public class NPC_State : State
    {
        protected NPC npc;
        public NPC_State(NPC _npc, StateMachine _statemachine, string _name)
        {
            npc = _npc;
            stateMachine = _statemachine;
            name = _name;
        }

    }
}

 