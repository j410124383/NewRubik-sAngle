using System.Collections.Generic;
using UnityEngine;
using YProjectBase;

namespace RubiksAndAngie
{

    public abstract class BodyControllerBase : MonoBehaviour, IBody, IInit, IGameReset, IGameStart, IGameEnd, IListener
    {
        [Header("基础设置")]
        [Tooltip("旋转父类 （可不填）")] [SerializeField] protected Transform rotateTrans;
        [Tooltip("影子父类 （可不填）")] public Transform shadowParentTrans;
        [Tooltip("模型存放")] public GameObject[] modelObj;

        [Header("控制对象清单")]
        [Tooltip("控制对象清单")] [Space(10)][SerializeField] protected List<ModelAixBase> aixObj = new List<ModelAixBase>();
        List<GameObject> _aixObj = new List<GameObject>();

        [Header("影子对象设置")]
        [Tooltip("影子位置Z轴z偏差")] [Space(10)] [SerializeField] protected float shadowOffsetZ;
        [HideInInspector] [SerializeField] protected bool disShadowList = false;
        [Tooltip("影子对象清单")] [Space(10)] [ConditionalHide("disShadowList", false)] [SerializeField] protected List<ShadowAixBase> shadowAixObj = new List<ShadowAixBase>();
        List<ModelAixBase> _shadowAixObj = new List<ModelAixBase>();

        public Transform RotateTrans { get { return rotateTrans; } }
        public List<ModelAixBase> AixObj { get { return aixObj; } }

        public List<ShadowAixBase> ShadowAixObj { get { return shadowAixObj; } }


        #region Editor 方法
#if UNITY_EDITOR

        private void OnValidate()
        {
            if (!Application.isPlaying)
            {
                  UpdatedBodyAix();
            }
        }
       
        [ContextMenu("InitBodyAndShadow")]
        public void InitBodyAndShadow()
        {
            InitAixObjs();
            InitAixShadowObj();
        }

        [ContextMenu("InitAixObjs")]
        public void InitAixObjs()
        {
            if(rotateTrans == null)
            {
                rotateTrans = new  GameObject(("RotateTrans")).transform;
                rotateTrans.SetParent(transform);
                rotateTrans.localPosition = Vector3.zero;
                rotateTrans.localRotation = Quaternion.Euler(0, 0, 0);
                rotateTrans.localScale = Vector3.one;
            }
            else
            {
                rotateTrans.localPosition = Vector3.zero;
                rotateTrans.localRotation = Quaternion.Euler(0, 0, 0);
                rotateTrans.localScale = Vector3.one;
            }

            if (modelObj == null || modelObj.Length <= 0) return;


            _aixObj.Clear();

            if (aixObj != null && aixObj.Count > 0)
            {
         
                for (int j = 0; j < modelObj.Length; j++)
                {
                    if (modelObj[j].GetComponent<MeshFilter>() == false) continue;
                    bool isNew = false;

                    for (int i = 0; i < aixObj.Count; i++)
                    {
                        if (aixObj[i] == null) continue;

                        if (aixObj[i].ModelObj == modelObj[j])
                        {
                            isNew = false;
                            break;
                        }
                        else
                        {
                            isNew = true;
                        }
                    }

                    if (isNew)
                    {
                        if (_aixObj.Contains(modelObj[j]) == false)
                        {
                            _aixObj.Add(modelObj[j]);
                            continue;
                        }
                    }

                }
                
            }
            else
            {
                for (int j = 0; j < modelObj.Length; j++)
                {
                    if (modelObj[j].GetComponent<MeshFilter>() == false) continue;

                    if (_aixObj.Contains(modelObj[j]) == false)
                        _aixObj.Add(modelObj[j]);
                }
            }


            Debug.Log("_aixObj.Count     :   " + _aixObj.Count);

            for (int i = 0; i < _aixObj.Count; i++)
            {
                if (_aixObj[i] == null) continue;

                if (_aixObj[i].GetComponent<MeshCollider>() == null)
                    _aixObj[i].AddComponent<MeshCollider>();

                GameObject _aixY = new GameObject(("AixY__" + _aixObj[i].name));
                GameObject _aixX = new GameObject(("AixX__" + _aixObj[i].name));
                GameObject _aixZ = new GameObject(("AixZ__" + _aixObj[i].name));

                _aixZ.transform.localPosition = Vector3.zero;
                _aixZ.transform.localRotation = Quaternion.Euler(Vector3.zero);
                _aixX.transform.localPosition = Vector3.zero;
                _aixX.transform.localRotation = Quaternion.Euler(Vector3.zero);

                _aixX.transform.SetParent(_aixY.transform);
                _aixZ.transform.SetParent(_aixX.transform);

                _aixY.transform.localRotation = Quaternion.Euler(0, _aixObj[i].transform.rotation.eulerAngles.y, 0);
                _aixX.transform.localRotation = Quaternion.Euler(_aixObj[i].transform.rotation.eulerAngles.x, 0, 0);
                _aixZ.transform.localRotation = Quaternion.Euler(0, 0, _aixObj[i].transform.rotation.eulerAngles.z);

                _aixY.transform.position = _aixObj[i].transform.position;

                _aixObj[i].transform.SetParent(_aixZ.transform);

                _aixObj[i].transform.localPosition = Vector3.zero;
                _aixObj[i].transform.localRotation = Quaternion.Euler(0,0,0);
                //_aixObj[i].transform.localScale = Vector3.one;
                _aixObj[i].layer = 8;

                _aixY.transform.SetParent(transform);
                Vector3 rotateVec = _aixY.transform.localRotation.eulerAngles + _aixX.transform.localRotation.eulerAngles + _aixZ.transform.localRotation.eulerAngles;

                ModelAixBase _modelAix = new ModelAixBase(_aixObj[i], _aixY.transform, _aixX.transform, _aixZ.transform, rotateVec);

                aixObj.Add(_modelAix);
            }

        }

        [ContextMenu("ClearAllAixObj")]
        public void ClearAllAixObj()
        {
            if (aixObj != null && aixObj.Count > 0)
            {
                for (int i = 0; i < aixObj.Count; i++)
                {
                    if (aixObj[i] == null) continue;
                    aixObj[i].ModelObj.transform.SetParent(transform);
                    DestroyImmediate(aixObj[i].AixObj);
                }

                aixObj.Clear();
            }

            ClearAllAixShadowObj();
        }

        [ContextMenu("InitAixShadowObj")]
        public void InitAixShadowObj()
        {
            if (shadowParentTrans == null)
            {
                shadowParentTrans = new GameObject(("ShadowParentTrans")).transform;
                shadowParentTrans.SetParent(transform.parent != null ? transform.parent : null);
                shadowParentTrans.localPosition = Vector3.zero;
                shadowParentTrans.localRotation = Quaternion.Euler(0, 0, 0);
                shadowParentTrans.localScale = new Vector3(1, 1, 0.01f);
            }
            else
            {
                rotateTrans.localPosition = Vector3.zero;
                rotateTrans.localRotation = Quaternion.Euler(0, 0, 0);
                rotateTrans.localScale = new Vector3(1, 1, 0.01f);
            }

            if (aixObj == null || aixObj.Count <= 0) return;

            _shadowAixObj.Clear();

            if (shadowAixObj != null && shadowAixObj.Count > 0)
            {
                for (int j = 0; j < aixObj.Count; j++)
                {

                    if (aixObj[j] == null || aixObj[j].ModelObj == null || aixObj[j].ModelObj.GetComponent<MeshFilter>() == false) continue;
                    bool isNew = false;

                    for (int i = 0; i < shadowAixObj.Count; i++)
                    {
                        if (shadowAixObj[i] == null) continue;

                        if (aixObj[j].ModelObj.GetComponent<MeshFilter>() == false) continue;

                        if (aixObj[i].ModelObj == shadowAixObj[j].ControllerModelObj)
                        {
                            isNew = false;
                            break;
                        }
                        else
                        {
                            isNew = true;
                        }
                      
                    }

                    if (isNew)
                    {
                        if (_shadowAixObj.Contains(aixObj[j]) == false)
                        {
                            _shadowAixObj.Add(aixObj[j]);
                            continue;
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < aixObj.Count; j++)
                {
                    if (aixObj[j].ModelObj.GetComponent<MeshFilter>() == false) continue;

                    if (_shadowAixObj.Contains(aixObj[j]) == false)
                        _shadowAixObj.Add(aixObj[j]);
                }
            }

            if(_shadowAixObj.Count <= 0)
            {
                SortShadowList();
                return;
            }

            Debug.Log("_shadowAixObj.Count     :   " + _shadowAixObj.Count);

            for (int i = 0; i < _shadowAixObj.Count; i++)
            {
                if (_shadowAixObj[i] == null) continue;

                GameObject _sAixY = new GameObject(("SAixY__" + _shadowAixObj[i].ModelObj.name));
                GameObject _sAixX = new GameObject(("SAixX__" + _shadowAixObj[i].ModelObj.name));
                GameObject _sAixZ = new GameObject(("SAixZ__" + _shadowAixObj[i].ModelObj.name));

                GameObject _shadowObj = new GameObject("S_" + _shadowAixObj[i].ModelObj.name);

                Component[] copiedComponents;

                copiedComponents = _shadowAixObj[i].ModelObj.GetComponents<Component>();

                for (int j = 0; j < copiedComponents.Length; j++)
                {
                    if (!copiedComponents[j]) continue;
                    UnityEditorInternal.ComponentUtility.CopyComponent(copiedComponents[j]);
                    UnityEditorInternal.ComponentUtility.PasteComponentAsNew(_shadowObj);
                }

                _sAixX.transform.SetParent(_sAixY.transform);
                _sAixZ.transform.SetParent(_sAixX.transform);
                _sAixY.transform.SetParent(shadowParentTrans);
                _shadowObj.transform.SetParent(_sAixZ.transform);
   

                _sAixY.transform.localScale = Vector3.one;
                _sAixX.transform.localScale = Vector3.one;
                _sAixZ.transform.localScale = Vector3.one;

                _sAixY.transform.localPosition = Vector3.zero;
                _sAixZ.transform.localPosition = Vector3.zero;
                _sAixZ.transform.localRotation = Quaternion.Euler(Vector3.zero);
                _sAixX.transform.localPosition = Vector3.zero;
                _sAixX.transform.localRotation = Quaternion.Euler(Vector3.zero);

                _shadowObj.transform.localPosition = Vector3.zero;
                _shadowObj.transform.localRotation = Quaternion.Euler(0, 0, 0);
                _shadowObj.transform.localScale = _shadowAixObj[i].ModelObj.transform.localScale;

                _shadowObj.layer = 8;

                Vector3 rotateVec = _sAixY.transform.localRotation.eulerAngles + _sAixX.transform.localRotation.eulerAngles + _sAixZ.transform.localRotation.eulerAngles;

                ShadowAixBase _sModelAix = new ShadowAixBase(_shadowAixObj[i].ModelObj, _shadowObj, _sAixY.transform, _sAixX.transform, _sAixZ.transform, shadowOffsetZ);

                _sModelAix.UpdatedPostionAix(_shadowAixObj[i].AixY);
                _sModelAix.UpdatedRotateAix(_shadowAixObj[i].AixY, _shadowAixObj[i].AixX, _shadowAixObj[i].AixZ);

                shadowAixObj.Add(_sModelAix);
            }

            SortShadowList();
        }

        [ContextMenu("ClearAllAixShadowObj")]
        public void ClearAllAixShadowObj()
        {
            if (shadowAixObj != null && shadowAixObj.Count > 0)
            {
                for (int i = 0; i < shadowAixObj.Count; i++)
                {
                    if (shadowAixObj[i] == null) continue;          
                    DestroyImmediate(shadowAixObj[i].AixObj);
                }

                shadowAixObj.Clear();
            }

            if (shadowParentTrans != null)
            {
                Transform[] childs = shadowParentTrans.GetComponentsInChildren<Transform>(true);

                int childLength = childs.Length;

                if (childLength <= 0) return;

                for (int i = 0; i < childLength; i++)
                {
                    if (childs[0] == null || shadowParentTrans == childs[0]) continue;
                    DestroyImmediate(childs[0].gameObject);
                }
            }

        }

        private void SortShadowList()
        {
            if (shadowAixObj != null && shadowAixObj.Count > 0)
            {
                List<ShadowAixBase> tempList = shadowAixObj;

                for (int i = 0; i < shadowAixObj.Count; i++)
                {
                    if (shadowAixObj[i] == null) continue;

                    for (int j = 0; j < aixObj.Count; j++)
                    {
                        if (aixObj[j] == null) continue;

                        if (shadowAixObj[i].ControllerModelObj != aixObj[j].ModelObj) continue;

                        tempList[j] = shadowAixObj[i];

                        break;
                    }
                }

                shadowAixObj = tempList;
            }
        }

        private void UpdatedBodyAix()
        {
            if (aixObj != null && aixObj.Count > 0)
            {
                for (int i = 0; i < aixObj.Count; i++)
                {
                    if (aixObj[i] == null) continue;
                    aixObj[i].UpdatedPostionAix();
                    aixObj[i].UpdatedRotateAix();
                }

                UpdatedSAix();
            }
        }

        private void UpdatedSAix()
        {
            SortShadowList();

            if (shadowAixObj != null && shadowAixObj.Count > 0)
            {
                for (int i = 0; i < shadowAixObj.Count; i++)
                {
                    if (shadowAixObj[i] == null) continue;
                    shadowAixObj[i].UpdatedPostionAix(aixObj[i].AixY);
                    shadowAixObj[i].UpdatedRotateAix(aixObj[i].AixY, aixObj[i].AixX, aixObj[i].AixZ);
                    shadowAixObj[i].UpdatedPostionOffsetZ(shadowOffsetZ);
                }
            }
        }

#endif
        #endregion



        protected void OnEnable()
        {
            OnEnabledBodyController();
        }

        protected void OnDisable()
        {
            OnDisableBodyController();
        }


        public abstract void Init();



        public void SortAixList()
        {
            if (aixObj == null && aixObj.Count <= 0) return;
            if (shadowAixObj != null && shadowAixObj.Count > 0)
            {
                List<ShadowAixBase> tempList = shadowAixObj;

                for (int i = 0; i < shadowAixObj.Count; i++)
                {
                    if (shadowAixObj[i] == null) continue;

                    for (int j = 0; j < aixObj.Count; j++)
                    {
                        if (aixObj[j] == null) continue;

                        if (shadowAixObj[i].ControllerModelObj != aixObj[j].ModelObj) continue;

                        tempList[j] = shadowAixObj[i];

                        break;
                    }
                }

                shadowAixObj = tempList;
            }
        }
        public GameObject GetAixObj(GameObject _go)
        {
            for (int i = 0; i < AixObj.Count; i++)
            {
                if (AixObj[i].ModelObj == _go)
                {
                    if (AixObj[i].AixObj != null)
                    {
                        return AixObj[i].AixObj;
                    }
                }
            }
            return null;
        }
        public GameObject GetModelObj(GameObject _go)
        {
            for (int i = 0; i < AixObj.Count; i++)
            {
                if (AixObj[i].ModelObj == _go)
                {
                     return AixObj[i].ModelObj;           
                }
            }
            return null;
        }

        protected abstract void OnEnabledBodyController();
        protected abstract void OnDisableBodyController();

        protected virtual void UpdatedBodyController()
        {

        }
        protected virtual void LateUpdatedBodyController()
        {

        }



        public abstract void GameStart();
        public abstract void GameEnd();
        public abstract void GameReset();

        public abstract void UpdatedAixData();
        public abstract void ResetAixData();
        public abstract void AddListener();
        public abstract void RemoveListener();

    }

}