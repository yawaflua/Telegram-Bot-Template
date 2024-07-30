# Template for Telegram BOT project
## About it
This is a template for VS 2023 Community/Enterprice for creating project with Telegram Bot service and base settings.



## Used stack:
- .NET 8 Sdk Web
- ASP
- Swagger UI with commas setup
- Telegram.Bots (For .NET)

# Use as template
For start used this project as a template, u should to download it.
After that, open it with Visual Studio, press "Project" -> "Export Template".
In window u need to select "Project export"(For exporting exactly Project) and press "Next".
In next window u can edit template name, description and icon.
After pressing "Finish" button, you can create project with this template.

### OR

Another way to use this project as a template: ZIP to folder with VS.
Download this project and compress it to .zip file with any name. After that, go to this destination:
```bash
C:\Users\{{YOUR_USERNAME}}\AppData\Roaming\Microsoft\VisualStudio\{{VS_VERSION}}\
```
Create folder with this name: `ProjectTemplatesCache` and pull zipped project to created folder.
After reloading VS you can create new project with this template.

# Use as base for project
Its very simple to use, just download it and in file `appsettings.json` change in 
"tg-token" (`"tg-token": "TOKEN"`) variable from TOKEN to your Bot`s token from [@BotFather](https://t.me/BotFather).

After that, change press RMB on project and select "Sync Namespaces". If you need to use Dockerfile, change from "$safeprojectname$" to your project name in last stroke

# appsettings.json

Your appsettings.json file must be looked like this:
```json
{
	"tg-token": "123456:AaBbCcDd"
}
```
or any with this variable

