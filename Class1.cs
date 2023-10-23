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
                    if (ltr.LayoutName != "Model")
                    {
                        layouts[++iLayout] = ltr.LayoutName;
                    }
                }
            }
            try
            {
                LayoutManager acLayoutManager = null;
                foreach (String layout in layouts)
                {
                    if (layout != null)
                    {
                        acLayoutManager = LayoutManager.Current;
                        acLayoutManager.CurrentLayout = layout;
                        //Проверка на корректность имен файла опущена 
                        //acDoc.Editor.WriteMessage("\nПоптыка сохранить лист: " + layout);
                        //acDoc.SendStringToExecute("._FILEDIA 0 ", true, false, true);
                        acDoc.SendStringToExecute("._EXPORTLAYOUT" + " E:  " + layout , true, false, true);
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           finally 
                {
                acDoc.SendStringToExecute("._FILEDIA 1 ", true, false, true);
            } 
          
        }
    }
}
