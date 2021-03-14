using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EffectManager : MonoBehaviour
{

    [System.Serializable]
    public struct Effect
    {
        public string tag;
        public GameObject gameObject;
        public uint size;
    }
    public static EffectManager instance;

    // Dictionary는 직렬화가 불가능함으로 리스트를 활용한다
    [SerializeField]
    private List<Effect> effects;
    private Dictionary<string, Queue<GameObject>> objcetPool;
  
    private void Awake()
    {
        instance = this;

        // 신이 바뀌여도 파괴 안됨
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        objcetPool = new Dictionary<string, Queue<GameObject>>();
        foreach (var effect in effects)
        {
            Queue<GameObject> effextQueue = new Queue<GameObject>();
            for (uint i = 0; i < effect.size; i++)
            {
                GameObject obj = Instantiate(effect.gameObject);
                obj.transform.parent = transform;
                obj.SetActive(false);
                effextQueue.Enqueue(obj);
            }

            objcetPool.Add(effect.tag, effextQueue);
        }
    }

    void ReMake(string tag ,GameObject obj, int makeSize)
    {
        for (int i = 0; i < makeSize; i++)
        {
            GameObject newobj = Instantiate(obj);
            newobj.transform.parent = transform;
            newobj.SetActive(false);
            objcetPool[tag].Enqueue(newobj);
        }
    }

    public GameObject SpawnEffect(string tag, Vector3 position, Quaternion rotation, GameObject parent = null)
    {
        if (!(objcetPool.ContainsKey(tag)))
        {
            Debug.LogWarning("tag :" + tag + "No Data");
            return null;
        }

        // 갯수가 한개 남으면 20개 재생성
        if (objcetPool[tag].Count == 1)
            ReMake(tag, objcetPool[tag].Dequeue(), 20);
        
        GameObject gameObject = objcetPool[tag].Dequeue();
        gameObject.SetActive(true);
        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        if (parent != null)
            gameObject.transform.parent = parent.transform;
        
        return gameObject;
    }

    public void ReturnEffect(string tag, GameObject obj)
    {
        if (!(objcetPool.ContainsKey(tag)))
        {
            Debug.LogWarning("tag :" + tag + "No Data");
            return;
        }

        obj.transform.parent = transform;
        obj.SetActive(false);
        objcetPool[tag].Enqueue(obj);

    }
}
