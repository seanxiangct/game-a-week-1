using UnityEngine;

public class DuckMovement : MonoBehaviour
{
    [SerializeField] private Player _player;
    private float angleBetween;
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDir = _player.transform.position - transform.position;
        angleBetween = Vector3.Angle(transform.forward, targetDir);
        transform.up = targetDir;
        gameObject.transform.Translate((new Vector3(0,(speed*Time.deltaTime),0)));
    }
}
