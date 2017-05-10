set /p ChangeSet= Bitte Changeset Nr. eingeben...
cd CS%ChangeSet%
md ..\Installer%ChangeSet%
del ..\Installer%ChangeSet%\jbarchive.7z
del ..\Installer%ChangeSet%\InstallCS%ChangeSet%.exe
"C:\Program Files\7-Zip\7z.exe" a ..\Installer%ChangeSet%\jbarchive.7z * -m0=BCJ2 -m1=LZMA:d25:fb255 -m2=LZMA:d19 -m3=LZMA:d19 -mb0:1 -mb0s1:2 -mb0s2:3 -mx
"C:\Program Files\7-Zip\7z.exe" a ..\Installer%ChangeSet%\jbarchive.7z ..\Handbuch\Ausfuellhilfe.pdf -m0=BCJ2 -m1=LZMA:d25:fb255 -m2=LZMA:d19 -m3=LZMA:d19 -mb0:1 -mb0s1:2 -mb0s2:3 -mx
copy /b "C:\Program Files\7-Zip\7z.sfx" + ..\Installer%ChangeSet%\jbarchive.7z ..\Installer%ChangeSet%\eRechnung-CS%ChangeSet%.exe
md Q:\ebInterface-codeplex\ebInterface4p2_CS%ChangeSet%
copy ..\Installer%ChangeSet%\eRechnung-CS%ChangeSet%.exe Q:\ebInterface-codeplex\ebInterface4p2_CS%ChangeSet%
cd ..
