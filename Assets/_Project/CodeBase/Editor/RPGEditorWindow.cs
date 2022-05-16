using System.IO;
using System.Linq;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.StaticData.ItemsDataBase;
using _Project.CodeBase.StaticData.ItemsDataBase.Types;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Weapon = _Project.CodeBase.Logic.HeroWeapon.Weapon;

namespace _Project.CodeBase.Editor
{
    public class RPGEditorWindow : OdinMenuEditorWindow
    {
        private const string ItemsPath = "Assets/_Project/StaticData/Items";
        private const string ItemsDataBasePath = "Assets/_Project/StaticData/Items/ItemsDataBase.asset";

        [MenuItem("Tools/RPG Editor")]
        private static void Open()
        {
            var window = GetWindow<RPGEditorWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        }
        
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true);
            tree.DefaultMenuStyle.IconSize = 28.00f;
            tree.Config.DrawSearchToolbar = true;
            
            tree.AddAllAssetsAtPath("", ItemsPath, typeof(ItemData), true);
            tree.EnumerateTree().AddIcons<ItemData>(x => x.ItemUIData.Icon);

            return tree;
        }
        
        protected override void OnBeginDrawEditors()
        {
            var selected = MenuTree.Selection.FirstOrDefault();
            var toolbarHeight = MenuTree.Config.SearchToolbarHeight;

            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                if (selected != null) 
                    GUILayout.Label(selected.Name);

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create Item")))
                {
                    ScriptableObjectCreator.ShowDialog<ItemData>(ItemsPath, obj =>
                    {
                        ConfigureItem(obj);

                        TrySelectMenuItemWithObject(obj);
                    });
                }
                
                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Update Item Database"))) 
                    UpdateItemDatabase();
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }

        private static void ConfigureItem(ItemData obj)
        {
            if (obj.GetType() == typeof(Equippable))
                obj.ItemPayloadData.MaxInContainer = 1;
            
            if (obj.GetType() == typeof(Armor))
                obj.ItemPayloadData.ItemType = ItemType.Armor;
            else if (obj.GetType() == typeof(Food))
                obj.ItemPayloadData.ItemType = ItemType.Food;
            else if (obj.GetType() == typeof(Weapon))
                obj.ItemPayloadData.ItemType = ItemType.Weapon;
        }

        private static void UpdateItemDatabase()
        {
            var db = AssetDatabase.LoadAssetAtPath<ItemsDataBase>(ItemsDataBasePath);
            db.ItemsDatabase.Clear();

            var index = 0;
            foreach (var file in Directory.EnumerateFiles(ItemsPath, "*.asset", SearchOption.AllDirectories))
            {
                var item = AssetDatabase.LoadAssetAtPath<ItemData>(file);
                if (item == null) continue;
                item.ItemPayloadData.DbId = index;
                db.ItemsDatabase.Add(item);
                index++;
            }

            EditorUtility.SetDirty(db);
        }
    }
}