using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider m_SliderValue;
    public Text m_SetVolume;

    public void Volume()
    {
        m_SetVolume.text = m_SliderValue.value.ToString();
    }
}
