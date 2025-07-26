using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 會在角色切換時更改這個 target
    public Vector3 offset;
    public float followSpeed = 5f;

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
