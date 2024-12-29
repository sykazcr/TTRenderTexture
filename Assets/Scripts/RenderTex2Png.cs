using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class RenderTex2PNG : MonoBehaviour
{
    [SerializeField] private RenderTexture _renderTex;
    [SerializeField] private bool _continue_out_flag=false;
    private Texture2D _texture;
    byte[] m_pngmem;
    MemoryMappedFile mmf;
    Mutex mutex;
    MemoryMappedViewAccessor accessor;

    private void Start()
    {
        /*MemoryMappedFile*/ mmf = MemoryMappedFile.CreateOrOpen("Global/MySharedMemory", 1024 * 40);
        /*Mutex*/ mutex = new Mutex(false, "Global/MySharedMemoryMutex");
        /*MemoryMappedViewAccessor*/ accessor = mmf.CreateViewAccessor();
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            Capture();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            _continue_out_flag = true;
        }
        if (_continue_out_flag)
        {
            Capture();
        }

    }

    public void Capture()
    {

        if (_texture == null)
        {
            _texture = new Texture2D(
                _renderTex.width, _renderTex.height, TextureFormat.RGBA32, false
            );
        }
        AsyncGPUReadback.Request(_renderTex, 0, _onRequest);

    }

    private void _onRequest(AsyncGPUReadbackRequest request)
    {


        if (request.hasError)
        {
            Debug.LogError("Error.");
        }
        else
        {
            if (_texture == null) {
                Debug.Log("_texture null access");
                return; 
            }
            var data = request.GetData<Color32>();
            _texture.LoadRawTextureData(data);
            _texture.Apply();
        }

        //var str = System.DateTime.Now.ToString("yyMMddHHmmssfff");
        //String path = "e:/utlog/";
        //File.WriteAllBytes(
        //    //$"{Application.dataPath}/image" + str + ".png",
        //    path + "image" + str + ".png",
        //    _texture.EncodeToPNG()
        //);

        //byte[] pngmem = _texture.EncodeToPNG();

        // Create or open a named mutex
        //using (Mutex mutex = new Mutex(false, "Global\\MySharedMemoryMutex"))
        {
            // Create or open a memory-mapped file
            //using (MemoryMappedFile mmf = MemoryMappedFile.CreateOrOpen("Global\\MySharedMemory", 1024 * 40))
            {
                // Lock the mutex to ensure exclusive access
                mutex.WaitOne();

                try
                {
                    // Write to the shared memory
                    //using (MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor())
                    {
                        byte[] pngmem = _texture.EncodeToPNG();
                        //byte[] message = System.Text.Encoding.UTF8.GetBytes("Hello from Process 1");
                        //accessor.WriteArray(0, message, 0, message.Length);
                        accessor.WriteArray(0, pngmem, 0, pngmem.Length);
                    }
                }
                finally
                {
                    // Release the mutex
                    mutex.ReleaseMutex();
                }
            }
        }


    }

    private static string _AppendTimeStamp(string fileName)
    {
        return string.Concat(
            Path.GetFileNameWithoutExtension(fileName),
            DateTime.Now.ToString("yyyyMMddHHmmssfff"),
            Path.GetExtension(fileName)
            );
    }


}