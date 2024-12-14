using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostMoneyController : BoostController
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private PlayerMovement playerMovement;

    protected override void Update()
    {
        base.Update();
    }

    protected override void Boost()
    {
        //Tiêu hao năng lượng thành công và nhận thêm tiền
        if (StaminaController.instance.UseStamina(playerData.speed))
        {
            SpeedMeterController.instance.speed = SpeedMeterController.instance.maxSpeed;
            CurrencyController.instance.AddMoney(playerData.income);
            CurrencyController.instance.DisplayCurrency(playerData.money);
            playerMovement.AutoMoveWhenBoostMoney();
        }
    }

}
