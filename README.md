# PokeApi

Pokemon API for getting pokemon data from the pokeapi endpoint.

## Organisation

The project is is organised into 5 projects:

* PokeApi - The main project
* PokeApi.Infrastructure - project housing infrastructure code such as HttpClient
* PokeApi.Models - housing common models
* PokeApi.PokemonManagement - Domain library housing specific code for pokemon management
* PokeApi.Tests - the test project

## Deploy and Run

The application is docker ready with Orchestration support. The docker file is located in the PokeApi folder as well as a docker compose yml file.

To deploy and run cd into the PokeApi folder and run:
```
docker-compose -f docker-compose.yml up
```