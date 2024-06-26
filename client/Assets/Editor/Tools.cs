using UnityEngine;
using UnityEditor;

public class Tools
{
    [MenuItem("Tools/AtlasToSprite")]
    public static void AtlasToSprite()
    {
        string out_path = "E:/OutSprite/";//图集最后输出路径
        string resourcesPath = "Assets/Resources/";//因为使用的是Resources.LoadAll加载只能放这个目录下
        Object[] selects = Selection.objects;
        if (selects.Length < 1)
        {
            Debug.LogError("未选中文件");
            return;
        }
        for (int index = 0; index < selects.Length; index++)
        {
            string select_path = AssetDatabase.GetAssetPath(selects[index]);
            if (!select_path.StartsWith(resourcesPath))
            {
                Debug.LogWarning("未在Assets.Resources目录下 path:" + select_path);
                continue;
            }

            string select_ext = System.IO.Path.GetExtension(select_path);
            if (!select_ext.Equals(".png"))
            {
                Debug.LogWarning("选中文件非png path:" + select_path);//默认就png吧  看需求改咯
                continue;
            }

            string load_path = select_path.Remove(select_path.Length - select_ext.Length);
            load_path = load_path.Substring(resourcesPath.Length);

            Sprite[] sprites = Resources.LoadAll<Sprite>(load_path);//加载到图集下所有sprite
            if (sprites.Length < 1)
            {
                Debug.Log("非sprite path:" + select_path);
                continue;
            }

            string outPath = out_path + load_path;
            System.IO.Directory.CreateDirectory(outPath);//创建目录

            foreach (Sprite sprite in sprites)
            {
                Texture2D tex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, sprite.texture.format, false);
                tex.SetPixels(sprite.texture.GetPixels((int)sprite.rect.xMin, (int)sprite.rect.yMin, (int)sprite.rect.width, (int)sprite.rect.height));
                tex.Apply();
                System.IO.File.WriteAllBytes(outPath + "/" + sprite.name + ".png", tex.EncodeToPNG());//导出png
            }
        }
    }
}