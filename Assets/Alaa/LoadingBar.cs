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
    GameObject hammer;
    private float startTime;
    private float loadingTime;
    public void ShowLoadingBar(float time,Vector3 position)
    {
        hammer = Instantiate(Resources.Load("Hammer")) as GameObject;
        hammer.transform.parent = transform;
        hammer.transform.position = Vector3.zero+offset;
        loadingTime = time;
        //startTime = time;
        loadingCanvas.transform.position = position + offset;
        loadingCanvas.SetActive(true);
        loadingImage = loading.GetComponent<Image>();
        StartCoroutine("LoadingEffect");
    }
    private void OnEnable()
    {
        
        //loadingImage = loading.GetComponent<Image>();
        //StartCoroutine("LoadingEffect");
        //loadingImage.fillAmount = 0;
    }
    IEnumerator LoadingEffect()
    {
        startTime = Time.time;
        //int timeStep = (int)(loadingTime / 0.05f);
        //Debug.Log(timeStep);
        //float steps = loadingTime / timeStep;
        //Debug.Log(loadingTime / timeStep);

        //for (int i = 0; i < timeStep; i++)
        //{
        //    loadingImage.fillAmount -= (1f/ (timeStep*0.7f));
        //    yield return new WaitForSeconds(0.05f);
        //}

        while ( Time.time<startTime+loadingTime)
        {
            loadingImage.fillAmount -= loadingTime * Time.deltaTime;

            if (loadingImage.fillAmount == 1)
                loadingCanvas.SetActive(false);
            yield return null;
        }
        Destroy(hammer);
        Destroy(gameObject);
    }

}
