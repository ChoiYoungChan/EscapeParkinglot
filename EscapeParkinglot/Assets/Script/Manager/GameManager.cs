using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject m_UIManagerObject = default;
    [SerializeField] private GameObject m_stage_01 = default, m_stage_02 = default, m_stage_03 = default;
    [SerializeField] private Camera m_mainCamera;

    private bool m_isStageClear = false;

    private int m_nowStageNum = 0;
    private UIManager m_uiManager;

    // ステージを管理するためのEnum
    public enum StageNum
    {
        StageNum_First = 0,
        StageNum_Second,
        StageNum_Third,
        StageNum_Max
    }

    private void Awake()
    {
        CheckSaveData();
    }

    /// <summary>
    /// ゲーム起動時に以前にプレイしたデータがあれば持ってくる
    /// </summary>
    private void CheckSaveData()
    {
        //現在のステージデータを取得する処理
        m_nowStageNum = PlayerPrefs.GetInt("STAGE_NO");
        //最後のステージを超えれば最初のステージに移動する
        if (m_nowStageNum > (int)StageNum.StageNum_Max || m_nowStageNum < (int)StageNum.StageNum_First)
        {
            m_nowStageNum = (int)StageNum.StageNum_First;
        }
    }

    /// <summary>
    /// 最終的に到達したステージデータを保存
    /// </summary>
    private void SaveStageData()
    {
        //現在のステージデータを保存する処理
        PlayerPrefs.SetInt("STAGE_NO", m_nowStageNum);
        if (m_nowStageNum > (int)StageNum.StageNum_Max || m_nowStageNum < (int)StageNum.StageNum_First)
        {
            m_nowStageNum = (int)StageNum.StageNum_First;
            PlayerPrefs.SetInt("STAGE_NO", m_nowStageNum);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    private void Initialize()
    {
        InitializeData();
        InitializeStage();
        InitializeUI();
        InitializeParticle();
    }

    /// <summary>
    /// Initialize UI
    /// </summary>
    private void InitializeData()
    {
        m_isStageClear = false;
        m_uiManager = m_UIManagerObject.GetComponent<UIManager>();
    }

    /// <summary>
    /// Initialize UI
    /// </summary>
    private void InitializeUI()
    {
        m_uiManager.Initialize();
    }

    /// <summary>
    /// Initialize Particle Objects
    /// </summary>
    private void InitializeParticle()
    {

    }
    /// <summary>
    /// Initialize Stage Data
    /// </summary>
    private void InitializeStage()
    {
        if (m_nowStageNum < (int)StageNum.StageNum_First || m_nowStageNum >= (int)StageNum.StageNum_Max)
        {
            m_nowStageNum = (int)StageNum.StageNum_First;
        }

        switch (m_nowStageNum)
        {
            case (int)StageNum.StageNum_First:
                m_stage_01.SetActive(true);
                m_stage_02.SetActive(false);
                m_stage_03.SetActive(false);
                break;
            case (int)StageNum.StageNum_Second:
                m_stage_01.SetActive(false);
                m_stage_02.SetActive(true);
                m_stage_03.SetActive(false);
                break;
            case (int)StageNum.StageNum_Third:
                m_stage_01.SetActive(false);
                m_stage_02.SetActive(false);
                m_stage_03.SetActive(true);
                break;
            default:
                m_stage_01.SetActive(true);
                m_stage_02.SetActive(false);
                m_stage_03.SetActive(false);
                break;
        }
    }

    /// <summary>
    /// Move to next stage
    /// </summary>
    private void MoveStage()
    {
        m_mainCamera.GetComponent<CameraController>().SetCameraPosition(m_nowStageNum);
        Initialize();
    }

    /// <summary>
    /// Function which will call when game is clear
    /// </summary>
    public void GameClear()
    {
        m_nowStageNum++;
        SaveStageData();
        m_uiManager.ShowGameResult();

        //最後のステージからはステージを移動しない
        if (m_nowStageNum >= (int)StageNum.StageNum_First && m_nowStageNum < (int)StageNum.StageNum_Max)
        {
            Invoke("MoveStage", 1.5f);
        }
    }

    /// <summary>
    /// クリアFlagを取得できるようにする関数
    /// </summary>
    /// <returns></returns>
    public bool GetStageClear()
    {
        return m_isStageClear;
    }

    /// <summary>
    /// クリアFlag変更を取得する関数
    /// </summary>
    /// <returns></returns>
    public bool SetStageClear(bool status)
    {
        m_isStageClear = status;
        return m_isStageClear;
    }

    /// <summary>
    /// 現在のステージ番号を取得できるようにする関数
    /// </summary>
    /// <returns></returns>
    public int GetNowStage()
    {
        return m_nowStageNum;
    }

}
