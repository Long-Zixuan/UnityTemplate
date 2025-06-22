using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[System.Serializable]
public struct Logo
{
    public GameObject obj;
    public float delay;
}
public class GeneralFadeScript : MonoBehaviour
{
    [Header("跳转场景")] public bool shouldLoadNextScene = true; // 控制是否在淡出后跳转场景的开关
    public string nextSceneName; // 下一个场景的名称

    /*private bool fadeInFinished = false; // 是否完成淡入
    private float timer; // 计时器
    private float delayTimer = 0f; // 延迟计时器
    private CanvasRenderer[] renderers;*/ // 存储所有相关的渲染器
    private float _timer;
    //public float swithLogoTime = 1;

    public Logo[] logos;

    void Start()
    {
        if (logos.Length == 0)
        {
            return;
        }

        try
        {
            foreach (var logo in logos)
            {
                logo.obj.SetActive(false);
            }

            logos[0].obj.SetActive(true);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error: Logo设置有问题，请检查是否给每个Logo设置了GameObject属性");
            if (shouldLoadNextScene)
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

    void Update()
    {
        FadeInLogic();
    }

    private int _logoIndex = 0;
    
    void FadeInLogic()
    {
        if (_logoIndex >= logos.Length)
        {
            return;
        }
        _timer += Time.deltaTime;
        if (_timer > logos[_logoIndex].delay)
        {
            _timer = 0;
            logos[_logoIndex].obj.SetActive(false);
            _logoIndex++;
            if (_logoIndex >= logos.Length && shouldLoadNextScene)
            {
                SceneManager.LoadScene(nextSceneName);
            }

            if (_logoIndex >= logos.Length)
            {
                return;
            }

            logos[_logoIndex].obj.SetActive(true);
        }
    }
}