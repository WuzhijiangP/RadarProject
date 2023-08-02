using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject evenObj;

    public Animator coverAnim;

    // Start is called before the first frame update
    void Start()
    {

        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(this.evenObj);

        coverAnim = gameObject.GetComponent<Animator>();
    }

    public IEnumerator LoadScene(int index)
    {
        //播放淡入动画
        // coverAnim.SetBool("FadeIn", true);
        // coverAnim.SetBool("FadeOut", false);

        yield return new WaitForSeconds(0.5f);

        //使用异步加载场景，实现场景切换完成后播放的淡出动画
        AsyncOperation async = SceneManager.LoadSceneAsync(index);
        async.completed += OnLoadScene;
    }

    private void OnLoadScene(AsyncOperation obj)
    {
        //播放淡出动画
        // coverAnim.SetBool("FadeIn", false);
        // coverAnim.SetBool("FadeOut", true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
