<# -----------------------------------------------------------------------------------------------------
SYNOPSIS
Powershell Script für Visual Studion 2015 zur Erstellung der Auslieferungsobjekte

AUFRUF
.\ReleaseBuild.ps1 -Configuration Debug|Release -UpdateVersionNumber Y|N -Compile Y|N

----------------------------------------------------------------------------------------------------- #>
param(
	[string]$Configuration="Debug",
	[string]$UpdateVersionNumber ="N",
	[string]$Compile="N",
	[string]$UpdateVstoVersion = "N"
)

function doBuild([string]$targetDir, [string]$project, [string]$config, [string]$target,[string]$OutputPath){
	$dir = $SolutionDir+$targetDir;
	Push-Location -Path $SolutionDir
	[string]$logFile =  $BuildLogDir +$targetDir+".log ";
	[string]$msbuild = "msbuild /flp1:logfile="+$logFile;
	[string]$cmd =$msbuild+" "+$target+' /p:SolutionDir="'+$SolutionDir+'"`;Configuration="'+$config+'" '+$project

	Invoke-Expression $cmd -Verbose
	Write-Host "-----------------------------------------------------------------------------------"
	Write-Host $cmd;
	Write-Host "-----------------------------------------------------------------------------------"
	$lines = Get-Content -Tail 5 $logFile
	Write-Host $lines
	Pop-Location
}

function cleanSolution([string]$config)
{
Push-Location -Path $SolutionDir;

[string]$cmd = 'msbuild eRechnungWordPlugIn.sln /t:Clean /p:Configuration="'+$config+'"';  #+"`;Platform=AnyCPU";

Write-Host $cmd;
Invoke-Expression $cmd -Verbose
Pop-Location
}

function Format-XML {Param ([Xml]$InputObject,[string]$FromText="N") 

  $doc=New-Object system.xml.xmlDataDocument 
	if($FromText -eq "Y")
	{
		$doc.LoadXml($InputObject.OuterXml)
 
	} 
	else 
	{
	   $doc = $InputObject 
	}
  $sw=New-Object system.io.stringwriter 
  $writer=New-Object system.xml.xmltextwriter($sw) 
  $writer.Formatting = [System.xml.formatting]::Indented 
  $doc.WriteContentTo($writer) 
  Write-Host $sw.ToString() 
}


function UpdateVstoProject([string]$project,[string]$publishDir){
	[xml]$proj=Get-Content($project);
	Write-Host "Before Update"
	Write-Host $proj.Project.PropertyGroup[0]
	[string]$tosplit = [string]$proj.Project.PropertyGroup[0].ApplicationVersion;
	$vers = $tosplit -split "\.";

	[xml]$xmlVersion = Get-Content($fnVersion);
	Format-Xml $xmlVersion
	$vers[0] =$xmlVersion.Version.Actual.Release
	$vers[1] =$xmlVersion.Version.Actual.Major
	$vers[2] =$xmlVersion.Version.Actual.Minor
	$vers[3] =$xmlVersion.Version.Actual.Update

	[string]$svers = $vers -join ".";
	Write-Host $svers;
	$proj.Project.PropertyGroup[0].ApplicationVersion = $svers;	
	$proj.Project.PropertyGroup[0].AssemblyVersion = $svers;
	[string]$versionDir =("{0}p{1}p{2}p{3}\" -f $xmlVersion.Version.Actual.Release,$xmlVersion.Version.Actual.Major,$xmlVersion.Version.Actual.Minor,$xmlVersion.Version.Actual.Update)
	[string]$pUrl = $publishDir + $versionDir
	
	$proj.Project.PropertyGroup[0].PublishUrl = $pUrl;
	$proj.Save($project);
	Write-Host "After Update"
	Write-Host $proj.Project.PropertyGroup[0]
	$fileVersion = [Version]$svers
	# C:\GitHub\ebinterface-word-plugin\eRechnungWordPlugIn\eRechnung\Properties\AssemblyInfo.cs
	$asmPath = (Split-Path -Path $project)+"\Properties\AssemblyInfo.cs" 
	Write-Host $asmPath
	UpdateAssembly -path $asmPath -fileVersion $fileVersion
	[string]$logAsmPath = $asmPath -replace "\\eRechnung\\", "\Logging\"
	UpdateAssembly -path $logAsmPath -fileVersion $fileVersion
	$pUrl
	return
}

function UpdateAssembly([string]$path, [Version]$fileVersion){
			
	$pattern = '\[assembly: AssemblyVersion\("(.*)"\)\]'
	(Get-Content $path) | ForEach-Object{
		if($_ -match $pattern){
			# We have found the matching line
			# Edit the version number and put back.
			#$fileVersion = [version]$matches[1]
			$newVersion = "{0}.{1}.{2}.{3}" -f $fileVersion.Major, $fileVersion.Minor, $fileVersion.Build, $fileVersion.Revision
			'[assembly: AssemblyVersion("{0}")]' -f $newVersion
		} else {
			# Output line as is
			$_
		}
	} | Set-Content $path
}

function UpdateVersion(){
param(
	[string]$build = "Debug",
	[string]$versionFile
	)
	
	#"fnVersion="+$fnVersion;
	[xml]$xmlVersion = Get-Content($versionFile);
	Format-Xml -InputObject $xmlVersion

	if($build.StartsWith("Release")) {
			[int]$iUpdate = [Convert]::ToInt32($xmlVersion.Version.Actual.Minor,10);
		if($updVers.StartsWith("y"))
		{
			$iUpdate += 1;
		}
		[int]$iPatch = [Convert]::ToInt32($xmlVersion.Version.Actual.Update,10);
		if($UpdateVstoVersion.ToLower().StartsWith("y")) 
		{
			$iPatch += 1;
		}
		$xmlVersion.Version.Actual.Minor = [string]$iUpdate;
		$xmlVersion.Version.Actual.Update = [string]$iPatch
		$xmlVersion.Save($fnVersion);
		}

Format-Xml -InputObject $xmlVersion

}

# -----------------------------------------------------------
# Check parameters
# -----------------------------------------------------------

# If not executed from Visual Studion Command Prompt, uncomment following lines
# =============================================================================
#pushd 'C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\Tools'    
#cmd /c "vsvars32.bat&set" |
#foreach {
#  if ($_ -match "=") {
#    $v = $_.split("="); set-item -force -path "ENV:\$($v[0])"  -value "$($v[1])"
#  }
#}
#popd
#write-host "`nVisual Studio 2015 Command Prompt variables set." -ForegroundColor Yellow

[string]$SolutionDir = "C:\GitHub\ebinterface-word-plugin\eRechnungWordPlugIn\"
[string]$updVers = $UpdateVersionNumber.ToLower();
if("y","n","yes","no" -notcontains $updVers)
{
	Throw "$($UpdateVersion) is not y or n"
}
[string]$BuildLogDir = $SolutionDir+"Buildlog\";
[string]$eRechnung = "eRechnung"
[string]$eRechnungProject = "eRechnung\eRechnung.csproj"
[string]$fnVersion = $SolutionDir +"Scripts\Version.xml"

if(!(Test-Path -Path $BuildLogDir)){
	New-Item -ItemType Directory -Path $BuildLogDir
}
[string]$cfg="$Configuration"+";Platform=Any CPU"
if($Compile -eq "Y")
{
  cleanSolution $cfg
 
}
# Update Version if UpdateVersionNumber=Y and Configuration=Release
UpdateVersion -build $Configuration -versionFile $fnVersion


[string]$proj = $SolutionDir + $eRechnungProject
# Put current Version Number in Project file
[string]$publishDir = "C:\trash\publish\Codeplex\"
[string]$publishUrl = UpdateVstoProject -project $proj -publishDir $publishDir
[string]$versionDir = Split-Path $publishUrl -Leaf

if($Compile -eq "Y")
{
	if($Configuration -eq "Release")
	{
		Read-Host -Prompt "Token für Signatur anstecken und dann enter drücken"
	}
  DoBuild -targetDir $eRechnung -project "eRechnungWordPlugIn.sln" -config $cfg -OutputPath $publishUrl -target "/t:Publish"
 
}
if(Test-Path $publishUrl) {
	[string]$delFiles= $publishUrl+"*.*"
	Remove-Item -force $delFiles
} else {
	md $publishUrl
}
[string]$buildDir = $SolutionDir+$eRechnung+"\bin\"+$Configuration+"\app.publish\*"
[string]$manual = $SolutionDir+"Handbuch\Anleitung.pdf"
[string]$jbArchive =$publishUrl+"jbarchive.7z"
[string]$7zip = '"C:\Program Files\7-Zip\7z.exe"'
[string]$arch1cmdParm= $7zip+ " a "+$jbArchive+" "+$buildDir+" "+$manual+ " -m0=BCJ2 -m1=LZMA:d25:fb255 -m2=LZMA:d19 -m3=LZMA:d19 -mb0:1 -mb0s1:2 -mb0s2:3 -mx"

Write-Host $arch1cmdParm
Invoke-Expression ('& '+$arch1cmdParm) -Verbose

[string]$installer       = $publishUrl+"`\eRechnungPlugIn-V"+$versionDir+".exe"
[string]$sfxBuilder      = "C:\Program Files (x86)\7z SFX Builder\3rdParty\Modules\7zsd_LZMA.sfx"
[string]$sfx      = "C:\Program Files\7-Zip\7z.sfx"
[string]$sfxConfig = $SolutionDir+"\Scripts\PluginCfg.txt"
[string]$copy ="copy /b "+ '"C:\Program Files\7-Zip\7z.sfx"' + $jbArchive+" "+ $publishUrl+ "\" + $versionDir+".exe"  #..\Installer%ChangeSet%\eRechnung-CS%ChangeSet%.exe

# Invoke-Expression ('& '+ $copy) -Verbose
Write-Host $sfxBuilder
Write-Host $sfxConfig
Write-Host $jbArchive
Get-Content $sfxBuilder,$sfxConfig,$jbArchive -Encoding Byte -Read 512  | sc  $installer -Encoding Byte
Remove-Item -force $jbArchive
Copy-Item $manual $publishUrl
[string]$publishOnQ = "Q:\ebInterface-codeplex\V"+$versionDir
if(Test-Path $publishOnQ){
		[string]$delFiles= $publishOnQ+"*.*"
	Remove-Item -force $delFiles
} else {
	md $publishOnQ
}

Copy-Item $installer $publishOnQ
Copy-Item $manual $publishOnQ
Write-Host "Fertig."
