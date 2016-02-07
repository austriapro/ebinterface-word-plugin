param ( 
    [string]$cfgName = "undefined",
    [string]$ProductFile = "C:\GitHub\ebinterface-word-plugin\eRechnungWordPlugIn\ebIModels\XmlData\ProductInfo.xml",
    [string]$VersionFile = "C:\GitHub\ebinterface-word-plugin\eRechnungWordPlugIn\Scripts\Version.xml"
)

$gitBranch = (git branch) | Out-String;
$parts = $gitBranch -split '\s+';
 [string]$gitId = (git rev-parse --short HEAD) | Out-String;
 $gitId;
 #$gitDate = (git log -1 HEAD --format=%cd) | Out-String;
 #'"'+$gitDate.Trim()+'"';
# $gitd = [datetime]::parseexact($gitDate.Trim(),"ddd MMM dd HH:mm:ss yyyy zzzz",[System.Globalization.CultureInfo]::InvariantCulture)
[datetime]$gitd = Get-Date
 $gitd;

[xml]$xmlVersion = Get-Content($VersionFile);

$xmlString = Get-Content($ProductFile);
$xmlString;
$xml1 = New-Object XML;
$xml1.LoadXml($xmlString);
$xml1.ProductInfo.Branch = $parts[1];
$xml1.ProductInfo.ChangeSetId = $gitId.Trim();
$xml1.ProductInfo.CompileInfo = $cfgName;
$xml1.ProductInfo.LastCommit = $gitd.tostring("yyyy-MM-dd HH:mm:ss");
[string]$cmpTime = Get-Date -Format "yyyy-MM-dd HH:mm:ss";
$xml1.ProductInfo.CompileTime= $cmpTime;
$xml1.ProductInfo.Version = $xmlVersion.Version.Actual.Release + "." + $xmlVersion.Version.Actual.Major +"." + $xmlVersion.Version.Actual.Minor +"."+ $xmlVersion.Version.Actual.Update;
$xml1.Save($ProductFile);
# $xml1.Save("C:\Trash\prod.xml");