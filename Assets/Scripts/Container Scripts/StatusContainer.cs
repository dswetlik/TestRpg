using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusContainer : MonoBehaviour
{
    [SerializeField] StatusEffect statusEffect;

    public StatusEffect GetStatusEffect() { return statusEffect; }

    public void SetStatusEffect(StatusEffect statusEffect)
    {
        this.statusEffect = statusEffect;
        gameObject.transform.GetChild(0).GetComponent<Text>().text = statusEffect.GetTurnAmount().ToString();
        gameObject.GetComponent<Image>().sprite = statusEffect.GetSprite();
    }

    public void DecrementStatusEffect()
    {
        gameObject.transform.GetChild(0).GetComponent<Text>().text = statusEffect.GetTurnAmount().ToString();
    }

    public int GetTurnAmount() { return statusEffect.GetTurnAmount(); }
}
