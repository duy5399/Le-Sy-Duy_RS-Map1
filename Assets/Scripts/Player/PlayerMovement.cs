using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MyBehaviour
{
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private Canvas canvasInput;
    [SerializeField] private bool _moveWithJoystick;
    [SerializeField] private Vector3 inputValue;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private bool autoMove;
    [SerializeField] private Coroutine coroutineAutoMoveWhenBoostMoney;

    [SerializeField] private PlayerData playerData;
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private TsunamiMovement tsunamiMovement;
    [SerializeField] private BoostSpeedController boostSpeedController;

    public bool moveWithJoystick
    {
        get { return _moveWithJoystick; }
        set { _moveWithJoystick = value; }
    }
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        inputValue = new Vector3(0, 0, 0);
        direction = new Vector3(0, 0, 0);
        playerData = this.GetComponent<PlayerData>();
        playerAnimation = this.GetComponent<PlayerAnimation>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (!moveWithJoystick)
        {
            //Di chuyển khi được boost ở MainScene
            if (autoMove)
            {
                inputValue = new Vector3(0, 0, 0);
                inputValue.z = playerData.currSpeed;
                playerAnimation.TriggerRun();
            }
        }
        else
        {
            //Nếu không có joystick thì không thể di chuyển
            if (joystick)
            {
                //Di chuyển, xoay, hoạt ảnh
                inputValue = new Vector3(0, 0, 0);
                direction = new Vector3(0, 0, 0);
                inputValue.x = joystick.Horizontal * (playerData.currSpeed + 5);
                inputValue.z = joystick.Vertical * (playerData.currSpeed + 5);
                if (joystick.Horizontal != 0 || joystick.Vertical != 0)
                {
                    direction = Vector3.RotateTowards(this.transform.forward, inputValue, rotateSpeed * Time.deltaTime, 0.0f);
                    this.transform.rotation = Quaternion.LookRotation(direction);
                    playerAnimation.TriggerRun();
                }
                else if (joystick.Horizontal == 0 && joystick.Vertical == 0)
                {
                    Debug.Log("TriggerIdle");
                    playerAnimation.TriggerIdle();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = inputValue;
    }
    public void EnableJoystickInput(bool isEnable)
    {
        moveWithJoystick = isEnable;
        //canvasInput.gameObject.SetActive(isEnable);
    }

    public void AutoMoveWhenBoostMoney()
    {
        try
        {
            StopCoroutine(coroutineAutoMoveWhenBoostMoney);
        }
        catch
        {
            Debug.Log("Không có coroutineAutoMoveWhenBoostMoney");
        }
        coroutineAutoMoveWhenBoostMoney = StartCoroutine(_AutoMoveWhenBoostMoney());
    }

    IEnumerator _AutoMoveWhenBoostMoney()
    {
        float time = 1f;
        playerAnimation.TriggerRun();
        inputValue = new Vector3(0, 0, 0);
        inputValue.z = 10;
        while (time > 0)
        {
            time -= Time.fixedDeltaTime;
            yield return null;
        }
        inputValue = new Vector3(0, 0, 0);
        playerAnimation.TriggerIdle();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter: " + other.name + " - " + GameObjectTag.TriggerNewStreet.ToString());
        if (other.gameObject.CompareTag(GameObjectTag.TriggerNewStreet.ToString())){
            MapController.instance.ChangeToNewRoad(other.transform.parent.gameObject);
        }
        if (other.gameObject.CompareTag(GameObjectTag.Animal.ToString()) && !playerData.catsRescued.Contains(other.gameObject))
        {
            playerData.catsRescued.Add(other.gameObject);
            AnimalMovement animalMovement = other.GetComponent<AnimalMovement>();
            animalMovement.following = this.gameObject;
            animalMovement.spaceFollow = 2.5f * playerData.catsRescued.Count;
            playerData.currSpeed -= LevelController.instance.weightOfCat;
        }
        if (other.gameObject.CompareTag(GameObjectTag.Tsunami.ToString()))
        {
            CMController.instance.ChangeCamera(1);
            EnableJoystickInput(false);
            rb.velocity = Vector3.zero;
            playerAnimation.TriggerIdle(true);
            WaitFor(1, () =>
            {
                playerData.money += LevelController.instance.GetReward(playerData.highestDistance, playerData.catsRescued.Count);
                PopupResultController.instance.DisplayResult(false, playerData.catsRescued.Count, LevelController.instance.numberOfCats, playerData.completedTime, playerData.highestSpeed, LevelController.instance.GetReward(playerData.highestDistance, playerData.catsRescued.Count));
                PopupResultController.instance.ChildActive(true);
            });
        }
        if (other.gameObject.CompareTag(GameObjectTag.FinishPhase1.ToString()))
        {
            EnableJoystickInput(false);
            joystick.gameObject.SetActive(false);
            autoMove = true;
            boostSpeedController.canBoost = true;
            CMController.instance.ChangeCamera(1);
        }
        if (other.gameObject.CompareTag(GameObjectTag.FinishPhase2.ToString()))
        {
            EnableJoystickInput(false);
            autoMove = false;
            inputValue = Vector3.zero;
            playerAnimation.TriggerIdle(true);
            tsunamiMovement.isRunning = false;
            WaitFor(1, () =>
            {
                playerData.money += LevelController.instance.GetReward(playerData.highestDistance, playerData.catsRescued.Count);
                LevelController.instance.raceLevel += 1;
                PopupResultController.instance.DisplayResult(true, playerData.catsRescued.Count, LevelController.instance.numberOfCats, playerData.completedTime, playerData.highestSpeed, LevelController.instance.GetReward(playerData.highestDistance, playerData.catsRescued.Count));
                PopupResultController.instance.ChildActive(true);
            });
        }
    }
}
