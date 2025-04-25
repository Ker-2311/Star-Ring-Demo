using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace UI
{
    public class CircleSlider : MonoBehaviour
    {
        [LabelText("Fill")]
        public Image source;
        [OnValueChanged("ProcessChange"),Range(0,360),LabelText("滑条最大角度")]
        public float fillMax = 360;
        [OnValueChanged("ProcessChange"), Range(0, 1), MaxValue(1), MinValue(0)]
        public float curProcss = 1;

        private void ProcessChange()
        {
            if (source)
            {
                source.fillAmount = curProcss * fillMax / 360;
            }
        }

    }

}
