# WidgetBoard

Need to run WinAppDriver

write up instructions for nodejs and Appium

run windows test via

dotnet test --framework net8.0  WidgetBoard.AutomationTests --settings .\WidgetBoard.AutomationTests\windows.runsettings

# Key Tool

keytool is a Java utility used to create keystores and manage cryptographic keys — essential for signing Android apps. It comes with the Java Development Kit (JDK).

Step 1: Make Sure JDK Is Installed Open PowerShell and run: java -version

If it isn't installed, install the Java JDK from Oracle

If it is installed, you may have to set the environment variables to point to its bin directory as that is where keytool is installed

set JAVA_HOME to C:\Program Files\Java\jdk-24 (or wherever yours is installed)
set PATH to include %JAVA_HOME%\bin

## Publish Android

# Generate Keystore file

keytool -genkeypair -v -keystore {filename}.keystore -alias {keyname} -keyalg RSA -keysize 2048 -validity 10000

# Generate Android Bundle

dotnet publish ./WidgetBoard/WidgetBoard.csproj --framework net8.0-android34.0 -p:ApplicationVersion="1" -p:AndroidKeyStore=true -p:AndroidSigningKeyStore="{filename}.keystore"
