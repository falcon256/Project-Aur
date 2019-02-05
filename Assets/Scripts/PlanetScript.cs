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
    private Texture2D surfaceDiffuse = null;
    private Texture2D surfaceEmission = null;
    private Texture2D surfaceDisplace = null;
    private Texture2D surfaceSpecular = null;
    

    void Start()
    {

        surfaceDiffuse = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        surfaceEmission = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        surfaceDisplace = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        surfaceSpecular = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        initializeRGBA32Texture(surfaceDisplace);
        //for(int i = 0; i < surfaceMaterial.GetTexturePropertyNames().Length; i++)
       //     Debug.Log(surfaceMaterial.GetTexturePropertyNames()[i]);
        surfaceMaterial.SetTexture("_ParallaxMap", surfaceDisplace);
        

        
    }
    
    void Update()
    {
        
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
    private void initializeRGBA32Texture(Texture2D tex)
    {
        if (tex == null)
        {
            Debug.Log("Weeeeee!!! NULLLLL");
            return;
        }
        var data = tex.GetRawTextureData<Color32>();
        byte[] bits = new byte[tex.width * tex.height * 4];
        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {
                byte n = (byte)(Perlin.OctavePerlin(x / 0.1f, y / 0.1f, 0.5, 8, 0.1) * 255);
                //tex.SetPixel(x, y, new Color(n, 0, 0, n));
                Color32 nn = new Color32(n, n, n, 255);
                data[x + y * tex.width] = nn;
                //bits[(x * 4) + (y * 4 * tex.width)] =   (byte) nibble;
                //bits[(x * 4) + (y * 4 * tex.width)+1] = (byte)(nibble << 8);
                //bits[(x * 4) + (y * 4 * tex.width)+2] = (byte)(nibble << 16);
                //bits[(x * 4) + (y * 4 * tex.width)+3] = (byte)(nibble << 24);
                //bits[(x * 4) + (y * 4 * tex.width)] = 0;
                //bits[(x * 4) + (y * 4 * tex.width)+1] = 0;
                //bits[(x * 4) + (y * 4 * tex.width)+2] = 0;
                //bits[(x * 4) + (y * 4 * tex.width)+3] = 0;
                // Debug.Log(nibble);
            }

        }
        Debug.Log("Fuckle");
        //tex.LoadRawTextureData(bits);
        tex.Apply();
    }
}
