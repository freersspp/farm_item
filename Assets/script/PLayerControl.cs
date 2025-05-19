using UnityEngine;

public class PLayerControl : MonoBehaviour
{
    [Header("°ò¥»±±¨î")]
    [SerializeField, Range(0, 20)] private float speed = 5f;
    [SerializeField, Range(0, 20)] private float jumpForce = 5f;
    [SerializeField] private Animator ani;


}
