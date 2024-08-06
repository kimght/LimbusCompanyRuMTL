param(
    [string]$version
)

# Set build version
$content = Get-Content -Path 'src/LCB_BuildInfo.cs' -Raw
$updatedContent = $content -replace '\{\{VERSION\}\}', $version
Set-Content -Path 'src/LCB_BuildInfo.cs' -Value $updatedContent

dotnet build ./src/LimbusCompanyBusRus.sln -c ML_Cpp_net6_interop /p:Version=$version
