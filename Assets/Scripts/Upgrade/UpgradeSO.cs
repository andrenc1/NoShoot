using UnityEngine;
public enum UpgradeType
    {
        WeaponDamage,
        WeaponCooldown,
        XPGain,
        MaxHP,
        Heal
    }

[CreateAssetMenu(fileName = "UpgradeSO", menuName = "Upgrades/NewUpgrade")]
public class UpgradeSO : ScriptableObject
{

    

    public Sprite icon;
    public string title;
    [TextArea] public string description;
    public UpgradeType type;
    public float value;
    public Weapon TargetWeapon;
}
