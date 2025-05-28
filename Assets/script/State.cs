using UnityEngine;

namespace PPman
{
    /// <summary>
    /// 狀態 : 包含進入、離開、更新等方法
    /// </summary>

    public class State
    {
        private string name;

        protected Player player;
        protected StateMachine stateMachine; 
        public void Enter()
        {
            Debug.Log($"<color=green>進入<{name}>狀態</color>");
        }
        public void Update()
        {
            Debug.Log($"<color=blue>更新<{name}>狀態</color>"); 
        }
        public void Exit()
        {
            Debug.Log($"<color=red>離開<{name}>狀態</color>");
        }




    }


}
