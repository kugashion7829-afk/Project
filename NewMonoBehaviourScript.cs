using UnityEngine;
// UnityEngine名前空間を使用する

public sealed class ProjectStartupCheck : MonoBehaviour
// public: どこからでもアクセス可能
// sealed: このクラスは継承できない（派生クラスを作れない）
// MonoBehaviour: UnityのGameObjectにアタッチできるようにする基底クラス
{
    private void Start()
    // private: このクラス内からのみ呼び出し可能
    // void: 戻り値なし
    // Start: Unityが自動で呼び出す、初期化用のメソッド
    {
        Debug.Log("Project setup completed.");
        // コンソールにログを出力する
    }
}
