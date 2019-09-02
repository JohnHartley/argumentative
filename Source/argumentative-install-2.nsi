; argumentative-install.nsi
;
; Based on example one with .Net detection from http://nsis.sourceforge.net/Get_.NET_Version (Thanks Amir Szekely)
; This script is based on example1.nsi, but it remember the directory, 
; has uninstall support and (optionally) installs start menu shortcuts.
;

!define Product_Version "0.5.51"

SetCompressor /SOLID /FINAL lzma

;--------------------------------
;Include Modern UI
;
  !include "MUI.nsh"
;
;--------------------------------

!include WordFunc.nsh
!insertmacro VersionCompare
 
!include LogicLib.nsh


;--------------------------------
;Interface Settings

  !define MUI_ABORTWARNING
  ;!define MUI_HEADERIMAGE
  ;!define MUI_HEADERIMAGE_BITMAP "nsis.bmp" ; 8 bit colour bitmap


;--------------------------------
;Pages

  !insertmacro MUI_PAGE_LICENSE "gpl.txt"
  !insertmacro MUI_PAGE_COMPONENTS
  !insertmacro MUI_PAGE_DIRECTORY
  !insertmacro MUI_PAGE_INSTFILES
  
  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES
  
 
;LoadLanguageFile "${NSISDIR}\Contrib\Language files\English.nlf"
  !insertmacro MUI_LANGUAGE "English"
  
;Version Information

VIProductVersion "${Product_Version}.0"
VIAddVersionKey /LANG=${LANG_ENGLISH} "FileVersion" "${Product_Version}"
VIAddVersionKey /LANG=${LANG_ENGLISH} "ProductName" "Argumentative"
VIAddVersionKey /LANG=${LANG_ENGLISH} "Comments" "Software to create argument maps"
VIAddVersionKey /LANG=${LANG_ENGLISH} "CompanyName" "Open Source"
;VIAddVersionKey /LANG=${LANG_ENGLISH} "LegalTrademarks" "Test Application is a trademark of Fake company"
VIAddVersionKey /LANG=${LANG_ENGLISH} "LegalCopyright" "� John Hartley, GPL Licence"
VIAddVersionKey /LANG=${LANG_ENGLISH} "FileDescription" "Argumentative installation files .Net 2.0"

BrandingText "Argumentative v${Product_Version}"

Function .onInit
  Call GetDotNETVersion
  Pop $0
  ${If} $0 == "not found"
    MessageBox MB_OK|MB_ICONSTOP ".NET runtime library is not installed.  Please download and install .Net 2.0 from Microsoft"
    Abort
  ${EndIf}
 
  StrCpy $0 $0 "" 1 # skip "v"
 
  ${VersionCompare} $0 "2.0" $1
  ${If} $1 == 2
    MessageBox MB_OK|MB_ICONSTOP ".NET runtime library v2.0 or newer is required. You have $0.  Please download and install .Net 2.0 from Microsoft"
    Abort
  ${EndIf}
  
FunctionEnd
 
Function GetDotNETVersion
  Push $0
  Push $1
 
  System::Call "mscoree::GetCORVersion(w .r0, i ${NSIS_MAX_STRLEN}, *i) i .r1 ?u"
  StrCmp $1 "error" 0 +2
    StrCpy $0 "not found"
 
  Pop $1
  Exch $0
FunctionEnd
;--------------------------------

; The name of the installer
Name "Argumentative"

; The file to write
OutFile "argumentative${Product_Version}.exe"

Icon "App.ico"


; The default installation directory
InstallDir $PROGRAMFILES\Argumentative

; Registry key to check for directory (so if you install again, it will 
; overwrite the old one automatically)
InstallDirRegKey HKLM "Software\Argumentative" "Install_Dir"


;--------------------------------

; The stuff to install
Section "Argumentative (required)" SectionRequired


  ; Section is Read Only
  SectionIn RO
  
  ; Correct .Net version found to get this far
  DetailPrint ".Net 2.0 or greater Runtime Library found."
  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; Put file there
  File "Argumentative.exe"
  File "Argumentative.chm"
  File "..\..\readme.txt"
  File "*.dll"
  File "..\..\Examples\RTF with picture.xslt"
  
  ; Write the installation path into the registry
  WriteRegStr HKLM SOFTWARE\Argumentative "Install_Dir" "$INSTDIR"
  
  ; Write the uninstall keys for Windows
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Argumentative" "DisplayName" "Argumentative"
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Argumentative" "UninstallString" '"$INSTDIR\uninstall.exe"'
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Argumentative" "NoModify" 1
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Argumentative" "NoRepair" 1
  WriteUninstaller "uninstall.exe"
  
SectionEnd

SectionGroup "Documentation" SectionDocumentation
Section "Example Files" SectionExampleFiles
  !define SectionSetText "Example files"
  SetOutPath $INSTDIR\Examples
  ; Include the samples without the CVS folder
  File /r /x CVS ..\..\Examples\*.* 
  ; Set directory back so installer lands in the right spot
  SetOutPath $INSTDIR
SectionEnd

Section "Manual" SectionManual
  SetOutPath $INSTDIR
  File *.pdf
SectionEnd

SectionGroupEnd

SectionGroup "Dictionaries" SectionDictionaries
Section "Australian Dictionary" SectionAusDic
  SetOutPath $INSTDIR
  File "en-AU.dic"
SectionEnd

Section "Spanish Dictionary" SectionSpanishDic
  SetOutPath $INSTDIR
  File "es-ES.dic"
SectionEnd

; Should check for locality
Section "US Dictionary" SectionUSdic
  SetOutPath $INSTDIR
  File "en-US.dic"
SectionEnd
SectionGroupEnd


; Optional section (can be disabled by the user)
Section "Start Menu Shortcuts" SectionShortcuts

  CreateDirectory "$SMPROGRAMS\Argumentative"
  CreateShortCut "$SMPROGRAMS\Argumentative\Uninstall.lnk" "$INSTDIR\uninstall.exe" "" "$INSTDIR\uninstall.exe" 0
  CreateShortCut "$SMPROGRAMS\Argumentative\Argumentative.lnk" "$INSTDIR\Argumentative.exe" "" "$INSTDIR\Argumentative.exe" 0
  CreateShortCut "$SMPROGRAMS\Argumentative\Readme.lnk" "$INSTDIR\Readme.txt"
  CreateShortCut "$SMPROGRAMS\Argumentative\Manual.lnk" "$INSTDIR\Argumentative Manual 0.5.pdf"
  
SectionEnd

Section "Associate .AXL" SectionAssociateAXL
  SectionIn 1 2
  !define Index "Line${__LINE__}"
  ReadRegStr $1 HKCR ".axl" ""
  StrCmp $1 "" "${Index}-NoBackup"
    StrCmp $1 "ArgumentativeFile" "${Index}-NoBackup"
    WriteRegStr HKCR ".axl" "backup_val" $1
"${Index}-NoBackup:"
  WriteRegStr HKCR ".axl" "" "ArgumentativeFile"
  ReadRegStr $0 HKCR "ArgumentativeFile" ""
  StrCmp $0 "" 0 "${Index}-Skip"
	WriteRegStr HKCR "ArgumentativeFile" "" "Argumentative File"
	WriteRegStr HKCR "ArgumentativeFile\shell" "" "open"
	WriteRegStr HKCR "ArgumentativeFile\DefaultIcon" "" "$INSTDIR\Argumentative.exe,0"
"${Index}-Skip:"
  WriteRegStr HKCR "ArgumentativeFile\shell\open\command" "" \
    '$INSTDIR\Argumentative.exe "%1"'
  WriteRegStr HKCR "ArgumentativeFile\shell\edit" "" "Edit Argumentative File"
  WriteRegStr HKCR "ArgumentativeFile\shell\edit\command" "" \
    '$INSTDIR\Argumentative.exe "%1"'
 
  System::Call 'Shell32::SHChangeNotify(i 0x8000000, i 0, i 0, i 0)'
!undef Index
  ; Rest of script
SectionEnd

Section "Associate .RTNL"  SectionAssociateRTNL
  SectionIn 1 2
  !define Index "Line${__LINE__}"
  ReadRegStr $1 HKCR ".rtnl" ""
  StrCmp $1 "" "${Index}-NoBackup"
    StrCmp $1 "ArgumentativeFile" "${Index}-NoBackup"
    WriteRegStr HKCR ".rtnl" "backup_val" $1
"${Index}-NoBackup:"
  WriteRegStr HKCR ".rtnl" "" "ArgumentativeFile"
  ReadRegStr $0 HKCR "ArgumentativeFile" ""
  StrCmp $0 "" 0 "${Index}-Skip"
	WriteRegStr HKCR "ArgumentativeFile" "" "Argumentative File"
	WriteRegStr HKCR "ArgumentativeFile\shell" "" "open"
	WriteRegStr HKCR "ArgumentativeFile\DefaultIcon" "" "$INSTDIR\Argumentative.exe,0"
"${Index}-Skip:"
  WriteRegStr HKCR "ArgumentativeFile\shell\open\command" "" \
    '$INSTDIR\Argumentative.exe "%1"'
  WriteRegStr HKCR "ArgumentativeFile\shell\edit" "" "Edit Argumentative File"
  WriteRegStr HKCR "ArgumentativeFile\shell\edit\command" "" \
    '$INSTDIR\Argumentative.exe "%1"'
 
  System::Call 'Shell32::SHChangeNotify(i 0x8000000, i 0, i 0, i 0)'
!undef Index
  ; Rest of script
SectionEnd

Section /o "Source Code" SectionSource
	SetOutPath $INSTDIR\Source
	File ..\..\*.cs
	File ..\..\*.resx
	File ..\..\*.ico
	File ..\..\*.csproj
	File "*.nsi"
	SetOutPath $INSTDIR\Source\tests
	File ..\..\tests\*.cs
	SetOutPath $INSTDIR
SectionEnd


LangString DESC_SectionRequired ${LANG_ENGLISH} "Core components of Argumentative."
LangString DESC_SectionDocumentation ${LANG_ENGLISH} "Documentation."
LangString DESC_SectionExampleFiles ${LANG_ENGLISH} "Sample Argument files and translations."
LangString DESC_SectionManual  ${LANG_ENGLISH} "The Argumentative Manual in a PDF file."
LangString DESC_SectionDictionaries  ${LANG_ENGLISH} "Dictionary files for spell checking."
LangString DESC_SectionShortcuts  ${LANG_ENGLISH} "Create icon in the programs area."

LangString DESC_SectionAssociateAXL  ${LANG_ENGLISH} "Associate .axl file extensions with Argumentative."
LangString DESC_SectionAssociateRTNL  ${LANG_ENGLISH} "Associate .rtnl (Rationale (tm)) file extensions with Argumentative."
LangString DESC_SectionSource  ${LANG_ENGLISH} "Install the C# source code. Download the developer's guide for more details."
; SectionManual SectionDictionaries

!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
  !insertmacro MUI_DESCRIPTION_TEXT ${SectionRequired} $(DESC_SectionRequired)
  !insertmacro MUI_DESCRIPTION_TEXT ${SectionDocumentation} $(DESC_SectionDocumentation)
  !insertmacro MUI_DESCRIPTION_TEXT ${SectionExampleFiles} $(DESC_SectionExampleFiles)
  !insertmacro MUI_DESCRIPTION_TEXT ${SectionManual} $(DESC_SectionManual)
  !insertmacro MUI_DESCRIPTION_TEXT ${SectionDictionaries} $(DESC_SectionDictionaries)
  !insertmacro MUI_DESCRIPTION_TEXT ${SectionShortcuts} $(DESC_SectionShortcuts)
  !insertmacro MUI_DESCRIPTION_TEXT ${SectionSource} $(DESC_SectionSource)
  !insertmacro MUI_DESCRIPTION_TEXT ${SectionAssociateAXL} $(DESC_SectionAssociateAXL)
  !insertmacro MUI_DESCRIPTION_TEXT ${SectionAssociateRTNL} $(DESC_SectionAssociateRTNL)
!insertmacro MUI_FUNCTION_DESCRIPTION_END



;--------------------------------

; Uninstaller

UninstallIcon "App.ico"

Section "Uninstall"
  
  ; Remove registry keys
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\Argumentative"
  DeleteRegKey HKLM SOFTWARE\Argumentative

  ; Remove files and uninstaller
  Delete $INSTDIR\Argumentative.exe
  Delete $INSTDIR\Argumentative.chm
  Delete $INSTDIR\*.bmp
  Delete $INSTDIR\*.dll
  Delete $INSTDIR\*.dic
  Delete $INSTDIR\*.pdf
  Delete $INSTDIR\uninstall.exe

  ; Remove shortcuts, if any
  Delete "$SMPROGRAMS\Argumentative\*.*"

  ; Remove directories used
  RMDir "$SMPROGRAMS\Argumentative"
  RMDir "$INSTDIR\Examples"
  RMDir "$INSTDIR\Source"
  RMDir "$INSTDIR"

SectionEnd
