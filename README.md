## ne14.portal.api

``` powershell
# Restore tools
dotnet tool restore

# General clean up
rd -r **/bin/; rd -r **/obj/;

# Run unit tests
rd -r ../**/TestResults/; dotnet test -c Release -s .runsettings; dotnet reportgenerator -targetdir:coveragereport -reports:**/coverage.cobertura.xml -reporttypes:"html;jsonsummary"; start coveragereport/index.html;

# Run mutation tests
rd -r ../**/StrykerOutput/; dotnet stryker -o;

# Pack and publish a pre-release to a local feed
$suffix="alpha001"; dotnet pack -c Release -o nu --version-suffix $suffix; dotnet nuget push "nu\*.*$suffix.nupkg" --source localdev; gci nu/ | ri -r; rmdir nu;
```

## build docker image
``` bash
# The following command passes a sensitive file as a secret to the docker build process (one-time secrets; not needed at run time)
# In this case, it contains the github PAT token to read packages from the private feed :)
docker build -f ".\ne14.portal.api\Dockerfile" --force-rm --tag portalapi --secret id=nuget_config_file,src="C:\temp\nuget-docker.golden-path.config" .
```
