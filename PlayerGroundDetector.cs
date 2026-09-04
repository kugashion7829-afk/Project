using UnityEngine;

namespace PerspectiveShift.Player
{
    /// <summary>
    /// PerspectiveShiftの中にあるPlayerという名前空間に属している
    /// </summary>

    public sealed class PlayerGroundDetector : MonoBehaviour
    {
    /// <summary>
    /// public どのクラスからも呼び出せる
    /// sealed 継承できない
    /// class クラスを作成
    /// PlayerGroundDetector クラス名
    /// MonoBehaviour UnityでComponentとして使用できる
    /// </summary>
    
    [Header("Ground Check")]
    [SerializeField]
    private Transform groundCheck;
    /// <summary>
    /// Header HTMLの様に見出しを設定
    /// "Ground Check" 見出しの内容
    /// SerializeField private変数をInspecterに表示する
    /// private 他のクラスから参照できない
    /// Transform 変数の型 Unity独自の型 Transform型
    /// groundCheck 変数名
    /// 処理 objectの位置を判定
    /// </summary>

    [SerializeField]
    [Min(0.01f)]
    private float groundCheckRadius = 0.18f;
    /// <summary>
    /// SerializeField private変数をInspecterに表示する
    /// Min Inspecterから設定できる値の最低値を0.01f floatに設定
    /// private 他のクラスから参照できない
    /// float 変数の型 float型 小数 浮動小数点数
    /// groundCheckRadius 変数名
    /// 0.18f 初期値 0.18 float
    /// 処理 地面と接地しているかどうか判定する範囲を指定
    /// </summary>
    
    [SerializeField]
    private LayerMask groundLayers;
    /// <summary>
    /// LayerMask 変数の型 Unity独自 LayerMask型
    /// 処理 地面として扱うUnityのLayer選べるように設定
    /// </summary>
    
    public bool IsGrounded
        {
            get
            {
                if (groundCheck == null)
                {
                    return false;
                }

                return Physics.CheckSphere(
                    groundCheck.position,
                    groundCheckRadius,
                    groundLayers,
                    QueryTriggerInteraction.Ignore
                );
            }
        }
        /// <summary>
        /// どのクラスからでもアクセス可能なbool型のIsGroundedプロパティを作成、
        /// getプロパティを実行して、groundCheckに値が代入されているかどうか判定
        /// trueならUnity独自のPhysics機能のCheckSpehreを使ってtrue or falseを判定している
        /// 引数には上記で記述した変数が含まれる。
        /// 処理 足元の位置を判定し、足の位置から半径xのうちに特定のLayerがあるかどうかを判定
        /// あればtrue、なければfalse
        /// 
        /// QueryTriggerInteraction.Ignore Trigger Colliderを判定対象から無視する
        /// </summary>
    
        private void Awake()
        {
            if (groundCheck == null)
            {
                Debug.LogError(
                    "PlayerGroundDetecterにGround Checkが設定されていません。",
                    this
                );
            }   

            if (groundLayers.value == 0)
            {
                Debug.LogWarning(
                    "PlayerGroundDetectorのGround Layersが未設定です。",
                    this
                );
            }
        }
        /// <summary>
        /// void 返り値を出さない
        /// Awake Unityが決まったタイミングで呼び出すメソッド(関数)
        /// componentが初期化された際にメソッドを実行
        /// groundCheckが指定されていなかったらエラーログを出力
        /// groundLayersが一つも設定されていなかったら注意ログを出力
        /// this consoleのエラーをクリックした際にエラーが出た箇所を注視してくれる
        /// </summary>

        private void OnValidate()
        {
            groundCheckRadius = Mathf.Max(0.01f, groundCheckRadius);
        }
        /// <summary>
        /// OnVaildate Unityが用意しているメソッド componentの値をinspecterで変更したときなどに呼び出される
        /// Mathf.Max() 引数の数値２つを比較して、大きい方を返す
        /// 数値が変更された際、入力された値が0.01fより小さい場合、0.01fが数値として代入されるように設定している
        /// </summary>

        private void OnDrawGizmosSelected()
        {
            if (groundCheck == null)
            {
                return;
            }

            Gizmos.color = IsGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
        /// <summary>
        /// OnDrawGizmosSelected Unityが用意している自動で呼び出しを行うメソッド
        /// Unity Editorで、componentが設定されているGameObjectが選択されているときにGizmosを描画する
        /// groundCheckが設定されていない場合処理を終了
        /// Gizmos.color Gizmosの色を設定している。
        /// \? : 三項演算子 条件 ? 条件がtrueの場合 : 条件がfalseの場合
        /// Gizmos.DrawWireSphere 指定した位置を中心に、指定した半径のワイヤー球を描画
        /// </summary>
    }
}