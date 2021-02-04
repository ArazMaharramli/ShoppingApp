# ShoppingApp

E-commerce web project which contains both web api and web UI applications. Developed by using C# .Net Core framework, CQRS, Repository pattern, auto mapper auto and validator

## Installation requirements

```bash
.Net Core 3.1
MSSQL server 2019
```

## Usage
For using code, you need to modify following files:

 1. sharedappsettings_forgit.json
 2. appsettings_forgit.json (In Web.Api project)
 3. appsettings_forgit.json (In Web.UI project)

Required Steps are:
 1. Replace values of keys with your parameters. For example:\
```json
"ApiKey":"AbC123KJE43avfe96"
```
 2. Remove "_forgit" part from the name of settings file and save
## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MI](https://choosealicense.com/licenses/mit/)
