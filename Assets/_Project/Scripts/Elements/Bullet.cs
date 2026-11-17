using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float maxDistance;

    private Vector3 _spawnPos;

    private void Start()
    {
        _spawnPos = transform.position;
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        var distance = (transform.position - _spawnPos).magnitude;

        if (distance > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().GetHit(1);
        }
        Destroy(gameObject);
    }
}
