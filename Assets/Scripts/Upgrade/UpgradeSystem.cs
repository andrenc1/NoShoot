using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{
    public static UpgradeSystem instace;

    [Header("Data")]
    public List<UpgradeSO> upgrades = new List<UpgradeSO>();

    private WeaponController _weaponController;
    private PlayerXP _playerXP;
    private PlayerHealth _playerHealth;

    [Header("UI")]
    public GameObject panel;
    public UpgradeSlot slot1;
    public UpgradeSlot slot2;
    public UpgradeSlot slot3;


    private void Awake()
    {
        instace = this;

        panel.SetActive(false);
    }

    private void Start()
    {
        _weaponController = PlayerController.Instance.GetComponent<WeaponController>();
        _playerXP = PlayerController.Instance.GetComponent<PlayerXP>();
        _playerHealth = PlayerController.Instance.GetComponent<PlayerHealth>();

    }

    public void Open()
    {
        Time.timeScale = 0f;
        panel.SetActive(true);


        var options = PickUpgrades();
        slot1.SetUpgradeInfo(options[0]);
        slot2.SetUpgradeInfo(options[1]);
        slot3.SetUpgradeInfo(options[2]);

       
    }

    public void Close()
    {
        Time.timeScale = 1f;
        panel.SetActive(false);
    }

    public void Apply(UpgradeSO upgrade)
    {
        if (upgrade == null) return;

        switch (upgrade.type)
        {
            case UpgradeType.Heal:
                _playerHealth.Heal(upgrade.value);
                break;
            case UpgradeType.MaxHP:
                _playerHealth.AddMaxHealth(upgrade.value);
                break;
            case UpgradeType.WeaponCooldown:
                _weaponController.UpgradeWeaponCooldown(upgrade.TargetWeapon, upgrade.value);
                break;
            case UpgradeType.WeaponDamage:
                _weaponController.UpgradeWeaponDamage(upgrade.TargetWeapon, upgrade.value);
                break;
            case UpgradeType.XPGain:
                _playerXP.xpGainMult *= (1f + upgrade.value);
                break;


        }
    }


    List<UpgradeSO> PickUpgrades()
    {
        List<UpgradeSO> pool = new List<UpgradeSO>();

        foreach (var upd in upgrades)
        {
            if (upd == null) continue;
            
           bool isWeaponUpgrade = upd.type == UpgradeType.WeaponDamage || upd.type == UpgradeType.WeaponCooldown;

           if (isWeaponUpgrade && upd.TargetWeapon != null && !_weaponController.HasWeapon(upd.TargetWeapon))
           {
                continue;
            }

           pool.Add(upd);
        }
        
        List<UpgradeSO> result = new List<UpgradeSO>();
        for (int i = 0; i<3; i++)
        {
            if (pool.Count == 0)
            {
                result.Add((upgrades[0]));
                continue;
            }

            int randomUpgrades = Random.Range(0, pool.Count);
            result.Add(pool[randomUpgrades]);
            pool.RemoveAt(randomUpgrades);
        }

        return result;

    }

}
