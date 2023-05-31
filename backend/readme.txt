https://github.com/dotnet/dotnet-docker/blob/main/samples/host-aspnetcore-https.md

Setup to use https (further steps might be needed):
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\backend.pfx -p abcABC123!
dotnet dev-certs https --trust

backend mappában:
docker build -t greenroosterbackend:0.5 .

command to run and remove when stopped
docker run --rm -it -p 44353:5000 -v $Env:USERPROFILE\.aspnet\https:/https/ greenroosterbackend:0.5

command to run
docker run -it -p 44353:5000 -v $Env:USERPROFILE\.aspnet\https:/https/ greenroosterbackend:0.5