using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicController : MyBehaviour
{
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private TsunamiMovement tsunamiMovement;
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private BoostSpeedController boostSpeedController;

    // Start is called before the first frame update
    void Start()
    {
        PlayMovieOnStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMovieOnStart()
    {
        joystick.gameObject.SetActive(false);
        boostSpeedController.canBoost = true;
        CMController.instance.ChangeCamera(1);
        playerAnimation.TriggerAnim("Character_LookAround", 1, true);
        WaitFor(3, () =>
        {
            CMController.instance.ChangeCamera(0);
            CooldownStartTimeController.instance.CooldownStartTime();
            WaitFor(3, () =>
            {
                boostSpeedController.canBoost = false;
                joystick.gameObject.SetActive(true);
                playerMovement.EnableJoystickInput(true);
                tsunamiMovement.isRunning = true;
            });
        });
    }
}
