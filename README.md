# api
Api for project marking system

# How to run?

Before running the api you will need to add:

```
"FrontendCors": "http://localhost:3000"
```

to `appsettings.Development.json` for development. This enables fetching and posting data to the api from the specified URI.

```
  "Jwt": {
    "Secret": "hrtusixntbegahtrkdlidjenxabvqlsu"
  }
```

to `appsettings.Development.json` for development. This enables the JWT secret, it will not run without it.

---

Inside `marking-api.API` run the following:

```
dotnet run watch
```
