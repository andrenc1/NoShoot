using UnityEngine;
using TMPro;

public class HitCanva : MonoBehaviour
{
    public TextMeshProUGUI hitText;

    public void changeValue(int damageValue)
    {
        hitText.text = damageValue.ToString();
    }
}
