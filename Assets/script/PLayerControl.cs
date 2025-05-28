using UnityEngine;

public class PLayerControl : MonoBehaviour
{
    [Header("�򥻱���")]
    [SerializeField, Range(0, 20)] private float movespeed = 5f;
    [SerializeField, Range(0, 20)] private float jumpForce = 10f;
    [SerializeField] private Animator ani;
    [SerializeField] private Rigidbody2D rig;
    [Header("�ˬd�a�O")]
    [SerializeField] private Vector3 �T�{�a�O�ؤo = Vector3.one;
    [SerializeField] private Vector3 �T�{�a�O�ؤo����m;
    [SerializeField] private LayerMask �i�H���o�ϼh;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + �T�{�a�O�ؤo����m, �T�{�a�O�ؤo);
    }


    private void Update()
    {
        float h = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(h * movespeed, rig.velocity.y);

        ani.SetFloat("����", Mathf.Abs(h));

        // ���]�w���L�ȽT�{�O�_���b�a�W, ��ݭn�ɤW(�y��, �ؤo, ����, �ϼh)
        bool �T�{�b�a�O�W = Physics2D.OverlapBox(transform.position + �T�{�a�O�ؤo����m, �T�{�a�O�ؤo, 0, �i�H���o�ϼh);
        
        ani.SetBool("�O�_�b�a�O�W", �T�{�b�a�O�W);
        ani.SetFloat("���D", rig.velocity.y);

        //�p�G���b�a�W�M���U�ť���N�i�H��
        if (�T�{�b�a�O�W && Input.GetKeyDown(KeyCode.Space))
        {  
            rig.velocity = new ( 0, jumpForce);
        }


        //�p�G h(��������)�������<0.1�N ���X
        if (Mathf.Abs(h) < 0.1)
        {
            return;
        }

        float �}�⨤�� = h > 0 ? 0 : 180;
        transform.eulerAngles = new Vector3(0, �}�⨤��, 0);
    }


}