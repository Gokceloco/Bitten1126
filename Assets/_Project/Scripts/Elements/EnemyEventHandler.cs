using UnityEngine;

public class EnemyEventHandler : MonoBehaviour
{
    public void TryHitPlayer()
    {
        GetComponentInParent<Enemy>().TryHitPlayer();
    }

    public void AttackStart()
    {
        GetComponentInParent<Enemy>().AttackStarted();
    }
}
