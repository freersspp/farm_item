using UnityEngine;
namespace PPman
{
    /// <summary>
    /// 攻擊值:攻擊相關設定在這
    /// </summary>
    public class AttackValue : MonoBehaviour
    {
        [field:SerializeField, Header("攻擊力"), Range(0, 100)]public float attackPower { get; private set; } = 5;

    }
}