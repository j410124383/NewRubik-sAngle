using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RubiksAndAngie.RPhysic
{
    public class WindPhysic : WindPhysicBase
    {
        protected override void SelectWindMode()
        {
            switch (mode)
            {
                case WindModeTypes.Directional:
                default:

                    break;
                case WindModeTypes.Line:
                    break;
                case WindModeTypes.Box:
                    break;
                case WindModeTypes.Sphere:
                    break;
            }
        }


        public override void AddListener()
        {
          
        }

        public override void RemoveListener()
        {
           
        }

    }

}
