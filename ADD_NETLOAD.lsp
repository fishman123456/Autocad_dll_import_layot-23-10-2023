(defun C:UD_NETLOAD (/ x1 x2 x3)
					;загружаем DLL 
  (vl-load-com)
  ;(vl-cmdf "_netload" "C:/Users/Fishman.DB.CORP/YandexDisk/Работа АСКО/AUTOCAD_Плагины/02-07-2021_/Layer_Add/bin/Debug/Layer_Add.dll") 
 (vl-cmdf "_netload" "C:/Users/Fishman.DB.CORP/Documents/GitHub/Autocad_dll_import_layot-23-10-2023/bin/Debug/Autocad_dll_import_layot-23-10-2023.dll")
  ;- лиспом dll загружаем
  (alert "DLL Загружен")
)