using UnityEngine;
namespace PPman
{

    //enum列舉 : 下拉選單(預設是單選)
    //快速鍵 enum + Tab 兩下
    /// <summary>
    /// GameManager.PlayerName受傷音效
    /// GameManager.PlayerName死亡音效
    /// GameManager.PlayerName衝刺音效
    /// 劍氣音效
    /// 吃道具音效
    /// 掉落道具音效
    /// 攻擊1_2音效
    /// 攻擊3音效
    /// 敵人受傷音效
    /// 敵人攻擊_1音效
    /// 敵人攻擊_2音效
    /// 敵人死亡音效
    /// 落地音效
    /// 走路音效
    /// 跳躍音校
    /// </summary>
    public enum Soundtype
    {
        PlayerHurt, //GameManager.PlayerName受傷音效
        PlayerDie, //GameManager.PlayerName死亡音效
        PlayerDash, //GameManager.PlayerName衝刺音效
        skill2, //劍氣音效
        EatItem, //吃道具音效        
        DropItem, //掉落道具音效
        Attack1_2, //攻擊1_2音效
        Attack3, //攻擊3音效
        EnemyHurt, //敵人受傷音效
        EnemyAttack1, //敵人攻擊_1音效
        EnemyAttack2, //敵人攻擊_2音效
        EnemyDie, //敵人死亡音效
        Fall, //落地音效
        Walk, //走路音效
        Jump, //跳躍音效
    }
}