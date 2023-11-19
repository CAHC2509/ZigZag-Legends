using UnityEngine;

[CreateAssetMenu(fileName = "CarData", menuName = "Car Data", order = 1)]
public class CarData : ScriptableObject
{
    public string carName;
    public int price;
    public bool unlocked;
    public bool revealed;
}
