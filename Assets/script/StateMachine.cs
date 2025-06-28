using UnityEngine;

namespace PPman
{
    /// <summary>
    /// 狀態機 : 預設狀態與狀態切換管理
    /// </summary>


    public class StateMachine
    {
        /// <summary>
        /// 當前狀態
        /// </summary>
        private State currentState;

        /// <summary>
        /// 指定預設狀態
        /// </summary>
        /// <param name="defaultState">要指定的預設狀態</param>
        public void DefaultState(State defaultState)
        {
            //當前狀態 指定 為預設狀態
            currentState = defaultState;
            //進入當前狀態
            currentState.Enter(); 
        }

        /// <summary>
        /// 更新當前狀態
        /// </summary>
        public void UpdateState()
        {
            //更新當前狀態
            currentState.Update();
        }

        /// <summary>
        /// 切換狀態
        /// </summary>
        /// <param name="newState">要切換的新狀態</param>
        public void SwitchState(State newState)
        {
            //先離開當前狀態
            currentState.Exit();
            //當前狀態更新為新狀態
            currentState = newState;
            //進入新狀態
            currentState.Enter();
        }


    }
}
 