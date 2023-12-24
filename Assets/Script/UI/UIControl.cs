using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using NaughtyAttributes;
using UnityEngine.Video;

public class UIControl : MonoBehaviour
{
    public static UIControl instance;
    [SerializeField]
    GameObject gun;
    [SerializeField] 
    public Animator skillUnderAnime;
    [SerializeField]
    public Image[] FlyTank_Image;           //蝴蝶槽的[]
    [SerializeField]
    public Slider[] FlyTank_energy;
    
    public Sprite nullimage;
    [SerializeField]
    public GameObject lightSet;

    [SerializeField] 
    List<GameObject> flytank;

    private FlyData[] flyData;
    
    [SerializeField]
    public Image blood_R;
    [SerializeField]
    public Image blood_Y;

    [SerializeField]
    public Image O2Image;
    [SerializeField]
    public Animator playerHit;

    public Slider BossSlider;
    private float currentPrg, targetPrg;
    public float AccelerHpSpeed = 0.5f;

    public GameObject DebugGrid;
    public GameObject debugTextPrefab;
    private Text debugText;

    private Animator animator;
    //播放影片
    public GameObject videoGameObject;
    public VideoClip GameStart;
    public VideoClip Cutscenes;
    private VideoPlayer videoPlayer;
    private RawImage rawImage;
    public bool isPlayingVideo;
    
    public Text damageTextPrefab;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        gun = GameManager.instance.gun;
        flytank = gun.GetComponent<GunType>().FlyTank;
        debugText = debugTextPrefab.GetComponent<Text>();
        //播放影片
        videoPlayer = videoGameObject.GetComponent<VideoPlayer>();
        rawImage = videoGameObject.GetComponent<RawImage>();
        animator = GetComponent<Animator>();
        if (gun != null && !gun.activeSelf)
        {
            ReloadGunUI();
        }
        
        currentPrg = blood_R.fillAmount;
        StopPlayVideo();
    }

    private void Update()
    {
        if (!ReferenceEquals(videoPlayer.texture ,null) && isPlayingVideo)
        {
            rawImage.texture = videoPlayer.texture;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("有按到結束");
                StopPlayVideo();
            }
        }
        
    }

    public void ReloadGunUI() //重製UI
    {
        for (int i = 0; i < 3; i++)
        {
            if (flytank[i])
            {
                FlyTank_Image[i].sprite = flytank[i].GetComponent<FlyData>().ButterFlyImage;
                FlyTank_energy[i].value = flytank[i].GetComponent<FlyData>().Fly_Energy;
            }
            else
            {
                FlyTank_Image[i].sprite = nullimage;
                FlyTank_energy[i].value = 0;
            }
        }
    }

    public void ReloadPlayeHpUI(float HpValue)
    {
        blood_R.fillAmount = HpValue;
        targetPrg = HpValue;
        StartCoroutine(changeYValue());
    }

    public void ReloadBossHpUI(int HpValue)
    {
        
    }

    IEnumerator changeYValue()
    {
        while (currentPrg != targetPrg)
        {
            // 使用 Mathf.MoveTowards 逐渐改变 currentPrg 的值
            currentPrg = Mathf.MoveTowards(currentPrg, targetPrg, AccelerHpSpeed * Time.deltaTime);
            // 更新血量条的显示
            blood_Y.fillAmount = currentPrg;

            yield return new WaitForSeconds(0.1f); // 调整这个等待时间以控制血量下降速度
        }
    }
    
    public void ReloadPlayerO2UI(float O2Value)
    {
        O2Image.fillAmount = O2Value;
    }

    public void PlayerHit()
    {
        //playerHit.Play("nothing");
        playerHit.Play("hit");
        //playerHit.Play("Warning");
    }
    
    public void ReloadBossHpUI(float hp)
    {
        BossSlider.value = hp;
    }
    
    public void ShowDamageText(int damageAmount, Vector3 worldPosition)
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        screenPosition += new Vector2(50, 50);
        Text damageText = Instantiate(damageTextPrefab, this.transform);
        damageText.rectTransform.position = screenPosition;
        damageText.text = "-" + damageAmount.ToString();

        Destroy(damageText.gameObject, 1.0f); // 1秒後銷毀顯示的傷害數字傷害數字
    }

    public void ChangeGunTank(int input)
    {
        if (!ReferenceEquals(skillUnderAnime , null))
        {
            skillUnderAnime.SetTrigger($"{input}");
        }
    }
    
    public void volumeSliderSet(float volumeValue)
    {
        AudioListener.volume = volumeValue;
        //Debug.Log(volumeValue);
    }

    public void lightSliderSet(float lightValue)
    {
        if (GameManager.mapLevel >= 2)
        {
            float mappedValue = (1 - lightValue) * 150f;
            lightSet.GetComponent<Image>().color = new Color32(0, 0, 0, (byte)mappedValue);
            //Debug.Log(lightValue);
            //Debug.Log(mappedValue);
        }
    }
    
    public void DebugText(string debug)
    {
        if (debugTextPrefab)
        {
            GameObject debugTextInstance = Instantiate(debugTextPrefab, DebugGrid.gameObject.transform);
            debugText = debugTextInstance.GetComponent<Text>();
            debugText.text = debug; 
            Color textColor = debugText.color;
            textColor.a = 1f; // 初始透明度为不透明
            debugText.color = textColor;
            StartCoroutine(FadeOutAndDestroy(debugTextInstance));
        }
    }
    
    private IEnumerator FadeOutAndDestroy(GameObject debugTextInstance)
    {
        // 等待一段时间（可根据需要调整）
        yield return new WaitForSeconds(1.0f);

        // 逐渐减小透明度
        for (float alpha = 1f; alpha >= 0f; alpha -= Time.deltaTime)
        {
            Color textColor = debugText.color;
            textColor.a = alpha;
            debugText.color = textColor;
            yield return null;
        }
        
        Color finalTextColor = debugText.color;
        finalTextColor.a = 0f;
        debugText.color = finalTextColor;
        Destroy(debugTextInstance);
        //DestroyImmediate(debugTextInstance, true);
    }

    [Button]
    public void PlayStartVideo()
    {
        //GameManager.instance.isStoping = true;
        animator.Play("noHourglassAppear");
        videoGameObject.SetActive(true);
        isPlayingVideo = true;
        videoPlayer.clip = GameStart;
        videoPlayer.Play();
        
        //isPlayingVideo = true;
        
        StartCoroutine(WaitForVideoToFinish());
    }
    
    [Button]
    public void PlayCutscenesVideo()
    {
        //GameManager.instance.isStoping = true;
        animator.Play("appear");
        videoGameObject.SetActive(true);
        isPlayingVideo = true;
        videoPlayer.clip = Cutscenes;
        videoPlayer.Play();

        // 开始视频准备
        videoPlayer.Prepare();
        //StartCoroutine(WaitForVideoToFinish());
    }
    
    private IEnumerator WaitForVideoToFinish()
    {
        yield return new WaitForSeconds(1f);
        while (videoPlayer.isPrepared && videoPlayer.isPlaying)
        {
            yield return null;
        }

        if (!videoPlayer.isPlaying)
        {
            // 视频播放完成后停止视频
            StopPlayVideo();
        }
    }
    
    [Button]
    public void StopPlayVideo()
    {
        animator.Play("disappear");
    }

    public void videoStop() //放在animation裡
    {
        isPlayingVideo = false;
        GameManager.instance.isStoping = false;
        videoPlayer.Stop();
        videoPlayer.clip = null;
    }
    
}
