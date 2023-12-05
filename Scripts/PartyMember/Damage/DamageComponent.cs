using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    private int Health;
    public int MaxHealth;

    public bool IsDead;

    public delegate void OnHealthChange();
    public event OnHealthChange onHealthChange;

    public delegate void OnDeath();
    public event OnDeath onDeath;

    public delegate void OnRevive();
    public event OnRevive onRevive;


    private void Awake() {
        Health = MaxHealth;
        IsDead = false;
    }

    public void TakeDamage(int damage)
    {
        if(!IsDead)
        {
            Health -= damage;
            if(Health <= 0)
            {
                Health = 0;
                IsDead = true;
                onDeath();
            }
            onHealthChange();

           
        }
    }

    public void GiveHealth(int amount)
    {
        Health += amount;
        if(Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        onHealthChange();
    }

    public void Revive()
    {
        GiveHealth(MaxHealth / 4);
        IsDead = false;
        onRevive();
    }

    public int GetHealth()
    {
        return Health;
    }

    public int GetMaxHealth()
    {
        return MaxHealth;
    }

    public bool IsAlive()
    {
        return Health > 0;
    }

}
