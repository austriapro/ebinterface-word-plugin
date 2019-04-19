function GetCurrentVersion 
{
$git = gitversion;
$gitVersion = $git | ConvertFrom-Json;
# $gitVersion | fl
[string]$build = (Get-Date).ToString("yy")+(Get-Date).DayOfYear.ToString("D3")
[string]$vFull = $gitVersion.MajorMinorPatch+"."+ $gitVersion.CommitsSinceVersionSource;

$lastCommitDate = GetGitProperty("Date");
 [string]$lastCommitDate = GetGitProperty("Date");
 [datetime]$gitd = [datetime]::Parse($lastCommitDate);


$gitVersion|Add-Member -MemberType NoteProperty -Name "LastCommitTimeStamp" -Value $gitd

[PSObject]$versionObject = New-Object psobject
$versionObject | Add-Member -MemberType NoteProperty -Name "Major" -Value $gitVersion.Major; 
$versionObject | Add-Member -MemberType NoteProperty -Name "Minor" -Value $gitVersion.Minor;
$versionObject | Add-Member -MemberType NoteProperty -Name "Patch" -Value $gitVersion.Patch;
$versionObject | Add-Member -MemberType NoteProperty -Name "Build" -Value $gitVersion.CommitsSinceVersionSource;
$versionObject | Add-Member -MemberType NoteProperty -Name "VersionFull" -Value $vFull;
                   

$obj = [pscustomobject]@{
'Version' = $versionObject
'GitVersion' = $gitVersion
}
#$obj | Add-Member -TypeName NoteProperty -Name "Version" -Value $versionObject;
#$obj | Add-Member -TypeName NoteProperty -Name "GitVersion" -Value $gitVersion;

   
return $obj

}

function GetGitProperty([string]$Property){
    [string]$git = "C:\Program Files\Git\bin\git.exe";
    
    if($Property -eq "Date"){
        $gitCmd = @("log","-1","--format=%cd","--date=format:%Y/%m/%d %H:%M:%S");
    }
    if($Property -eq "Branch"){
        $gitCmd = @("symbolic-ref","--short","HEAD");
    }
    if($Property -eq "RevisionId"){
        $gitCmd = @("rev-parse","--short","HEAD");
    }
	$result = & $git $gitCmd | Out-String
    return $result;
}

GetCurrentVersion