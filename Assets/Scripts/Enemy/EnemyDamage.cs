using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float _damage;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player") ;
		{
			collision.GetComponent<Health>().TakeDamage(_damage);
		}
	}
}
