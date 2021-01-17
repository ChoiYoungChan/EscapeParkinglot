using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button m_retryButton = default;
    [SerializeField] private Text m_resultText = default;

    private string m_message = "Good!";

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
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// リトライボタンを押したら実行される関数
    /// </summary>
    public void ShowGameResult()
    {
        Debug.Log("Activate ShowGameResult func");
        m_resultText.gameObject.SetActive(true);

        m_resultText.fontSize = 100;
        m_resultText.text = "Good";
        m_resultText.color = Color.blue;

        if (GameManager.Instance.GetNowStage() == (int)(GameManager.StageNum.StageNum_Max))
        {
            Invoke("ShowRetryButton", 1.0f);
        }
    }

    /// <summary>
    /// リトライボタンを表示する
    /// </summary>
    private void ShowRetryButton()
    {
        m_retryButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// リトライボタンを押したら実行される関数
    /// </summary>
    public void DoRetryButton()
    {
        SceneManager.LoadScene(0);
    }
}
