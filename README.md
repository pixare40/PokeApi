# PokeApi

Pokemon API for getting pokemon data from the pokeapi endpoint built using asp.net core.

## Organisation

The project is is organised into 5 projects:

* PokeApi - The API project. Entrypoint for the app.
* PokeApi.Infrastructure - project housing infrastructure code such as HttpClientWrapper
* PokeApi.Models - housing common models
* PokeApi.PokemonManagement - Domain library housing specific code for pokemon management
* PokeApi.Tests - the test project

## Deploy and Run

The application is docker ready. The docker file is located in the PokeApi folder as well as a docker compose yaml file.

### Steps

* Download Docker Desktop at <https://www.docker.com/products/docker-desktop>
* To deploy and run, cd into the root folder and run:

```
docker-compose -f docker-compose.yml up
```

You can now navigate to the application at
<http://localhost:8080/pokemon>

## Things I could have done differently in production level situation:

* CACHING - At present I have attempted to cache results using the built in inMemoryCache due to the rate limits for the translations API. In production a more robust solution would be using an off app solution like redis.
* ERROR HANDLING - In production readable errors in case of something for example if the pokemon could not be found should be more clear so that the application consuming the API can be more clear to the end user. At present I have not done enough to create better error handling in the API system which would be of utmost necessity in a production system.
* DOCUMENTATION - I have not really documented this API at all. A simple thing would be displaying the swagger pages in development but that would not be sufficiently conclusive.
* LISKOV SUBSTITUTION - I tried not to over engineer the dynamic nature of the translators because I kept thinking of how I could possibly add more translators which are completely different in future but I wanted it still simple enough for the scope of the project.
* INTERFACE SEGREGATION - The reasoning behind having multiple class libraries abstracted by interfaces is to mimic having separate teams working across multiple domains instead providing consumable libraries for the product.
* TESTING - I would split the tests from one project to multiple projects which would be the case if there were different domain teams.
* CODE COVERAGE REPORTS - Generate code coverage reports to a dashboard to uphold code quality.
* LOGGING & TELEMETRY - in this current implementation I haven't included any logging even at console level. But in production I would engineer this to a Splunk instance for example. Metrics of things like execution timings and hit rates would be important for factors such as if we had subscription to the translation API etc. We can do Telemetry using .NET Application Insights. Also important to know whether we have added items that slow down the system.
* CONFIGURATION - I have tried to isolate configurable sections for the app into the appsettings files as good practice and we can adjust these variables as part of the CI/CD pipeline if needed.
* COMMITS - Commits messages could be a bit more detailed.
* NAMING CONVENTIONS - I might have chosen weird names for some of the files 😉.
  
### Contact me for any support query