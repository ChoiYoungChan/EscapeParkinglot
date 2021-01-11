using UnityEngine;
using System;

/// <summary>
/// シングルトンクラス
/// </summary>
/// <typeparam name="T">クラス</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            //インスタンス化されてなかったときのみ実行
            if (instance == null)
            {
                Type t = typeof(T);

                instance = (T)FindObjectOfType(t);
                if (instance == null)
                {
                    //エラー処理
                    Debug.LogError(t + ":Null");
                }
            }
            //インスタンスを返す
            return instance;
        }
    }

    virtual protected void Awake()
    {
        //他のゲームオブジェクトにアタッチされていた場合は破棄
        CheckInstance();
    }

    /// <summary>
    /// 他のゲームオブジェクトにアタッチされていないかを
    /// </summary>
    /// <returns>アタッチされていたかどうか</returns>
    protected bool CheckInstance()
    {
        if (instance = null)
        {
            instance = this as T;
            return true;
        }
        else if (Instance == this)
        {
            return true;
        }
        Destroy(this);
        return false;
    }
}