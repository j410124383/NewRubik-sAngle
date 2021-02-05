using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtension
{
    /// <summary>
    /// ParticleSystem.MinMaxCurve 扩充
    /// </summary>
    public static class MyParticleMinMaxCurve
    {
        /// <summary>
        ///  (获得最大粒子时长) ParticleSystem.MinMaxCurve 扩充
        /// </summary>
        /// <param name="minMaxCurve"></param>
        /// <returns></returns>
        public static float YGetMaxValue(this ParticleSystem.MinMaxCurve minMaxCurve)
        {
            float maxCurve = 0;

            switch (minMaxCurve.mode)
            {
                case ParticleSystemCurveMode.Constant:
                    maxCurve = minMaxCurve.constant;
                    break;

                case ParticleSystemCurveMode.Curve:
                    for (int j = 0; j < minMaxCurve.curve.keys.Length; j++)
                    {
                        if (minMaxCurve.curve.keys[j].value > maxCurve)
                            maxCurve = minMaxCurve.curve.keys[j].value;              
                    }
                    break;


                case ParticleSystemCurveMode.TwoConstants:
                    if (minMaxCurve.constantMax > minMaxCurve.constantMin)
                        maxCurve = minMaxCurve.constantMax;
                    else
                        maxCurve = minMaxCurve.constantMin;
                    break;


                case ParticleSystemCurveMode.TwoCurves:

                    for (int j = 0; j < minMaxCurve.curveMax.keys.Length; j++)
                    {
                        if (minMaxCurve.curveMax.keys[j].value > maxCurve)                       
                            maxCurve = minMaxCurve.curveMax.keys[j].value;
                       
                    }
                    for (int j = 0; j < minMaxCurve.curveMin.keys.Length; j++)
                    {
                        if (minMaxCurve.curveMin.keys[j].value > maxCurve)
                            maxCurve = minMaxCurve.curveMin.keys[j].value;                   
                    }

                    break;
            }

            return maxCurve;
        }
    }

}
