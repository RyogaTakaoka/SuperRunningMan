using UnityEngine;

// フルーツを制御するスクリプト
public class Eat : MonoBehaviour
{
	public GameObject m_collectedPrefab;
    private GameObject player;
    PlayerController pCtl;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pCtl = player.GetComponent<PlayerController>();

    }

    // 他のオブジェクトと当たった時に呼び出される関数
    private void OnTriggerEnter2D( Collider2D col )
	{
		if ( col.gameObject.tag == "Player" )
		{
            if (this.gameObject.tag == "GoodMeat")
            {
                if (pCtl.status != 1)
                {
                    pCtl.status++;
                }
            }
            else if (this.gameObject.tag == "BadMeat")
            {
                if (pCtl.status != -1)
                {
                    pCtl.status --;
                }
            }
            gameObject.SetActive(false);

            // 獲得演出のオブジェクトを作成する
            var collected = Instantiate
			(
				m_collectedPrefab,
				transform.position,
				Quaternion.identity
			);

			// 獲得演出のオブジェクトからアニメーターの情報を取得する
			var animator = collected.GetComponent<Animator>();

			// 現在再生中のアニメーションの情報を取得する
			var info = animator.GetCurrentAnimatorStateInfo( 0 );

			// 現在再生中のアニメーションの再生時間（秒）を取得する
			var time = info.length;

			// アニメーションの再生が完了したら
			// 獲得演出を削除するように登録する
			Destroy( collected, time );
		}
	}
}