using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YProjectBase;

public class DiamantController : MonoBehaviour,IGameEnd, IInit
{
    private GameData gameData;
    private int sOpNum = 0;
    private int tempOpNum = 0;
    public IGameEvent gameEndEvent;
    [SerializeField] LayerMask layer = 1<<10;

    public GameData SetGameData
    {
        set
        {
            gameData = value;
        }
    }

    private void Awake()
    {
        tempOpNum = sOpNum;
        EventCenter.GetInstance().AddEventListener(EventData.playerReset, GameReset);
    }

    public void Init()
    {
        if (gameData != null)
        {
            sOpNum = (gameData.opNum - 1) >= 0 ? (gameData.opNum - 1) : 0;
            if(gameData.tempOpNum != sOpNum)
                gameData.tempOpNum = sOpNum;
        }

        tempOpNum = sOpNum;
    }


    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener(EventData.playerReset, GameReset);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameData != null)
            sOpNum = gameData.tempOpNum;

        if (sOpNum < 0) return;

        if (Physics.CheckBox(transform.position,Vector3.one / 2,transform.rotation, layer))
        {
            if (sOpNum > 0)
            {
                sOpNum--;
                if (gameData != null && gameData.tempOpNum != sOpNum)
                    gameData.tempOpNum = sOpNum;
            }
            else if (sOpNum == 0)
            {
                sOpNum --;
                if (gameData != null && gameData.tempOpNum != sOpNum)
                    gameData.tempOpNum = sOpNum;
                GameEnd();
            }


            if (gameEndEvent != null)
                gameEndEvent?.Invoke(null);

            if (GameDataController.GetInstance() != null && GameDataController.GetInstance().musicData != null)
                MusicMgr.GetInstance().PlaySound(GameDataController.GetInstance().musicData.GetSEClip(6));

            this.gameObject.SetActive(false);
        }
    }

    public void GameEnd()
    {    
        Vector3 campoint = InputController.GetInstance().GetMainCam.WorldToScreenPoint(transform.parent.position);
        Vector2 center = new Vector2(campoint.x / Screen.width, campoint.y / Screen.height);
        EventCenter.GetInstance().EventTrigger<Vector2>("GameEndVector2", center);
        EventCenter.GetInstance().EventTrigger("GameEnd");
        EventCenter.GetInstance().RemoveEventListener(EventData.playerReset, GameReset);

        if (gameData != null)
        {
            gameData.gameClearanceTime = (System.DateTime.Now - gameData.startTime).ToString();
            gameData.gameClearanceDate = System.DateTime.Now.ToString();
        }

        if (GameDataController.GetInstance() != null && GameDataController.GetInstance().sceneData != null)
        {
            string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            if (GameDataController.GetInstance().sceneData.GetIsLock(sceneName))
            {
                //Debug.Log("GetIsLock");
                GameDataController.GetInstance().sceneData.ChangeSceneLock(sceneName, false);
            }

            GameDataController.GetInstance().GameSave();
        }



#if UNITY_IOS || UNITY_ANDROID
        InputController.GetInstance().IphoneShake();
#endif
    }

    public void GameReset()
    {
        this.gameObject.SetActive(true);
        sOpNum = tempOpNum;
        if (gameData != null && gameData.tempOpNum != sOpNum)
            gameData.tempOpNum = sOpNum;
    }


}

