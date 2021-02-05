using MyTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShadowAixController : MonoBehaviour, IGameEnd
{

    public bool isEditor = true;

    public Transform AixY;
    public Transform AixX;
    public Transform AixZ;
    public ShadowAix[] shadowAix;

    [SerializeField] Vector3 resRotate = new Vector3();



    Quaternion aixYRotateOld;
    Quaternion aixXRotateOld;
    Quaternion aixZRotateOld;


    Vector3 posOld;
    bool isReset;

    public void SetResRoate(Vector3 _vector)
    {
        resRotate = _vector;
    }


    private void OnEnable()
    {
        ToUpdateParentAix(resRotate, ref AixY, ref AixX, ref AixZ);
        ToUpdateAix(AixY, AixX, AixZ);
        ToUpdateTransPos();
    }

    private void LateUpdate()
    {

        ResetAix(resRotate, ref AixY, ref AixX, ref AixZ);

        if (isEditor)
        {
            ToUpdateParentAix(resRotate, ref AixY, ref AixX, ref AixZ);
        }

        ToUpdateAix(AixY, AixX, AixZ);
        ToUpdateTransPos();

    }


    public void ToUpdateAix(Transform _aixY, Transform _aixX, Transform _aixZ)
    {

        if (_aixY.rotation != aixYRotateOld || _aixX.rotation != aixXRotateOld || _aixZ.rotation != aixZRotateOld)
        {
            if (shadowAix != null && shadowAix.Length > 0)
            {
              
                for (int i = 0; i < shadowAix.Length; i++)
                {
                    if (shadowAix[i] != null)
                    {
                        shadowAix[i].ToUpdateAix(_aixY, _aixX, _aixZ);
                    }
                }

                aixYRotateOld = _aixY.rotation;
                aixXRotateOld = _aixX.rotation;
                aixZRotateOld = _aixZ.rotation;
            }
        }

    }

    public void ToUpdateTransPos()
    {
        if(posOld != transform.position)
        {
            if (shadowAix != null && shadowAix.Length > 0)
            {
                for (int i = 0; i < shadowAix.Length; i++)
                {
                    if (shadowAix[i] != null)
                    {
                        shadowAix[i].ToUpdateTrans(transform);
                    }
                }
                posOld = transform.position;
            }
        }

    }


    public void ToUpdateParentAix(Vector3 resRotate ,ref Transform _aixY, ref Transform _aixX, ref Transform _aixZ)
    {
        if (_aixY == null || _aixX == null || _aixZ == null)
        {
            MyDebug.ToDebugLog("AixY  == null || AixX  == null || AixZ  == null",Color.blue);
            return;                                                    
        }

        _aixY.localEulerAngles = Vector3.up * resRotate.y;
        _aixX.localEulerAngles = Vector3.right * resRotate.x;
        _aixZ.localEulerAngles = Vector3.forward * resRotate.z;

        ToUpdateAix(AixY, AixX, AixZ);
        ToUpdateTransPos();
    }


    public void ResetAix(Vector3 resRotate, ref Transform _aixY, ref Transform _aixX, ref Transform _aixZ)
    {
        if (_aixY == null || _aixX == null || _aixZ == null)
        {
            MyDebug.ToDebugLog("AixY  == null || AixX  == null || AixZ  == null", Color.blue);
            return;
        }

        if (isReset)
        {
            if(Quaternion.Angle(Quaternion.Euler(0, resRotate.y, 0), _aixY.localRotation) > 0.01f ||
                Quaternion.Angle(Quaternion.Euler(resRotate.x , 0 , 0), _aixX.localRotation) > 0.01f ||
                Quaternion.Angle(Quaternion.Euler(0, 0 , resRotate.z), _aixZ.localRotation) > 0.01f)
            {

                //MyDebug.ToDebugLog("ResetAix", Color.red);

                if (Quaternion.Angle(Quaternion.Euler(0, resRotate.y, 0), _aixY.localRotation) > 0.01f)
                    _aixY.localRotation = Quaternion.Lerp(_aixY.localRotation, Quaternion.Euler(0, resRotate.y, 0), 2f * Time.deltaTime);
                else
                    _aixY.localRotation = Quaternion.Euler(0, resRotate.y, 0);


                if (Quaternion.Angle(Quaternion.Euler(resRotate.x, 0, 0), _aixX.localRotation) > 0.01f)
                    _aixX.localRotation = Quaternion.Lerp(_aixX.localRotation, Quaternion.Euler(resRotate.x, 0, 0), 2f * Time.deltaTime);
                else
                    _aixX.localRotation = Quaternion.Euler(resRotate.x, 0, 0);


                if (Quaternion.Angle(Quaternion.Euler(0, 0, resRotate.z), _aixZ.localRotation) > 0.01f)
                    _aixZ.localRotation = Quaternion.Lerp(_aixZ.localRotation, Quaternion.Euler(0, 0, resRotate.z), 2f * Time.deltaTime);
                else
                    _aixZ.localRotation = Quaternion.Euler(0, 0, resRotate.z);

            }
            else
            {
                isReset = false;
            }
        }
    }


    public void GameEnd()
    {
        isReset = true;
    }


}
