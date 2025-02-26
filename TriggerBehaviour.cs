using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField]
    private string targetTag = "Player"; // Тег объекта, который мы ищем
    [SerializeField]
    private Material triggerOn;
    [SerializeField]
    private Material triggerOff;
    
    private bool isInside = false; // Флаг, находится ли объект внутри триггера
    private int numOfObjectsInside=0;

    void Start(){
        GetComponent<Renderer>().material = triggerOff;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag)) // Проверяем тег объекта
        {
            isInside = true;
            GetComponent<Renderer>().material = triggerOn;
            numOfObjectsInside++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            numOfObjectsInside--;
            if (numOfObjectsInside == 0)
            {
                isInside = false;
                GetComponent<Renderer>().material = triggerOff;
            }
        }
    }
}
