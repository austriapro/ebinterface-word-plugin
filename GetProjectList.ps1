Add-Type -Path (${env:ProgramFiles(x86)} + '\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\amd64\Microsoft.Build.dll')

$slnDir = "C:\GitHub\ebinterface-word-plugin\eRechnungWordPlugIn.sln"
$slnPath = $slnDir
$slnFile = [Microsoft.Build.Construction.SolutionFile]::Parse($slnPath)
$pjcts = $slnFile.ProjectsInOrder

foreach ($item in $pjcts)
{

    if($item.ProjectType -eq 'KnownToBeMSBuildFormat')
    {
		$project = $item.AbsolutePath;
		[xml]$proj=Get-Content($project);
        Write-Host "Project  :" $item.ProjectName, $item.AbsolutePath,  $proj.Project.PropertyGroup[0].TargetFrameworkVersion, $item.ProjectConfigurations
		# RegEx \[assembly: Assembly(|File)Version\(\"(.*)\)\]
		# [assembly: Assembly$1Version("5.0.4.4")]
		# read from $item.AbsolutePath -> asmString
		# asmString -replace "^\[assembly: Assembly(|File)Version\(\"(.*)\)\]",'[assembly: Assembly$1Version("5.0.4.4")]'
		

    #    'SolutionFolder'{Write-Host Solution Folder : $item.ProjectName}
    }
}