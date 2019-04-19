<# -----------------------------------------------------------------------------------------------------
SYNOPSIS
Powershell Script für Visual Studion 2015 zur Erstellung der Auslieferungsobjekte

AUFRUF
.\ReleaseBuild.ps1 -Configuration Debug|Release -UpdateVersionNumber Y|N -Compile Y|N

----------------------------------------------------------------------------------------------------- #>
param(
	[string]$Configuration="Debug",
	[string]$Compile="Y"
)
$scriptPath  = Split-Path $MyInvocation.MyCommand.Path
$scriptPath
Import-Module "C:\GitHub\ebinterface-word-plugin\Scripts\GetCurrentVersion.ps1"
Get-Command -Module "GetCurrentVersion"

$gitVersion = GetCurrentVersion

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

	$proj.Project.PropertyGroup[0].ApplicationVersion = $gitVersion.Version.VersionFull;	
	$proj.Project.PropertyGroup[0].AssemblyVersion = $gitVersion.Version.VersionFull;	
	[string]$versionDir =$gitVersion.Version.VersionFull.Replace(".","p")
	[string]$pUrl = $publishDir + $versionDir
	
	if($Compile -eq "Y")
	{
  		$proj.Project.PropertyGroup[0].PublishUrl = $pUrl;
		$proj.Save($project);
		Write-Host "After Update"
		Write-Host $proj.Project.PropertyGroup[0]
		$fileVersion = [Version]$svers
		# C:\GitHub\ebinterface-word-plugin\eRechnungWordPlugIn\eRechnung\Properties\AssemblyInfo.cs

	}	
	$pUrl
	return
}

function UpdateAssembly(){
			
# Update all AssemblyInfo.cs
& gitversion -updateassemblyinfo -ensureassemblyinfo

}




[string]$SolutionDir = "C:\GitHub\ebinterface-word-plugin\"
[string]$BuildLogDir = $SolutionDir+"Buildlog\";
[string]$eRechnung = "eRechnung"
[string]$eRechnungProject = "eRechnungWordPlugIn\eRechnung\eRechnung.csproj"
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
# UpdateVersion -build $Configuration -versionFile $fnVersion


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
	UpdateAssembly 
	DoBuild -targetDir $eRechnung -project "eRechnungWordPlugIn.sln" -config $cfg -OutputPath $publishUrl -target "/t:Publish"
 
}
if(Test-Path $publishUrl) {
	[string]$delFiles= $publishUrl+"*.*"
	Remove-Item -force $delFiles
} else {
	md $publishUrl
}
[string]$buildDir = $SolutionDir+"\eRechnungWordPlugIn\"+$eRechnung+"\bin\"+$Configuration+"\app.publish\*"
[string]$manual = $SolutionDir+"eRechnungWordPlugin\Handbuch\Anleitung.pdf"
[string]$jbArchive = $publishUrl+"`\eRechnungPlugIn-V"+$versionDir+".zip"
[string]$7zip = '"C:\Program Files\7-Zip\7z.exe"'
[string]$arch1cmdParm= $7zip+ " a "+$jbArchive+" "+$buildDir+" "+$manual # + " -m0=BCJ2 -m1=LZMA:d25:fb255 -m2=LZMA:d19 -m3=LZMA:d19 -mb0:1 -mb0s1:2 -mb0s2:3 -mx"

Write-Host $arch1cmdParm
Invoke-Expression ('& '+$arch1cmdParm) -Verbose


# Invoke-Expression ('& '+ $copy) -Verbose
Write-Host $jbArchive
Copy-Item $manual $publishUrl
[string]$publishOnQ = "Q:\ebInterface-codeplex\V"+$versionDir
if(Test-Path $publishOnQ){
		[string]$delFiles= $publishOnQ+"*.*"
	Remove-Item -force $delFiles
} else {
	md $publishOnQ
}

Copy-Item $jbArchive $publishOnQ
Copy-Item $manual $publishOnQ
Write-Host "Fertig."
