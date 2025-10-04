using TMPro;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float _attackCooldown;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject[] _arrows;

    private float _cooldownTimer;
	public void Update()
	{
        _cooldownTimer += Time.deltaTime; 
        if (_cooldownTimer >= _attackCooldown)
            Attack();
	}
    private void Attack()
    {
        _cooldownTimer = 0;
        _arrows[FindArrows()].transform.position = _firePoint.position;
        _arrows[FindArrows()]
            .GetComponent<EnemyProjectile>()
            .ActiveProjectile();

	}

    private int FindArrows()
    {
        for (int i = 0; i < _arrows.Length; i++)
        {
            if (!_arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
