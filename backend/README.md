# Backend Submission

This is the backend submission for NZMSA phase 2. It is a Pokemon Team Api that a user can make different teams with three of their favorite pokemons. The teams are stored in local in-memory caching service. There are in total of six different APIs designed to give all the services to the user in order to make their Pokemon teams.
![Capture](https://user-images.githubusercontent.com/48300768/183849760-413877e7-458e-46e6-af75-bb50a700e7ba.JPG)

When user want to change one of their pokemons to their favorite ones, user need to refer to [Pokeapi](https://pokeapi.co/) as there are some pokemons that are not present in this API. For this submission, Pokeapi was used as 3rd party API, in order, to meet the requirements for the submission.

For configuration files, two files are used to start the project, i.e., appsettings.Development.json and appsettings.json. For this submission, we used appsettings.Development.json to access some configuration as we mainly work in development phase. However, if used the appsettings.json to start the project, it could cause some issues as most of the settings have been kept to default.

Dependency Injection (DI) is a programming technique that makes a class independent of its dependencies. It is one of the popular tools used in .Net C# as it reduces the amount you have to code, facilitating the creation of better applications and a smoother development process. Using middleware like swagger greatly helped in development phase when we needed to test and debug many parts of the code.

Also, for this submission, a NUnit unit test project was made in order to test the functionality of the main project. By running all the tests, it was confirmed that all the functions are working fine. There are two tests that fails when running the unit test, however, running both again will make them green again.

Using middleware libraries made the code easier to test as it greatly helped in testing the functionality of the API without writing too much test code. It also makes the debugging a lot easier too, in order to find the issue in the test code.
