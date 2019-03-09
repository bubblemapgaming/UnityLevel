using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : Interactable {

    [SerializeField]
    private float detonationTimer = 3.0f;
    private bool fuseLit;

    //Explostion Vars
    [SerializeField]
    private float explosionForce = 1000;
    [SerializeField]
    private float explosionRadius = 100;

    //Text Prefab
    [SerializeField]
    private GameObject textPrefab;

    //Explosion Particles Prefab
    [SerializeField]
    private GameObject explosionPrefab;

    public enum placeType
    {
        placed,
        preview
    }

    //Store our type
    public placeType placedType;

    //Subscribes/Unsubscribes for events
    void OnEnable()
    {
        BombPlacement.DetonateBombs += LightFuse;
    }

    void OnDisable()
    {
        BombPlacement.DetonateBombs -= LightFuse;
    }

    // Update is called once per frame
    void Update () {

        //If fuse is lit reduce the detonation timer by delta time then check if we shoud explode
        if (fuseLit)
        {
            detonationTimer -= Time.deltaTime;

            if(detonationTimer <= 0.0f)
            {
                Explode();
            }

        }

	}

    /// <summary>
    /// Script makes the bomb explode, triggering the explsoion force and then creating a particle effect, then destroying the game object
    /// </summary>
    private void Explode()
    {
        //Get all coliders in a sphere radius of 5 around the bomb
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);

        foreach (Collider collider in colliders)
        {

            //Get the rigidbody
            Rigidbody r = collider.GetComponent<Rigidbody>();

            //If object has a rigidbody, then apply explsion force
            if (r != null)
            {
                //Add explosion force at bomb postion
                r.AddExplosionForce(explosionForce, transform.position, explosionRadius, 2, ForceMode.Impulse);
            }

        }

        //Instanitate Particle Prefab and thus make explosion 
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.transform.position = transform.position;
        explosion.transform.parent = transform.parent;

        //Scale Bomb Particles
        //Set Scale to one
        explosion.GetComponent<ParticleSystem>().transform.localScale = Vector3.one;

        //Get the estimated truer scale (based on parents)
        //E.G If parents scale is 2, out local scale is one. Our true scale is 2
        Vector3 trueScale = explosion.GetComponent<ParticleSystem>().transform.lossyScale;
        //Set localscale to 1/truescale. Compensate for the parent scale
        explosion.GetComponent<ParticleSystem>().transform.localScale = new Vector3(1 / trueScale.x, 1 / trueScale.y, 1 / trueScale.z);

        //Destory this object
        Destroy(gameObject);
    }

    /// <summary>
    /// Changes the state of having the fuse lit to true
    /// </summary>
    public void LightFuse()
    {
        fuseLit = true;

        //Create 3D Text to show countdown
       /* if (placedType == placeType.placed)
        {
            GameObject text = Create3DText("TEST", textPrefab, transform.position, new Vector3(0,0,0))l
        }*/
    }

}
