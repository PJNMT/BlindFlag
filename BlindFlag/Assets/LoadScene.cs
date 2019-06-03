using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public enum Scene
    {
        START = 0, Navigation, Port, ShipShop, Taverne, ChasseAuTresor, SeaBattle, Combat, END, ENDCOMBAT
    }

    private static string ToString(Scene s)
    {
        switch (s)
        {
            case Scene.Navigation:
                return "navi";
            case Scene.SeaBattle:
                return "seabattle";
            case Scene.Port:
                return "Port";
            case Scene.Taverne:
                return "taverne";
            case Scene.Combat:
                return "combat";
            case Scene.ShipShop:
                return "ShipShop";
            case Scene.ChasseAuTresor:
                return "chasseautrésor";
            case Scene.END:
                return "capclairvoyant";
            case Scene.ENDCOMBAT:
                return "claircbt";
        }
        
        return "Menu_Start";
    }
    public static void Load(Scene new_scene, Scene old_scene)
    {
        foreach (var g in SceneManager.GetSceneByName(ToString(old_scene)).GetRootGameObjects())
        {
            DestroyImmediate(g);
        }
        
        var go = new GameObject("Sacrificial Lamb");
        DontDestroyOnLoad(go);

        foreach(var root in go.scene.GetRootGameObjects()) Destroy(root);
        
        BlindShip_Stat.SceneLoad = (int) new_scene;
        SceneManager.LoadScene((int) new_scene);
    }
}
