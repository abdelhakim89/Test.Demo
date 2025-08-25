using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {

    }
    private void OnMouseDown()
    {
        GameObject.Find("SavingManager").GetComponent<JsonTest>().UpdateScore(10);
        Destroy(gameObject);
    }
}
