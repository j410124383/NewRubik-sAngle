using YProjectBase;

namespace RubiksAndAngie
{
    public class BodyController : BodyControllerBase
    {

        protected override void OnEnabledBodyController()
        {
            Init();
        }

        protected override void OnDisableBodyController()
        {
            RemoveListener();
        }

        protected override void UpdatedBodyController()
        {
            UpdatedAixData();
        }


        public override void Init()
        {
            SortAixList();
            InitAixData();
            AddListener();
        }



        public override void GameEnd()
        {
            ResetAixData();
        }
        public override void GameStart()
        {
            
        }
        public override void GameReset()
        {
            ResetAixData();
        }


        private void InitAixData()
        {
            if (aixObj != null && aixObj.Count > 0)
            {
                for (int i = 0; i < aixObj.Count; i++)
                {
                    if (aixObj[i] == null) continue;
                    aixObj[i].OriginPostionAix = aixObj[i].ModelPos;
                    aixObj[i].OriginRotateAix = aixObj[i].ModelRotate;
                }
                UpdatedAixData();
            }
        }

        public override void ResetAixData()
        {
            if (aixObj != null && aixObj.Count > 0)
            {
                for (int i = 0; i < aixObj.Count; i++)
                {
                    if (aixObj[i] == null) continue;
                    aixObj[i].ResetRotateAix();
                    aixObj[i].ResetPostionAix();
                }
                UpdatedAixData();
            }
        }
        public override void UpdatedAixData()
        {
            if (aixObj == null || aixObj.Count <= 0) return;

            if (shadowAixObj != null && shadowAixObj.Count > 0)
            {
                for (int i = 0; i < shadowAixObj.Count; i++)
                {
                    if (shadowAixObj[i] == null || aixObj[i] == null) continue;
                    shadowAixObj[i].UpdatedPostionAix(aixObj[i].AixObj.transform);
                    shadowAixObj[i].UpdatedRotateAix(aixObj[i].AixY, aixObj[i].AixX, aixObj[i].AixZ);
                    shadowAixObj[i].UpdatedPostionOffsetZ(shadowOffsetZ);
                    //Debug.Log(" shadowAixObj[i] " + shadowAixObj[i].AixY);
                }
            }
        }

        public override void AddListener()
        {
            MonoMgr.GetInstance().AddUpdateListener(UpdatedBodyController);
        }
        public override void RemoveListener()
        {
            MonoMgr.GetInstance().RemoveUpdateListener(UpdatedBodyController);
        }

    }
}