using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xh.FastTrading.Wpf.Common.Untils
{
   public class INI
    {
        INI opi = new INI();
        public INI() 
        {

        }
        private void SaveDataInfo() 
        {

            //opi.INIWriteValue(file, "Desktop", "Color", "Red");
            //opi.INIWriteValue(file, "Desktop", "Width", "3270");

            //opi.INIWriteValue(file, "Toolbar", "Items", "Save,Delete,Open");
            //opi.INIWriteValue(file, "Toolbar", "Dock", "True");

            ////写入一批键值
            //opi.INIWriteItems(file, "Menu", "File=文件\0View=视图\0Edit=编辑");

            ////获取文件中所有的节点
            //string[] sections = opi.INIGetAllSectionNames(file);

            ////获取指定节点中的所有项
            //string[] items = opi.INIGetAllItems(file, "Menu");

            ////获取指定节点中所有的键
            //string[] keys = opi.INIGetAllItemKeys(file, "Menu");

            ////获取指定KEY的值
            //string value = opi.INIGetStringValue(file, "Desktop", "color", null);

        }



        private void Del() 
        {

            //opi.INIWriteValue(file, "Desktop", "Color", "Red");
            //opi.INIWriteValue(file, "Desktop", "Width", "3270");

            //opi.INIWriteValue(file, "Toolbar", "Items", "Save,Delete,Open");
            //opi.INIWriteValue(file, "Toolbar", "Dock", "True");

            ////写入一批键值
            //opi.INIWriteItems(file, "Menu", "File=文件\0View=视图\0Edit=编辑");

            ////获取文件中所有的节点
            //string[] sections = opi.INIGetAllSectionNames(file);

            ////获取指定节点中的所有项
            //string[] items = opi.INIGetAllItems(file, "Menu");

            ////获取指定节点中所有的键
            //string[] keys = opi.INIGetAllItemKeys(file, "Menu");

            ////获取指定KEY的值
            //string value = opi.INIGetStringValue(file, "Desktop", "color", null);

        }

        private void DelKey() 
        {
            ////删除指定的KEY
            //opi.INIDeleteKey(file, "desktop", "color");

            ////删除指定的节点
            //opi.INIDeleteSection(file, "desktop");

            ////清空指定的节点
            //opi.INIEmptySection(file, "toolbar");
        }
    }
}
