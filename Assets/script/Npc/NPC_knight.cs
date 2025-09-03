using UnityEngine;
namespace PPman
{

    public class NPC_knight : NPC
    {
        private static NPC_knight _instance;
        public static NPC_knight Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<NPC_knight>();                   
                }
                return _instance;
            }
        }

        public  void LoadCount(int count)
        {
            手上任務物品數量 = count;
        }

        
    }
}