using UnityEngine;
namespace ppman
{
    /// <summary>
    /// 玩家數據(存讀檔使用)
    /// </summary>

    [System.Serializable]
    public class Player_data
    {
        public Vector3 position;
        public Vector3 rotation;
        public float hpmax;
        public float hp;
        public int missionCountNPC;

    }
}