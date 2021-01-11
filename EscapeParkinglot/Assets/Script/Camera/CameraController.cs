using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController: MonoBehaviour
{
    private Vector3[] m_cameraPosition = new Vector3[3];
    private Vector3 m_nextPos = Vector3.zero;
    private int m_oldStageNum = 0;

    private void Awake()
    {
        Initialize();
    }

    /// <summary>
    /// 初期化する
    /// </summary>
    private void Initialize()
    {
        //ステージ毎のカメラ位置を指定して配列に入れる
        m_cameraPosition[(int)GameManager.StageNum.StageNum_First] = new Vector3(14.87f, 28.0f, -1.82f);
        m_cameraPosition[(int)GameManager.StageNum.StageNum_Second] = new Vector3(60.1f, 28.0f, -1.82f);
        m_cameraPosition[(int)GameManager.StageNum.StageNum_Third] = new Vector3(109.8f, 28.0f, -1.82f);
    }

    // Start is called before the first frame update
    void Start()
    {
        //現在のステージが設定したステージ以内なら実行する
        if (GameManager.Instance.GetNowStage() < (int)GameManager.StageNum.StageNum_Max &&
            GameManager.Instance.GetNowStage() > (int)GameManager.StageNum.StageNum_First)
        {
            //最初のカメラ位置を入力、設定する
            gameObject.transform.position = m_cameraPosition[GameManager.Instance.GetNowStage()];
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(MoveNextStage());
    }

    /// <summary>
    /// カメラの位置を移動する
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveNextStage()
    {
        //次のステージ位置が0じゃない時のみ実行
        if (m_nextPos != Vector3.zero)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, m_nextPos, 50.0f * Time.deltaTime);
        }
        yield return 0;
    }


    /// <summary>
    /// カメラの位置を設定する
    /// </summary>
    public void SetCameraPosition(int stagenumber)
    {
        //以前のステージではない時に動かす処理を実行
        if (m_oldStageNum != stagenumber)
        {
            //最後のステージを超えない時だけ実行する
            if (stagenumber < (int)GameManager.StageNum.StageNum_Max)
            {
                m_nextPos = Vector3.Lerp(gameObject.GetComponent<Transform>().position, m_cameraPosition[stagenumber], 1.0f);
            }
            //現在のステージを適用
            m_oldStageNum = stagenumber;
        }
    }
}
