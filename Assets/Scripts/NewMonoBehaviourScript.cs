using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    void Start()
    {
        int[] numbers = { 1, 2, 3, 4, 5 };
        /*for(int i = 0; i < numbers.Length; i++)
        {
            Debug.Log(numbers[i]);
        }*/
        foreach(int item in numbers)
        {
            Debug.Log(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
