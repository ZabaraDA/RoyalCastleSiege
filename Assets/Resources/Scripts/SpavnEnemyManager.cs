using UnityEngine;

public class SpavnEnemyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnContainerGameObject;


    private void Start()
    {
        var waves = JsonReaderService.ReadJsonInResources<Wave>("Json/Waves");
    }
}
