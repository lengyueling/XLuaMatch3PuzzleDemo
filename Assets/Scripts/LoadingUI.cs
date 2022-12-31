using UnityEngine;
using UnityEngine.UI;
public class LoadingUI : MonoBehaviour
{
    public Scrollbar progressValue;
    public Text progressDesc;

    float m_Max;

    public void InitProgress(float max, string desc)
    {
        m_Max = max;
        progressDesc.gameObject.SetActive(true);
        progressDesc.text = desc;
        progressValue.size = max > 0 ? 0 : 100;
    }

    public void UpdateProgress(float progress)
    {
        progressValue.size = progress / m_Max;
    }
}