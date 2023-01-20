using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraManager : MonoBehaviour
{
    public static MapCameraManager instance;
    
    [Header("Script Variable")]
    [SerializeField] private MoveManager moveManager;
    [SerializeField] private BackButtonManager backButtonManager;
    [SerializeField] private JoystickManager joystickManager;
    
    [Header("Object Variable")]
    [SerializeField] private GameObject mainCameraObject;
    private Camera mainCamera;
    [SerializeField] private GameObject[] zoomTouchObject;

    [Header("Constant Variable")]
    private readonly Vector3 initialCamera = new Vector3(0, 0, -10);
    private readonly Vector3 cameraUnit = new Vector3(3.0f, 0, 0);
    public const float defaultZoomSize = 5.0f;
    private const float mov_time = 0.8f;


    [Header("Variable")]
    public bool cameraOnPlayer;
    private float cameraSize;
    private Vector3 cameraOffset = new Vector3(0, 0, -10);
    private Vector3 destOffset;
    private Vector3 cameraPos;
    private Player_Dir cameraDir = Player_Dir.None;
    private Coroutine moveCoroutine;

    void Awake(){
        instance = this;
        this.initialSetUp();
    }

    private void initialSetUp(){
        mainCamera = mainCameraObject.GetComponent<Camera>();
        cameraOnPlayer = true;
    }

    private void Update(){
        if(cameraOnPlayer){
            if(cameraDir != moveManager.GetDir()){
                cameraDir = moveManager.GetDir();
                if(moveCoroutine != null){
                    StopCoroutine(moveCoroutine);
                }
                moveCoroutine = StartCoroutine(MoveCamera(cameraDir));
            }
            mainCameraObject.transform.position = moveManager.GetPlayerPos() + cameraOffset;
        }
    }

    IEnumerator MoveCamera(Player_Dir cameraDir_){
        if(cameraDir_ == Player_Dir.Left){
            destOffset = initialCamera + -1.0f * cameraUnit;
        }
        else if(cameraDir_ == Player_Dir.Right){
            destOffset = initialCamera + 1.0f * cameraUnit;
        }
        else{
            destOffset = initialCamera;
        }
        
        float step_timer = 0.0f;
        Vector3 startOffset = cameraOffset;
        while(destOffset != cameraOffset){
            float rate = step_timer / mov_time;
            rate = Mathf.Min(rate, 1.0f);
            rate = Mathf.Sin(rate * Mathf.PI / 2.0f);
            cameraOffset = Vector3.Lerp(startOffset, destOffset, rate);

            //mainCameraObject.transform.position = moveManager.GetPlayerPos() + cameraOffset;
            
            step_timer += Time.unscaledDeltaTime;
            yield return null;
        }
    }

    public void ZoomIn(float posX, float posY, float zoomSize){
        cameraOnPlayer = false;
        cameraPos = mainCameraObject.transform.position;
        cameraSize = mainCamera.orthographicSize;
        ZoomManager.instance.Zoom(true, posX, posY, zoomSize, ZoomInCurtain);
    }

    public void ZoomOut(){
        ZoomManager.instance.Zoom(false, cameraPos.x, cameraPos.y, cameraSize, ZoomOutCurtain);
    }

    private void ZoomInCurtain(){
        zoomTouchObject[DataManager.gameData.characterData.characterPos.curFloor - 1].SetActive(false);
        moveManager.SetTransparent();
        backButtonManager.MakeBackButton();
    }

    private void ZoomOutCurtain(){
        zoomTouchObject[DataManager.gameData.characterData.characterPos.curFloor - 1].SetActive(true);
        moveManager.SetVisible();
        backButtonManager.DeleteBackButton();
        cameraOnPlayer = true;
    }
}