using System.IO;
using System.Linq;
using _Project.CodeBase.Logic.Inventory;
using _Project.CodeBase.StaticData;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace _Project.CodeBase.Editor
{
    public class RPGEditorWindow : OdinMenuEditorWindow
    {
        private const string ItemsPath = "Assets/_Project/StaticData/Items";
        private const string ItemsInfoPath = "Assets/_Project/StaticData/Items/ItemsInfo.asset";
        
        private ItemsInfo _itemsInfo;

        [MenuItem("Tools/RPG Editor")]
        private static void Open()
        {
            var window = GetWindow<RPGEditorWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(900, 600);
        }
        
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true);
            tree.DefaultMenuStyle.IconSize = 28.00f;
            tree.Config.DrawSearchToolbar = true;
            
            tree.AddAllAssetsAtPath("", ItemsPath, typeof(ItemInfo), true);
            tree.EnumerateTree().AddIcons<ItemInfo>(x => x.UIInfo.Icon);

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
                    ScriptableObjectCreator.ShowDialog<ItemInfo>(ItemsPath, obj =>
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

        private void ConfigureItem(ItemInfo obj)
        {
            if (obj.GetType() == typeof(EquippableInfo))
                obj.PayloadInfo.SetMaxInContainer(1);

            var type = GetItemType(obj);
            if (type != ItemType.None)
                obj.PayloadInfo.SetItemType(type);
        }

        private ItemType GetItemType(ItemInfo obj)
        {
            var type = ItemType.None;
            if (obj.GetType() == typeof(ArmorInfo))
                type = ItemType.Armor;
            else if (obj.GetType() == typeof(FoodInfo))
                type = ItemType.Food;
            else if (obj.GetType() == typeof(GunInfo))
                type = ItemType.Weapon;
            else if (obj.GetType() == typeof(AttachmentInfo))
                type = ItemType.Attachment;
            return type;
        }

        private void UpdateItemDatabase()
        {
            _itemsInfo = AssetDatabase.LoadAssetAtPath<ItemsInfo>(ItemsInfoPath);
            _itemsInfo.ItemsDatabase.Clear();

            var foundFiles = Directory.EnumerateFiles(ItemsPath, "*.asset", SearchOption.AllDirectories).ToList();

            var foundItems = foundFiles.Select(AssetDatabase.LoadAssetAtPath<ItemInfo>)
                .Where(asset => asset != null)
                .OrderBy(x => x.ID).ToList();
            
            var inRange = foundItems.Where(x => x.ID != Inventory.ErrorIndex && x.ID < foundItems.Count)
                .OrderBy(x => x.ID).ToList();
            
            var notInRange = foundItems.Where(x => x.ID != Inventory.ErrorIndex && x.ID >= foundItems.Count)
                .OrderBy(x => x.ID).ToList();
            
            var noID = foundItems.Where(x => x.ID <= Inventory.ErrorIndex)
                .OrderBy(x => x.ID).ToList();

            var index = 0;
            for (var i = 0; i < foundItems.Count; i++)
            {
                var itemToAdd = inRange.Find(x => x.ID == i);
                if (itemToAdd != null)
                    AddItemToDatabase(itemToAdd);
                
                else if (index < noID.Count)
                {
                    noID[index].PayloadInfo.SetID(i);
                    AddItemToDatabase(noID[index]);
                    index++;
                }
            }

            foreach (var item in notInRange) 
                AddItemToDatabase(item);

            SaveDatabase();
        }

        private void AddItemToDatabase(ItemInfo itemToAdd) => 
            _itemsInfo.ItemsDatabase.Add(itemToAdd);

        private void SaveDatabase()
        {
            foreach (var item in _itemsInfo.ItemsDatabase)
                SetDirty(item);
            SaveAsset(_itemsInfo);
        }

        private void SaveAsset(Object obj)
        {
            SetDirty(obj);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void SetDirty(Object obj) => 
            EditorUtility.SetDirty(obj);
    }
}