
# Green Rooster - An employee rostering application

This project was made in 14 short days by a team of three aspiring software developers studying at CodeCool. We had 4 sprints to do the project and before each sprint we had a week to familiarize ourselves with the concepts that we were about to implement in the upcoming sprint. For the backend we have learned and used ASP.NET Core, Entity Framework, Authentication, CI-CD, GitHub Actions, Docker and Docker Compose. For the backend we used React.

The application tackles the problem of hospital employee scheduling. We aimed to build a compact platform that creates a roster that takes into account the preferences of the employees and some other constraints. During this short time period we have achieved a lot, but there is still a ton of work to be done.
## Run Locally

There are two ways to run the project locally. We will document the easier way, which will make use of Docker.

### The 1 container way

Install Docker
Clone the project
Make a copy of the .env_public file in the project root folder, rename it to `.env` and add your strong passwords (if the password is not strong enough, the SQL server won't run).

Open a terminal and cd into the root folder, then enter:

```bash
docker-compose up
```

After a few seconds, the project should be running. The backend has a Swagger side for testing, which should be reachable on https://localhost:44353 and the frontend is on https://localhost:3000

Default login/pass for admin is admin twice (`admin, admin`), for the users it's user + id, twice (`user1, user1`). And it's `accountant, accountant` for the accountant.

### The 2 container way
You can run the backend and the frontend separately by cd-ing into the appropriate folders, making the .env copies and running docker-compose up in each. Run the backend first, else the frontend won't work - obviously.
## Authors

This project was made by 
- [whitword](https://github.com/whitword),
- [gregpappdev](https://github.com/gregpappdev) and
- [hrudolf](https://github.com/hrudolf/)

as their final, 4 week sprint project in the Advanced module of CodeCool's full stack development course.
