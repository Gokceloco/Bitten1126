using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public Transform fillBarPivot;
    public void SetHealth(float ratio)
    {
        fillBarPivot.localScale = new Vector3(ratio, 1, 1);
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + Vector3.back);
    }
}
