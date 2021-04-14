using UnityEditor;
using UnityEngine;

public class InitMap
{
    [MenuItem("Tools/初始化地图", false, 0)]
    static void Init()
    {
        GameObject map1GameObject = GameObject.Find("mapSub1");
        string name1 = "mountain01";
        int[][] map1 =
        {
            new[] {3,4,5,6,7,8,9,10,11,12},
            new[] {1,2,3,4,5,6,7,8,9,10,11,12,13},
            new[] {1,2,3,4,5,6,7,8,9,10,11,12,13},
            new[] {1,2,3,4,5,6,7,8,9,10,11,12,13}
        };
        CreatMap(map1GameObject.transform, map1, name1);
        GameObject map2GameObject = GameObject.Find("mapSub2");
        string name2 = "mountain02";
        int[][] map2 =
        {
            new[] {5,6,7,8,9,10,11,12,13},
            new[] {2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17},
            new[] {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17},
            new[] {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17},
            new[] {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17}
        };
        CreatMap(map2GameObject.transform, map2, name2);

    }

    static void CreatMap(Transform mapTransform, int[][] map, string name)
    {
        int i = 0;
        for (int y = 0; y < map.Length; y++)
        {
            foreach (int x in map[y])
            {
                GameObject go = new GameObject(name+"_"+i);
                go.transform.SetParent(mapTransform);
                go.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("map/"+name+"/"+name+"_"+i);
                go.AddComponent<PolygonCollider2D>();
                go.AddComponent<DestructibleSprite>();
                go.tag = "ground";
                go.transform.localPosition = new Vector3(5*(x - 1), -y*5, 0);
                go.transform.localScale = new Vector3(5, 5, 1);
                i++;
            }
        }
    }
    
}
