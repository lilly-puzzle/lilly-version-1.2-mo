using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player_Steps
{
    None = -1,
    Idle = 0,
    Move,
    Accel,
    Decel,
};

public enum Player_Dir{
    None = -1,
    Left,
    Right
};

public class MoveManager : MonoBehaviour
{
    [Header("Script Variable")]
    [SerializeField] private JoystickManager joystickManager;
    [SerializeField] private MapCameraManager mapCameraManager;
    [SerializeField] private UpDownButtonManager updownButtonManager;

    [Header("Object Variable")]
    [SerializeField] private GameObject playerObject;
    private SpriteRenderer playerSprite;
    

    [Header("Constant Variable")]
    [SerializeField] private const float maxSpeed = 8.0f;
    [SerializeField] private const float speedUnit = 10.0f;
    [SerializeField] private readonly float[] moveLimit = {-1.0f, 38.0f};    // [0] is Left Limit, [1] is Right Limit  
    [SerializeField] private const float updownButtonMakeLine = 35.0f;
    [SerializeField] private readonly Vector3[] floorPos = {Vector3.zero, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(0.0f, 10.0f, 0.0f), new Vector3(0.0f, 20.0f, 0.0f), new Vector3(0.0f, 30.0f, 0.0f)}; // Start from 1

    [Header("Variable")]
    [SerializeField] private Player_Steps selectedStep = Player_Steps.None;
    [SerializeField] private Player_Dir selectedDir = Player_Dir.None;
    private Player_Dir pastDir = Player_Dir.None;
    [SerializeField] private int playerOnFloor = 1;
    private float speed = 0.0f;
    private bool updownButtonActive = false;
    private float playerPosX;


    void Awake(){
        playerSprite = playerObject.GetComponent<SpriteRenderer>();
        LoadCharacterData();
        playerObject.transform.position = floorPos[playerOnFloor] + new Vector3(playerPosX, 0.0f, 0.0f);
    }

    private void LoadCharacterData(){
        playerOnFloor = DataManager.gameData.characterData.characterPos.curFloor;
        playerPosX = DataManager.gameData.characterData.characterPos.curPosX;
    }

    void Update(){
        pastDir = selectedDir;
        ApplyDir();
        if(pastDir != selectedDir){
            speed = speed / 3.0f;
        }

        DecideStep();
        switch(selectedStep){
            case Player_Steps.Idle:
                speed = 0.0f;
                break;
            case Player_Steps.Move:
                speed = maxSpeed;
                break;
            case Player_Steps.Accel:
                speed += Time.deltaTime * speedUnit;
                break;
            case Player_Steps.Decel:
                speed -= Time.deltaTime * speedUnit;
                break;
            default:
                Debug.Log("Error");
                break;
        }

        // Player Position

        if (this.selectedDir == Player_Dir.Right){
            playerObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f * speed, 0);
        }
        else if (this.selectedDir == Player_Dir.Left){
            playerObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.0f * speed, 0);
        }
        else{
            playerObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f * speed, 0);
        }

        // For Updown Button
        if(playerObject.transform.position.x > updownButtonMakeLine){
            if (!updownButtonActive){
                if (playerOnFloor == 1){
                    //if(floor1 clear){
                    updownButtonManager.makeButton(true);
                    //}
                }
                else if (playerOnFloor == 5){

                }
                else{
                    //if(해당floor clear){
                    updownButtonManager.makeButton(true);
                    //}
                    updownButtonManager.makeButton(true);
                }
                updownButtonActive = true;
            }
        }
        else{
            if(updownButtonActive){
                updownButtonManager.deleteButton(true);
                updownButtonManager.deleteButton(false);
                updownButtonActive = false;
            }
        }
    }

    private void DecideStep(){
        if(joystickManager.GetisJoystickAct()){ // Joystick is active
            if(joystickManager.GetJoystickDir() != 0){
                if(speed >= maxSpeed){
                    selectedStep = Player_Steps.Move;
                }
                else{
                    selectedStep = Player_Steps.Accel;
                }
            }
            else{   // GetJoystickDir == 0;
                if(speed <= 0.0f){
                    selectedStep = Player_Steps.Idle;
                }
                else{
                    selectedStep = Player_Steps.Decel;
                }
            }
        }
        else{   // Joystick is deactive
            if(speed <= 0.0f){
                selectedStep = Player_Steps.Idle;
            }
            else{
                selectedStep = Player_Steps.Decel;
            }
        }

        if (this.selectedDir == Player_Dir.Right){
            if (playerObject.transform.position.x >= moveLimit[1]){
                if (speed <= 0.0f){
                    selectedStep = Player_Steps.Idle;
                }
                else{
                    selectedStep = Player_Steps.Decel;
                }
            }
        }
        if (this.selectedDir == Player_Dir.Left){
            if (playerObject.transform.position.x <= moveLimit[0]){
                if (speed <= 0.0f){
                    selectedStep = Player_Steps.Idle;
                }
                else{
                    selectedStep = Player_Steps.Decel;
                }
            }
        }
    }  

    public void SetTransparent(){
        Color color = playerSprite.color;
        color.a = 0.0f;
        playerSprite.color = color;
    }

    public void SetVisible(){
        Color color = playerSprite.color;
        color.a = 1.0f;
        playerSprite.color = color;
    }

    public void SetStep(Player_Steps step){
        selectedStep = step;
    }

    public Player_Steps GetStep(){
        return this.selectedStep;
    }

    private void ApplyDir(){
        if(joystickManager.GetJoystickDir() >= 1){
            selectedDir = Player_Dir.Right;
        }
        else if(joystickManager.GetJoystickDir() <= -1){
            selectedDir = Player_Dir.Left;
        }
    }

    public Player_Dir GetDir(){
        return selectedDir;
    }

    public Vector3 GetPlayerPos(){ 
        return playerObject.transform.position;
    }

    public void MoveFloor(bool isUp){
        playerOnFloor = isUp ? playerOnFloor++ : playerOnFloor--;

        ZoomManager.instance.Zoom(false, moveLimit[0], floorPos[playerOnFloor].y, MapCameraManager.defaultZoomSize, MoveFloorCurtainFunction);
    }

    private void MoveFloorCurtainFunction(){
        playerObject.transform.position = new Vector3(moveLimit[0], floorPos[playerOnFloor].y, floorPos[playerOnFloor].z);
    }

}
