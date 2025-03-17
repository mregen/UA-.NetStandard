<#
 .SYNOPSIS
    Sets CI version build variables and/or returns version information.

 .DESCRIPTION
    The script is a wrapper around any versioning tool we use and abstracts it from
    the rest of the build system.

 .PARAMETER BuildTemplate
    The build template which is calling the script.

#>

param(
    [string] $BuildTemplate = ""
)

$version = & (Join-Path $PSScriptRoot "get-version.ps1")

if ($BuildTemplate -eq "official")
{
   Write-Host "Detected official build. The preview tag is removed from the packaged version."
   $prereleaseversion = ''
}
else
{
   $prereleaseversion = $version.Pre
}

Write-Host "Version: $($version.Full)$($prereleaseversion)"

# Call versioning for build
if ($version.Public -eq 'True')
{
   & ./tools/nbgv  @("cloud", "-c", "-a", "-v", "$($version.Full)$($prereleaseversion)")
}
else
{
   & ./tools/nbgv  @("cloud", "-c", "-a")
}

if ($LastExitCode -ne 0) {
   Write-Warning "Error: 'nbgv cloud -c -a' failed with $($LastExitCode)."
}

if ($BuildTemplate -eq "official")
{
   Write-Host "Override NBGV_PrereleaseVersion to remove the preview tag"
   Write-Host "##vso[task.setvariable variable=NBGV_PrereleaseVersion]"
}

# Set build environment version numbers in pipeline context
Write-Host "Setting version build variables:"
Write-Host "##vso[task.setvariable variable=Version_Full;isOutput=true]$($version.Full)"
Write-Host "##vso[task.setvariable variable=Version_Prefix;isOutput=true]$($version.Prefix)"
