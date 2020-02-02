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

        loadingTime = time;
        //startTime = time;
        loadingCanvas.transform.position = position + offset;
        loadingCanvas.SetActive(true);
        loadingImage = loading.GetComponent<Image>();
        StartCoroutine("LoadingEffect");
    }
    private void OnEnable()
    {
        
        startTime = Time.time;
        //loadingImage = loading.GetComponent<Image>();
        //StartCoroutine("LoadingEffect");
        //loadingImage.fillAmount = 0;
    }
    IEnumerator LoadingEffect()
    {
        int timeStep = (int)(loadingTime / 0.05f);
        Debug.Log(timeStep);
        float steps = loadingTime / timeStep;
        Debug.Log(loadingTime / timeStep);

        for (int i = 0; i < timeStep; i++)
        {
            loadingImage.fillAmount -= 1f/ timeStep*2;
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
        //while (startTime + loadingTime < Time.time)
        //{
            
            
        //    if (loadingImage.fillAmount == 1)
        //        loadingCanvas.SetActive(false);
        //}

    }

}
