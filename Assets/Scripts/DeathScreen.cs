using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRestartClicked()
    {
        GameManager.Instance.Restart();
    }
}
