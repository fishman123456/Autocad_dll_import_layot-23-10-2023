using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Application = Autodesk.AutoCAD.ApplicationServices.Application;

namespace Autocad_dll_import_layot_23_10_2023
{
    public class Class1
    {
[CommandMethod("U_83")]
        //C# Разбивка чертежа на "Модель - Лист".
        //https://forum.dwg.ru/showthread.php?t=107322
        public static void ExportLayouts()
        {
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            Editor acEditor = acDoc.Editor;
            Database acCurDb = acDoc.Database;
            String[] layouts = null;

            using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
            {
                DBDictionary layoutDict = (DBDictionary)acTrans.GetObject(acCurDb.LayoutDictionaryId, OpenMode.ForRead);

                Int32 iLayout = 0;
                layouts = new string[layoutDict.Count];

                foreach (DictionaryEntry id in layoutDict)
                {
                    Layout ltr = (Layout)acTrans.GetObject((ObjectId)id.Value, OpenMode.ForRead);
                    if (ltr.LayoutName.ToLower() != "model")
                    {
                        layouts[++iLayout] = ltr.LayoutName;
                    }
                }
                

            LayoutManager acLayoutManager = null;
            foreach (String layout in layouts)
            {

                if (layout != null)
                {
                    acLayoutManager = LayoutManager.Current;
                    acLayoutManager.CurrentLayout = layout;
                   acDoc.Editor.WriteMessage($"\n Поптыка сохранить лист: {layout}");
                    //acDoc.SendStringToExecute("._FILEDIA 0 ", true, false, true);
                    //Проверка на корректность имен файла опущена 
                    acDoc.SendStringToExecute($"._EXPORTLAYOUT  {layout}" , false, false, false);
                    //acDoc.SendStringToExecute("\n",false, false, true);
                   //acDoc.SendStringToExecute("\n" + "(princ)",  false, false, false);
                    //acEditor.Command("\n" + "._EXPORTLAYOUT" + " E: " + layout);
                }
            }
                acTrans.Commit();
            }
        }
        [CommandMethod("CLA")]

        public static void CommandLineArguments()

        {

            var ed = Application.DocumentManager.MdiActiveDocument.Editor;

            var args = System.Environment.GetCommandLineArgs();

            ed.WriteMessage(

              //"\nCommand-line arguments at AutoCAD launch were ",
              ".ЭКСПОРТВЭЛИСТА "

            );

            foreach (var arg in args)

            {

                ed.WriteMessage("[{0}] ", arg);

            }

        }
    }
}
