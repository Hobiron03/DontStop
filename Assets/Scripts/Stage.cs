using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour {

    //blockの種類
    private const char B_BLOCK = 'b';
    private const char W_BLOCK = 'w';
    private const char S_BLOCK = 's';

    public GameObject blackBlock;
    public GameObject whiteBlock;
    public GameObject startBlock;

    // マップのデータ.
    public TextAsset m_defaultMap;
    public TextAsset[] m_map_texasset;

    public GameObject stages;


    // stageの構造体.
    struct StageData
    {
        public int width;
        public int length;
        public int offset_x;    // data[0][0]はポジションoffset_x,offset_zのブロックを示す.
        public int offset_z;
        public char[,] data;
        public float[,] height;

        public int[,] gemParticleIndex;
    };

    private StageData stageData;

    // Use this for initialization
    void Start ()
    {
        LoadFromAsset(m_defaultMap);
        CreateMap();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //mapデータの読み込み
    private void LoadFromAsset(TextAsset asset)
    {
        string txtMapData = asset.text;

        //splitメソッドで、からの要素を削除するためのオプション
        System.StringSplitOptions option = System.StringSplitOptions.RemoveEmptyEntries;

        //一行ずつ取り出す
        string[] lines = txtMapData.Split(new char[] {'\r', '\n'}, option);

        //','区切りで１文字ずつとりだす
        char[] spliter = new char[1] { ',' };

        string[] sizewh = lines[0].Split(spliter, option);
        stageData.width = int.Parse(sizewh[0]);
        stageData.length = int.Parse(sizewh[1]);

        char[,] mapdata = new char[stageData.length, stageData.width];

        for (int lineCnt = 0; lineCnt < stageData.length; lineCnt++)
        {
            string[] data = lines[stageData.length - lineCnt].Split(spliter,option);

            for (int col = 0; col < stageData.width; col++)
            {
                mapdata[lineCnt, col] = data[col][0];
            }
        }
        stageData.data = mapdata;
    }

    void CreateMap()
    {
        for (int x = 0; x < stageData.width; x++)
        {
            for (int z = 0; z < stageData.length; z++)
            {
                //blockの座標
                Vector3 blockPos = new Vector3(-0.5f + x, -0.25f, -z);

                GameObject obj;
                switch (stageData.data[z,x])
                {
            
                    case S_BLOCK:
                        obj = Instantiate(startBlock, blockPos, Quaternion.identity);
                        obj.transform.parent = stages.transform;
                        break;
                    case B_BLOCK:
                        obj = Instantiate(blackBlock, blockPos, Quaternion.identity);
                        obj.transform.parent = stages.transform;
                        break;
                    case W_BLOCK:
                        obj = Instantiate(whiteBlock, blockPos, Quaternion.identity);
                        obj.transform.parent = stages.transform;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
