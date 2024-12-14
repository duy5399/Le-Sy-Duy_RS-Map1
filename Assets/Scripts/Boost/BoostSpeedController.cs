using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostSpeedController : BoostController
{
    [SerializeField] private PlayerData playerData;


    protected override void Update()
    {
        base.Update();
    }

    protected override void Boost()
    {
        //Tiêu hao năng lượng thành công và tăng tốc độ chạy tạm thời
        if (StaminaController.instance.UseStamina(playerData.speed))
        {
            SpeedMeterController.instance.speed = SpeedMeterController.instance.maxSpeed;
            playerData.BoostSpeed(playerData.speed);
        }
    }

}
