using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button m_retryButton = default;
    [SerializeField] private Text m_resultText = default;
    [SerializeField] private Text m_timeText = default;

    private float m_nowTime = default;

    private string[] m_message =
   {
        "Good!",
        "Failed"
    };

    private enum nowUIState
    {
        UIState_None = -1,
        UIState_Clear,
        UIState_Faill,
        UIState_Max
    };

    private void Awake()
    {
        Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// UIを初期化する
    /// </summary>
    public void Initialize()
    {
        m_resultText.gameObject.SetActive(false);
        m_retryButton.gameObject.SetActive(false);
        m_nowTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// リトライボタンを押したら実行される関数
    /// </summary>
    public void DoRetryButton()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// リトライボタンを押したら実行される関数
    /// </summary>
    public void ShowGameResult()
    {
        string message = default;
        if(GameManager.Instance.GetStageClear() == true)
        {
            message = m_message[0];
        }
        else
        {
            message = m_message[1];
        }

        m_resultText.text = message;
    }

    /// <summary>
    /// リトライボタンを表示する
    /// </summary>
    private void ShowRetryButton()
    {
        m_retryButton.gameObject.SetActive(true);
    }


    private void ShowTimeText()
    {

    }
}
