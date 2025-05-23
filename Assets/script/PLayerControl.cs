using UnityEngine;

public class PLayerControl : MonoBehaviour
{
    [Header("基本控制")]
    [SerializeField, Range(0, 20)] private float movespeed = 5f;
    [SerializeField, Range(0, 20)] private float jumpForce = 10f;
    [SerializeField] private Animator ani;
    [SerializeField] private Rigidbody2D rig;


    private void Update()
    {
       float h = Input.GetAxis("Horizontal");

       rig.velocity = new Vector2(h * movespeed, rig.velocity.y );

        ani.SetFloat("移動", Mathf.Abs(h));

          
        float 腳色角度 = h > 0 ? 0 : 180;
        transform.eulerAngles = new Vector3(0, 腳色角度, 0);
    }


}