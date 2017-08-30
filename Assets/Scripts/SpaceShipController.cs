using UnityEngine;

[System.Serializable]
public class Boundary1D {

    public float xMin, xMax;
}

public class SpaceShipController : MonoBehaviour {

    public float speed = 0.07f;
    public Boundary1D boundary;

    public Transform shotSpawn;

    public float fireRate = 0.3F;
    private float nextFire = 0.3F;
    private float myTime = 0.0F;

    void Start ()
    {
		
	}
	
	void Update ()
    {
        CheckInput();
	}

    void CheckInput()
    {
        Vector2 pos = transform.position;
        // Move
        // TODO: https://docs.unity3d.com/ScriptReference/Input.GetAxis.html
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.position = new Vector2(Mathf.Clamp(pos.x - speed, boundary.xMin, boundary.xMax), pos.y);
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            transform.position = new Vector2(Mathf.Clamp(pos.x + speed, boundary.xMin, boundary.xMax), pos.y);
        }

        myTime = myTime + Time.deltaTime;
        if (Input.GetButton("Fire1") && myTime > nextFire) {
            nextFire = myTime + fireRate;

            GameObject boltGameObject = StaticShotPool.Instance.GetShot();
            if (boltGameObject != null) {
                boltGameObject.transform.position = shotSpawn.position;
                boltGameObject.transform.rotation = shotSpawn.rotation;
                boltGameObject.SetActive(true);

                nextFire = nextFire - myTime;
                myTime = 0.0F;
            }
        }
    }
}
