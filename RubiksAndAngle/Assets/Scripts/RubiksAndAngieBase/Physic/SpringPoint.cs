using System.Collections;
using UnityEngine;
using YProjectBase;

namespace RubiksAndAngie
{
    public class SpringPoint : SpringPointBase,IGameReset
    {
        [Tooltip("触发次数 (-1 : 无次数限制)")] [SerializeField]
        private int triggerNum = 1;

        [SerializeField] bool isToMoveCenter = false;

        [Space(10)] public IGameEvent SpringEvent;

        bool isMoveToCenter;
        bool isTrigger;
        private Vector2 GetSpringCenter()
        {
            return new Vector2(transform.position.x + vCenter.x, transform.position.y + vCenter.y);
        }
        private Transform colliderTrans;
        private Rigidbody colliderRigidbody;
        private GenerateGravity colliderGravity;
        private Vector2 centerPos;
        private int oldTriggerNum;
        Transform veiwModel;

        float transpose = -4;
        static float note = 8;

        protected override void OnValidate()
        {
            base.OnValidate();
            triggerNum = triggerNum >= -1 ? triggerNum : 0;
        }

        public override void Init()
        {
            base.Init();
            isMoveToCenter = false;
            isTrigger = false;
            centerPos = GetSpringCenter();
            if (veiwModel == null)
                veiwModel = transform.GetChild(0);
            if (veiwModel != null)
                veiwModel.gameObject.SetActive(true);
            note = 8;
            oldTriggerNum = triggerNum;
        }


        public override void FixedUpatedSpring()
        {
            if (isTrigger) return;
            if (isMoveToCenter) return;
            base.FixedUpatedSpring();
        }
        public override void ToShootOff()
        {
            if (isToMoveCenter)
            {
                Collider[] colliders = OverlapColliders();
                if (colliders == null || colliders.Length <= 0) return;

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i] == null || MyMathf.IsInLayerMask(colliders[i].attachedRigidbody.gameObject, colliderLayer) == false) continue;

                    if ((triggerNum > 0 || triggerNum == -1) && !isMoveToCenter)
                    {
                        isMoveToCenter = true;

                        if (veiwModel != null)
                            veiwModel.gameObject.SetActive(false);

                        colliderRigidbody = colliders[i].attachedRigidbody;
                        colliderTrans = colliderRigidbody.transform;
                        colliderGravity = colliderTrans.GetComponent<GenerateGravity>();
                        if (colliderGravity)
                        {
                            colliderGravity.SetIsGravity = false;
                        }
                        colliderRigidbody.velocity = Vector3.zero;
                        isTrigger = true;
                        break;
                    }

                    break;
                }

            }
            else
            {
                Collider[] colliders = OverlapColliders();
                if (colliders == null || colliders.Length <= 0) return;

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i] == null || MyMathf.IsInLayerMask(colliders[i].attachedRigidbody.gameObject, colliderLayer) == false) continue;

                    if ((triggerNum > 0 || triggerNum == -1))
                    {
                        if (veiwModel != null)
                            veiwModel.gameObject.SetActive(false);

                        ToShootOff(colliders[i].attachedRigidbody);

                        if(SpringEvent != null)
                        {
                            SpringEvent?.Invoke(null);
                        }

                        if (triggerNum > 0)
                        {
                            triggerNum--;
                        }
                    }
                    break;
                }
            }


        }


        public void UpadedSpring()
        {
            MovementToCenter(colliderTrans);
        }
        public void MovementToCenter(Transform _trans)
        {
            if (isMoveToCenter)
            {
                Vector2 _transPos = new Vector2(_trans.position.x, _trans.position.y);
                if (Vector2.Distance(_transPos, centerPos) > 0.01f)
                {       
                    Vector2 movePos = Vector2.Lerp(_transPos, centerPos, 0.1f);
                    _trans.position = new Vector3(movePos.x, movePos.y, _trans.position.z);
                }
                else
                {
                    ToShootOff(colliderRigidbody);

                    if (SpringEvent != null)
                    {
                        SpringEvent?.Invoke(null);
                    }

                    if (colliderGravity)
                    {
                        colliderGravity.SetIsGravity = true;
                    }
                    if (triggerNum > 0)
                    {
                        triggerNum--;
                    }
                    isMoveToCenter = false;
                    StartCoroutine(ResetTigger());
                }
            }
        }


        public override void AddListener()
        {
            base.AddListener();
            MonoMgr.GetInstance().AddUpdateListener(UpadedSpring);
            EventCenter.GetInstance().AddEventListener("ResetPlayer", GameReset);
        }
        public override void RemoveListener()
        {
            base.RemoveListener();
            MonoMgr.GetInstance().RemoveUpdateListener(UpadedSpring);
            EventCenter.GetInstance().RemoveEventListener("ResetPlayer", GameReset);
        }


        IEnumerator ResetTigger()
        {
            yield return null;

            float waitTime = 1f;
            float tempTime = 0;
            while (tempTime < waitTime)
            {
                tempTime += Time.deltaTime;
                yield return null;
            }

            isTrigger = false;
        }

        public void GameReset()
        {
            if(veiwModel == null)
                veiwModel = transform.GetChild(0);

            if (veiwModel != null)
                veiwModel.gameObject.SetActive(true);

            triggerNum = oldTriggerNum;
        }

    }
}

