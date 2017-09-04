using PlainObjects;
using UnityEngine;

[System.Serializable]
public class Boundary1D
{
    public float XMin;
    public float XMax;
}

public class PlayerShipController : MonoBehaviour
{
    public Transform ShotSpawn;
    public Boundary1D Boundary;

    public float Speed;
    public float FireRate;

    public int ShotPoolSize;
    public GameObject PlayerShot;
    public GameObject PlayerExplosion;

    private float _remainingCoolDownTime;
    private ShotPool ShotPool { get; set; }
    private PlayerShipHealth PlayerShipHealth { get; set; }
    private const bool ShotPoolGrow = false;

    private void Start()
    {
        ShotPool = new ShotPool(ShotPoolSize, ShotPoolGrow, PlayerShot);
        PlayerShipHealth = GetComponent<PlayerShipHealth>();
    }

    private void Update ()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        Vector2 pos = transform.position;
        // Move input.
        // TODO: https://docs.unity3d.com/ScriptReference/Input.GetAxis.html
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = Move(pos.x - Speed, pos.y);
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = Move(pos.x + Speed, pos.y);
        }

        // Only shoot if we are requested to and if the weapon has finished cooling down.
        _remainingCoolDownTime -= Time.deltaTime;
        if (Input.GetButton("Fire1") && _remainingCoolDownTime < 0) Shoot();
    }

    private Vector2 Move(float newX, float newY)
    {
        return new Vector2(Mathf.Clamp(newX, Boundary.XMin, Boundary.XMax), newY);
    }

    private void Shoot()
    {
        // Get the bolt to shoot.
        GameObject boltGameObject = ShotPool.Dequeue();
        if (boltGameObject == null) return; // There was no bolt => cannot shoot.

        // Position the bolt to be correctly fired & enable it
        boltGameObject.transform.position = ShotSpawn.position;
        boltGameObject.transform.rotation = ShotSpawn.rotation;
        boltGameObject.SetActive(true);

        // Cold down the weapon
        _remainingCoolDownTime = FireRate;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: handle loose case when spaceships reaches some close to player
        var shot = other.GetComponent<Shot>();
        PlayerShipHealth.TakeShot(shot.Damage);
        Instantiate(PlayerExplosion, transform.position, transform.rotation);
    }
}
