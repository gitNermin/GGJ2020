using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    [SerializeField]
    GameObject loadingCanvas;
    [SerializeField]
    GameObject loading;
    Image loadingImage;
    [SerializeField]
    Vector3 offset;
    private float startTime;
    private float loadingTime;
    public void ShowLoadingBar(float time,Vector3 position)
    {
        startTime = time;
        loadingCanvas.transform.position = position + offset;
        loadingCanvas.SetActive(true);
    }
    private void OnEnable()
    {
        startTime = Time.time;
        loadingImage = loading.GetComponent<Image>();
        loadingImage.fillAmount = 0;
    }
    IEnumerator LoadingEffect()
    {
        yield return null;
        while (startTime + loadingTime < Time.time)
        {
            if (loadingImage.fillAmount == 1)
                loadingCanvas.SetActive(false);
        }

    }

}
