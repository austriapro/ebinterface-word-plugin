set /p ChangeSet= Bitte Changeset Nr. eingeben...
cd CS%ChangeSet%
md ..\InstallerTest%ChangeSet%
del ..\InstallerTest%ChangeSet%\jbarchive.7z
del ..\InstallerTest%ChangeSet%\InstallCS%ChangeSet%.exe
"C:\Program Files\7-Zip\7z.exe" a ..\InstallerTest%ChangeSet%\jbarchive.7z * -m0=BCJ2 -m1=LZMA:d25:fb255 -m2=LZMA:d19 -m3=LZMA:d19 -mb0:1 -mb0s1:2 -mb0s2:3 -mx
copy /b "C:\Program Files\7-Zip\7z.sfx" + ..\InstallerTest%ChangeSet%\jbarchive.7z ..\InstallerTest%ChangeSet%\eRechnung-CS%ChangeSet%.exe
copy ..\InstallerTest%ChangeSet%\eRechnung-CS%ChangeSet%.exe Q:\ebInterface-codeplex\ebInterface4p1_Test
