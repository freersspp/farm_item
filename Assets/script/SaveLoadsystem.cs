using ppman;
using UnityEngine;

namespace PPman
{
    /// <summary>
    /// 儲存與讀取系統
    /// </summary>

    public class SaveLoadsystem : MonoBehaviour
    {
        #region 單例模式
        private static SaveLoadsystem _instance;
        public static SaveLoadsystem instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SaveLoadsystem>();
                }
                return _instance;
            }
        }
        #endregion
        [SerializeField] private Player_data playerData;
        private const string DataName = "遊戲儲存資料"; // 存檔的鍵值
        private Transform playerTransform;
        private Player player;
        private NPC_knight npc_Knight;

        private void Awake()
        {
            playerTransform = GameObject.Find(GameManager.PlayerName).transform;
            player = GameObject.Find(GameManager.PlayerName).GetComponent<Player>();
            npc_Knight = GameObject.Find(GameManager.NPCName).GetComponent<NPC_knight>();
        }

        private void Start()
        {
            //Savedata();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1)) Savedata();
            if (Input.GetKeyDown(KeyCode.F2)) Loaddata();
        }

        /// <summary>
        /// 存檔
        /// </summary>
        public void Savedata()
        {
            playerData.position = playerTransform.position;
            playerData.rotation = playerTransform.eulerAngles;
            playerData.hpmax = player.HPmaxdata;
            playerData.hp = player.HPdata;
            playerData.missionCountNPC = npc_Knight.手上任務物品數量;

            //存檔格式為json(文字格式)
            var json = JsonUtility.ToJson(playerData);

            //將轉為json的資料存到本地端, 名稱為"遊戲儲存資料"
            PlayerPrefs.SetString(DataName, json);
        }

        public void Loaddata()
        {
            //讀取本地端的存檔資料
            var json = PlayerPrefs.GetString(DataName);
            //將json格式的資料轉回Player_data格式
            playerData = JsonUtility.FromJson<Player_data>(json);
            //將讀取到的資料套用到玩家
            playerTransform.position = playerData.position;
            playerTransform.eulerAngles = playerData.rotation;
            player.LoadHPUpdateUI(playerData.hpmax, playerData.hp);
            npc_Knight.LoadCount(playerData.missionCountNPC);
        }

    }
}
