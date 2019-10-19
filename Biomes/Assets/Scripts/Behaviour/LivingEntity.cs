using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    public int colourMaterialIndex;
    public Specimen Specimen;
    public Material material;

    public int myAge;
    public int myLifeSpan;

    public Coord coord;

    [HideInInspector]
    public int mapIndex;
    [HideInInspector]
    public Coord mapCoord;

    protected bool dead;

    public virtual void Init(Coord coord)
    {
        this.coord = coord;
        transform.position = Environment.tileCentres[coord.x, coord.y];

        // Set material to the instance material
        var meshRenderer = transform.GetComponentInChildren<MeshRenderer>();
        for (int i = 0; i < meshRenderer.sharedMaterials.Length; i++)
        {
            if (meshRenderer.sharedMaterials[i] == material)
            {
                material = meshRenderer.materials[i];
                break;
            }
        }

        switch (Specimen)
        {
            case Specimen.Undefined:
                break;
            case Specimen.Plant:
                myLifeSpan = Random.Range(1, 76);
                break;
            case Specimen.Rabbit:
                myLifeSpan = Random.Range(1, 3);
                break;
            case Specimen.Fox:
                myLifeSpan = Random.Range(1, 6);
                break;
            default:
                myLifeSpan = 0;
                Debug.LogWarning("No life span set for " + gameObject.name);
                break;
        }
    }

    public void Age()
    {
        myAge++;

        if (myAge > myLifeSpan)
            Die(CauseOfDeath.Age);
    }

    protected virtual void Die(CauseOfDeath cause)
    {
        if (!dead)
        {
            dead = true;
            Environment.RegisterDeath(this);
            Destroy(gameObject);
        }
    }
}