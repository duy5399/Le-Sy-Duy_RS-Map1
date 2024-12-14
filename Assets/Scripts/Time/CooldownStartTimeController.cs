using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CooldownStartTimeController : Singleton<CooldownStartTimeController>
{
    [SerializeField] private TextMeshProUGUI txtTime;
    [SerializeField] private int cooldownTime;

    protected void Awake()
    {
        base.Awake();
        txtTime = this.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        ChildActive(false);
    }

    public void CooldownStartTime()
    {
        ChildActive(true);
        StartCoroutine(_CooldownStartTime());
    }

    IEnumerator _CooldownStartTime()
    {
        cooldownTime = 3;
        while (cooldownTime > 0)
        {
            yield return new WaitForSeconds(1);
            cooldownTime--;
            txtTime.text = cooldownTime.ToString();
        }
        ChildActive(false);
    }
}
