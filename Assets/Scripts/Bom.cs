using UnityEngine;

public class Bom : MonoBehaviour
{
    public float deleteTime = 3.0f;

    //生成されて数秒後に削除
    void Start()
    {
        Destroy(gameObject, deleteTime);
    }
}
