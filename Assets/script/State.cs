using UnityEngine;

namespace PPman
{
    /// <summary>
    /// 狀態 : 包含進入、離開、更新等方法
    /// </summary>

    public class State
    {
        protected string name;
                 
        protected StateMachine stateMachine;
        protected float h;

        /// <summary>
        /// 計時器 : 用於計算狀態持續時間
        /// </summary>
        protected float timer;
             
        public virtual void Enter()
        {
            //Debug.Log($"<color=green>進入<{name}>狀態</color>");
            // 重置計時器
            timer = 0;
        }
        public virtual void Update()
        {
            //Debug.Log($"<color=blue>更新<{name}>狀態</color>");
            h = Input.GetAxis("Horizontal");
            timer += Time.deltaTime;

        }
        public virtual void Exit()
        {
            //Debug.Log($"<color=red>離開<{name}>狀態</color>");
        }




    }


}
