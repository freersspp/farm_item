using UnityEngine;

public class PLayerControl : MonoBehaviour
{
    [Header("�򥻱���")]
    [SerializeField, Range(0, 20)] private float movespeed = 5f;
    [SerializeField, Range(0, 20)] private float jumpForce = 10f;
    [SerializeField] private Animator ani;
    [SerializeField] private Rigidbody2D rig;


    private void Update()
    {
       float h = Input.GetAxis("Horizontal");

       rig.velocity = new Vector2(h * movespeed, rig.velocity.y );

        ani.SetFloat("����", Mathf.Abs(h));

          
        float �}�⨤�� = h > 0 ? 0 : 180;
        transform.eulerAngles = new Vector3(0, �}�⨤��, 0);
    }


}