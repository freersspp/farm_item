using UnityEngine;
namespace PPman
{
    /// <summary>
    /// 儲存玩家資料與基本功能
    /// </summary>
    public class Player : MonoBehaviour
    {
        [Header("基本控制")]
        [SerializeField, Range(0, 20)] private float movespeed = 5f;
        [SerializeField, Range(0, 20)] private float jumpForce = 10f;
        [SerializeField] private Animator ani;
        [SerializeField] private Rigidbody2D rig;
        [Header("檢查地板")]
        [SerializeField] private Vector3 確認地板尺寸 = Vector3.one;
        [SerializeField] private Vector3 確認地板尺寸的位置;
        [SerializeField] private LayerMask 可以跳得圖層;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + 確認地板尺寸的位置, 確認地板尺寸);
        }

    }

}
