using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UpgradeSlot : MonoBehaviour
{
    public Button button;
    public Image icon;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descText;


    private UpgradeSO current;


    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    public void SetUpgradeInfo(UpgradeSO upgrade)
    {
        current = upgrade;

        icon.sprite = upgrade.icon;
        titleText.text = upgrade.title;
        descText.text = upgrade.description;
    }

    void OnClick()
    {
        if (current == null)
        {
            return;
        }

        UpgradeSystem.instace.Apply(current);
        UpgradeSystem.instace.Close();
    }
}
