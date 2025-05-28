using UnityEngine;

public class PLayerControl : MonoBehaviour
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


    private void Update()
    {
        float h = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(h * movespeed, rig.velocity.y);

        ani.SetFloat("移動", Mathf.Abs(h));

        // 先設定布林值確認是否有在地上, 後需要補上(座標, 尺寸, 角度, 圖層)
        bool 確認在地板上 = Physics2D.OverlapBox(transform.position + 確認地板尺寸的位置, 確認地板尺寸, 0, 可以跳得圖層);
        
        ani.SetBool("是否在地板上", 確認在地板上);
        ani.SetFloat("跳躍", rig.velocity.y);

        //如果有在地上和按下空白鍵就可以跳
        if (確認在地板上 && Input.GetKeyDown(KeyCode.Space))
        {  
            rig.velocity = new ( 0, jumpForce);
        }


        //如果 h(水平角度)取絕對值<0.1就 跳出
        if (Mathf.Abs(h) < 0.1)
        {
            return;
        }

        float 腳色角度 = h > 0 ? 0 : 180;
        transform.eulerAngles = new Vector3(0, 腳色角度, 0);
    }


}