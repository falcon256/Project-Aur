using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour
{
    public GameObject surfaceGO = null;
    public GameObject cloudGO = null;
    public MeshRenderer surfaceMeshRenderer = null;
    public MeshRenderer cloudMeshRenderer = null;
    public Material surfaceMaterial = null;
    public Material cloudMaterial = null;
    public float planetSize = 1.0f; // 1.0 = 1 earth;
    public float planetMass = 1.0f; // 1.0 = 1 earth;
    private Texture2D surfaceDiffuse = null;
    private Texture2D surfaceEmission = null;
    private Texture2D surfaceDisplace = null;
    private Texture2D surfaceDetail = null;
    private Texture2D surfaceSpecular = null;
    private int textureWidth = 0;
    private int textureHeight = 0;
    private int generationSeed = 0;
    
    

    void Start()
    {
        textureWidth = Mathf.NextPowerOfTwo((int)planetSize * 256);
        textureHeight = Mathf.NextPowerOfTwo((int)((planetSize * 256) / 1.5f));
        surfaceDiffuse = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
        surfaceEmission = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
        surfaceDisplace = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
        surfaceDetail = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
        surfaceSpecular = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
        initializeRGBA32HeightTexture(surfaceDisplace);
        initializeRGBA32HeightTexture(surfaceDetail);
        //for(int i = 0; i < surfaceMaterial.GetTexturePropertyNames().Length; i++)
        //     Debug.Log(surfaceMaterial.GetTexturePropertyNames()[i]);
        surfaceMaterial.SetTexture("_ParallaxMap", surfaceDisplace);
        surfaceMaterial.SetTexture("_BumpMap", surfaceDetail);


    }
    
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        this.transform.Rotate(0, 0.1f, 0);

    }

    private void initializeR16Texture(Texture2D tex)
    {
        if (tex == null)
        {
            Debug.Log("Weeeeee!!! NULLLLL");
            return;
        }
        byte[] bits = new byte[tex.width * tex.height * 2];
        for(int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {
                short nibble = (short)(Perlin.OctavePerlin(x / 0.001f, y / 0.001f, 0.5, 4, 0.5) * 65536.0);
                bits[(x * 2) + (y * 2 * tex.width)] = (byte)(nibble>>8);
                bits[(x * 2) + (y * 2 * tex.width) + 1] = (byte)nibble;// this needs testing, really not sure if we are going big endian or little endian
                Debug.Log(nibble);
            }

        }
       
    }


    private void initializeRGBA32HeightTexture(Texture2D tex)
    {
        if (tex == null)
        {
            Debug.Log("Weeeeee!!! NULLLLL");
            return;
        }
        var data = tex.GetRawTextureData<Color32>();
        float equatorMultiplier = 0;
        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {
                equatorMultiplier = Mathf.Min(0 + y + generationSeed, tex.height - y + generationSeed) /((float)(tex.height));
                byte n = (byte)(Perlin.OctavePerlin(x / ((float)tex.width/16.0f), y / ((float)tex.height/16.0f), 0.5,10, 0.5) * 255 * equatorMultiplier);
                Color32 nn = new Color32(n, n, n, 255);
                data[x + y * tex.width] = nn;
            }

        }
        tex.filterMode = FilterMode.Point;
        tex.wrapMode = TextureWrapMode.Repeat;
        tex.Apply();
    }
}
