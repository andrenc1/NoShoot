using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;
using System;

public class WeaponController : MonoBehaviour
{

    [Serializable]
    public class WeaponSlot
    {
        public Weapon weapon;
        [HideInInspector]
        public float timer;

        [Header("Upgrades")]

        public float cooldownMult = 1f;
        public float damagedMult = 1f;
    }

    public List<WeaponSlot> weapons = new List<WeaponSlot>();


    //public Weapon weapon;

    //private float timer;

    private void Update()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            WeaponSlot w = weapons[i];
            if (w.weapon == null) continue;

            w.timer += Time.deltaTime;

            float finalCooldown = w.weapon.cooldown * w.cooldownMult; 

            if (w.timer >= finalCooldown)
            {
                Attack(w);
                w.timer = 0f;
            }
        }
    }

    void Attack(WeaponSlot w)
    {
        int finalDamage = Mathf.RoundToInt(w.weapon.damage * w.damagedMult);

        GameObject wp = Instantiate(w.weapon.attackPrefab, transform.position, Quaternion.identity);
        wp.GetComponent<AttackArea>().damageDealer.GetDamage(finalDamage);
    }

    //void Attack()
    //{
    //        GameObject wp = Instantiate(weapon.attackPrefab, transform.position, Quaternion.identity); 
    //        wp.GetComponent<AttackArea>().damageDealer.GetDamage(weapon.damage);
    //}

    public bool HasWeapon(Weapon weapon)
    {
        return GetSlot(weapon) != null;

    }

    public void UpgradeWeaponDamage(Weapon weapon, float percent)
    {
        WeaponSlot slot = GetSlot(weapon);
        if (slot == null)
        {
            return;
        }
        slot.damagedMult *= (1f + percent);
    }

    public void UpgradeWeaponCooldown(Weapon weapon, float percent)
    {
        WeaponSlot slot = GetSlot(weapon);

        if (slot == null)
        {
            return;
        }

        slot.cooldownMult *= (1f - percent);
        slot.cooldownMult = MathF.Max(0.1f, slot.cooldownMult);
    }

    WeaponSlot GetSlot(Weapon weapon)
    {
        for (int i = 0; i < weapons.Count; i++) {
            if (weapons[i].weapon == weapon)
            {
                return weapons[i];
            }
        }
        return null;
    }
}