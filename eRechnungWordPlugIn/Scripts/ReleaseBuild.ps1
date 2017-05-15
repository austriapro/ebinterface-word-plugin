<# -----------------------------------------------------------------------------------------------------
SYNOPSIS
Powershell Script für Visual Studion 2015 zur Erstellung der Auslieferungsobjekte

AUFRUF
.\ReleaseBuild.ps1 -Configuration Release -UpdateVersionNumber Y

----------------------------------------------------------------------------------------------------- #>
param(
    [string]$Configuration="Debug",
    [string]$UpdateVersionNumber ="N"
)

#function doBuild([string]$targetDir, [string]$config, [string]$target){
#    $dir = $SolutionDir+$targetDir;
#    Push-Location -Path $dir
#    [string]$logFile =  $BuildLogDir +$targetDir+".log ";
#    [string]$msbuild = "msbuild /flp1:logfile="+$logFile;
#    [string]$cmd =$msbuild+$target+' /p:SolutionDir="'+$SolutionDir+'"`;Configuration="'+$config+'"';

#    Write-Host $cmd;
#    Invoke-Expression $cmd -Verbose
#    $lines = Get-Content -Tail 5 $logFile
#    Write-Host $lines
#    Pop-Location
#}

function cleanSolution([string]$config)
{
Push-Location -Path $SolutionDir;

[string]$cmd = 'msbuild eRechnungWordPlugIn.sln /t:Clean /p:Configuration="'+$config+'"';  #+"`;Platform=AnyCPU";

Write-Host $cmd;
Invoke-Expression $cmd -Verbose
Pop-Location
}


function UpdateVstoProject([string]$project){
    [xml]$proj=Get-Content($project);
    $proj.Project.PropertyGroup[0] | fl
    [string]$tosplit = [string]$proj.Project.PropertyGroup.ApplicationVersion;
    $vers = $tosplit -split "\.";
    #$ivers = [Convert]::ToInt32($vers[3]) + 1;
    #$vers[3] = [string]$ivers;

    [xml]$xmlVersion = Get-Content($fnVersion);
    $xmlVersion | fl;
    $vers[0] =$xmlVersion.Version.Actual.Release
    $vers[1] =$xmlVersion.Version.Actual.Major
    $vers[1] =$xmlVersion.Version.Actual.Minor
    $vers[3] =$xmlVersion.Version.Actual.Update

    [string]$svers = $vers -join ".";
    $svers;
    $proj.Project.PropertyGroup[0].ApplicationVersion = $svers;
    $proj.Save($project);
}

function UpdateVersion(){
param(
    [string]$build = "Debug"
    )
    
    "fnVersion="+$fnVersion;
    [xml]$xmlVersion = Get-Content($fnVersion);
    $xmlVersion | fl;
    [int]$iUpdate = [Convert]::ToInt32($xmlVersion.Version.Actual.Minor,10);

    if($build.StartsWith("Release")) {
        $iUpdate += 1;
        $xmlVersion.Version.Actual.Minor = [string]$iUpdate;
        #$yy = "{0:D2}" -f (Get-Date -Format "yy");
        #$dd1 = (Get-Date).dayofyear # -uformat %j;
        #$ddd = $dd1.ToString("000");
        #$dyyy = $yy+$ddd; 
        $xmlVersion.Version.Actual.Update = "0"
        $xmlVersion.Save($fnVersion);
        #Write-Host $dyyy;
        }

Write-Output $xmlVersion.Version.Actual | fl
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
[string]$eRechnungProject = "eRechnung.csproj"
[string]$updVers = $UpdateVersionNumber.ToLower();
if("y","n","yes","no" -notcontains $updVers)
{
    Throw "$($UpdateVersion) is not y or n"
}
[string]$BuildLogDir = $SolutionDir+"Buildlog\";
[string]$eRechnung = "eRechnung"
[string]$fnVersion = $SolutionDir +"Scripts\Version.xml"

if(!(Test-Path -Path $BuildLogDir)){
    New-Item -ItemType Directory -Path $BuildLogDir
}
[string]$cfg="$Configuration"+";Platform=Any CPU"
cleanSolution $cfg
