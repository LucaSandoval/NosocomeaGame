using UnityEngine;

public class PulsatingObject : MonoBehaviour
{
    public float maxSize = 5f; // maximum size for the object
    public float growthRate = 0.1f; // rate at which the object grows
    public float fadeOutTime = 1.0f; // time it takes for the object to fade out
    public Material objectMaterial; // material for the object
    public float emissionIntensity = 1f; // intensity of the object's emission

    private Material objectMaterialCopy; // copy of the material for the object
    private float timeElapsed = 0f; // time elapsed since object reached its maximum size
    private bool isFadingOut = false; // flag to track whether the object is currently fading out

    private void Start()
    {
        // make a copy of the material
        objectMaterialCopy = new Material(objectMaterial);
        float tiling = Random.Range(0.6f, 1f); // randomize tiling between 0.6 and 1
        objectMaterialCopy.mainTextureScale = new Vector2(tiling, tiling); // set the tiling of the material
        GetComponent<MeshRenderer>().material = objectMaterialCopy;

        // set the initial emission intensity of the object
        objectMaterialCopy.EnableKeyword("_EMISSION");
        objectMaterialCopy.SetColor("_EmissionColor", objectMaterialCopy.color * emissionIntensity);
    }

    private void Update()
    {
        if (transform.localScale.x < maxSize)
        {
            transform.localScale += new Vector3(growthRate, growthRate, growthRate); // increase size of the object
        }
        else if (!isFadingOut) // if the object has reached its maximum size and is not currently fading out
        {
            isFadingOut = true; // set the flag to indicate that the object is now fading out
            timeElapsed = 0f; // reset the time elapsed counter
        }

        if (isFadingOut)
        {
            timeElapsed += Time.deltaTime;
            float fadeAmount = Mathf.Clamp01(timeElapsed / fadeOutTime); // calculate the fade amount between 0 and 1

            // fade out the object's emission intensity
            objectMaterialCopy.SetColor("_EmissionColor", objectMaterialCopy.color * (1f - fadeAmount) * emissionIntensity);

            if (fadeAmount >= 1f)
            {
                Destroy(gameObject);
            }
        }
    }
}



// using UnityEngine;

// public class PulsatingObject : MonoBehaviour
// {
//     public float maxSize = 5f; // maximum size for the object
//     public float growthRate = 0.1f; // rate at which the object grows
//     public float fadeOutTime = 1.0f; // time it takes for the object to fade out
//     public Material objectMaterial; // material for the object

//     private Material objectMaterialCopy; // copy of the material for the object
//     private float timeElapsed = 0f; // time elapsed since object reached its maximum size
//     private bool isFadingOut = false; // flag to track whether the object is currently fading out

//     private void Start()
//     {
//         // make a copy of the material
//         objectMaterialCopy = new Material(objectMaterial);
//         float tiling = Random.Range(0.6f, 1f); // randomize tiling between 0.6 and 1
//         objectMaterialCopy.mainTextureScale = new Vector2(tiling, tiling); // set the tiling of the material
//         GetComponent<MeshRenderer>().material = objectMaterialCopy;
//     }

//     private void Update()
//     {
//         if (transform.localScale.x < maxSize)
//         {
//             transform.localScale += new Vector3(growthRate, growthRate, growthRate); // increase size of the object
//         }
//         else if (!isFadingOut) // if the object has reached its maximum size and is not currently fading out
//         {
//             isFadingOut = true; // set the flag to indicate that the object is now fading out
//             timeElapsed = 0f; // reset the time elapsed counter
//         }

//         if (isFadingOut)
//         {
//             timeElapsed += Time.deltaTime;
//             float fadeAmount = Mathf.Clamp01(timeElapsed / fadeOutTime); // calculate the fade amount between 0 and 1
//             Color materialColor = objectMaterialCopy.color;
//             materialColor.a = Mathf.Lerp(1f, 0f, fadeAmount); // lerp the transparency value
//             objectMaterialCopy.color = materialColor;

//             if (fadeAmount >= 1f)
//             {
//                 Destroy(gameObject);
//             }
//         }
//     }
// }
