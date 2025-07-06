using UnityEngine;

public class HealthMenager : MonoBehaviour
{
    [SerializeField] private float _startHealth;
    public float Health {  get; private set; }

    private void Awake()
    {
        Health = _startHealth;   
    }

    public void SetDamage(float damage)
    {
        Health -= damage;
        Debug.Log("Health " + Health);
    }
}
