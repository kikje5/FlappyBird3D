using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + rotationSpeed * Time.deltaTime, 0);
    }
}
