@echo Off
set config=%1
if "%config%" == "" (
   set config=Debug
)

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild .\src\Stripe.csproj /p:Configuration="%config%" /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

if "%config%" == "Release" (
	nuget pack .\src\Stripe.csproj -Build -Symbols -Properties Configuration=Release
)