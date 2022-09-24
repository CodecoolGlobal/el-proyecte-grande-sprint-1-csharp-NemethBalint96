# El Proyecte Grande

## Story

As your Codecool Journey nears its end, the time has come for a final Teamwork Project that tests all the programming skills you've obtained so far (and some new ones you will learn along the way).

You have the freedom of choosing your teammates (assemble a team of 3-4 students) and the topic of the project this time. Think of an app that you would find useful in your daily activities, a tool that an employee of a certain industry might crave, a fun game or something completely out of the box.

This project is meant for four sprints at least, but it may keep you company until the end of the course, or even much longer. Who knows? Although we do not give you any direct tasks to fulfill, there are some technical requirements for each sprint. You are expected to make incremental changes in a Scrum way, developing the project further and further, adding new features, technologies, and so on.

¡Comience El Proyecte Grande!

## What are you going to learn?

- Work in a Scrum team on a real project.
- Grow your project iteratively.
- Deliver increments in each sprint.

## Tasks

### Sprint planning
Create a Product backlog (on Github) with user stories that cover at least the feature set you aim to complete next. Break down the user stories into smaller tasks, prioritize them, estimate them, and, taking your capacities into account, determine how far you can get during this sprint.
- There is a Product backlog for the project.
- The backlog items are broken down into smaller tasks or subtasks.
- The backlog items are in priority order in the backlog.
- Each backlog item (at least those that are relevant for the actual sprint) has an estimation value.
- The top priority part of the backlog is marked as the Sprint backlog, in accordance with the estimation values and the foreseeable team resources.
- The backlog and the project plan are checked and accepted by a mentor on the first day of the sprint (before any implementation).
- By the end of the sprint, there is less than 30% deviation from the plan (70% to 130% is completed according to the original plan)

### Agile work
You need to use technologies which help you achieve an Agile workflow.
- Every item in the backlog appears as an Issue on GitHub.
- The repository has a Project defined on GitHub for every sprint. The project board contains every issue related to the sprint.
- With every feature branch, a Pull request is opened and maintained. The Pull request contains the Issue linked with it. The Pull request contains the assignee, who is responsible for the given Issue. The Pull request contains at least one Reviewer, who is responsible for checking on their peers' work.

### Scrum practices during development
Use Scrum methodology with your team throughout your project.
- A daily Scrum meeting is organized by the Scrum Master (no longer than 15 minutes).
- Any necessary corrections in the sprint plan are introduced to the backlog and validated by a mentor.
- After the demo, the Scrum Master organizes a Sprint Review meeting, during which the team investigates how much of the planned Sprint Backlog is fulfilled, and whether it is well thought-out and balanced for the team to handle.
- Each Sprint Review produces an Increment Document, that is, a changelog of sorts, listing all the changes to the product that are a result of this sprint.
- After the Sprint Review, the Scrum Master organizes a Sprint Retrospective meeting, during which the team discusses how the work went during the sprint, what practices worked well, what needs improvement, and what needs to stop (and also what to introduce).

### Technical requirements
You need to fulfill a few technical requirements.
### Sprint - 1
- The project backend is written in ASP.NET.
- The project has at least three pages which use the templating system of the framework.
### Sprint - 2
- The project has a backend part with at least five API endpoint patterns providing data in JSON format.
- The project has a frontend part written in React which consumes at least five different API endpoint patterns from the backend.
### Sprint - 3
#### Backend
- The project has a backend part with an Object Relation Mapping tool communicating with a database, where the applications data is persisted.
#### Frontend
- The project has a frontend part written in React which consumes at least ten different API endpoint patterns from the backend.
### Sprint - 4
#### Backend
- A user can register into the application by setting at least their username, e-mail address, and password.
- A user can log in to the application.
- The user can log out from the application.
- There are at least two different roles defined in the application.
- There are at least 3 endpoints, that are only available for authenticated users.
- There are at least 5 endpoints, that are only available for authenticated users.
- There is an admin page, which lists all users of the application, available only with the admin role.
#### OPTIONAL TASK: Welcome e-mail
After a successful registration (username not taken, etc.) send a welcome e-mail to your new users!
- After successful registration, the user receives an email welcoming them to the page.
#### OPTIONAL TASK: Forgot your password?
Create a Forgot your password button on the login page, that can help restore the password by email.
- There is a Forgot your password button on the login page.
- After clicking on the Forgot your password button the user can give their email.
- After the email given for recovering the password, the email is validated. If no such email can be found in the database, an error is shown.
- After choosing Forgot your password with a registered account, an email is received.
- The Forgot your password email contains a link, that redirects to a page, where the user can set a new password. The password belonging to the email address is overwritten to the new one given.
#### OPTIONAL TASK: Integration with Google
Add a possibility to log in with Google Id using guide provided in resources
- New project in Google's credential page is added. It has an OAuth 2.0 Client IDs record.
- User secrets are used to store Google's ClientSecret. Ensure this data is not saved in the repo!
- Google authentication is enabled. Users can successfully authenticate via GoogleId.
#### Frontend
- There is a registration page, where a user can register by setting at least their username, e-mail address, and password.
- There is a login page, where a user can log in to the application, by giving their username and password.
- The username of the logged-in user is shown on the page's header.
- There is a logout button on the page. After hitting the logout button, the user is redirected to the login page.
### Sprint - 5
#### Backend
- The project's test coverage is at least 60%.
- There is a workflow that runs tests upon push or PR on development branch.
- There is a workflow that runs tests upon push or PR on main branch.
- There is a running lint test in GitHub Actions for the app.
- Valid Dockerfile created, the image starts the app.
- A successful testing workflow triggers a deployment workflow (only if a PR has been merged or commits are pushed to the main branch).
- The workflow builds and pushes the Docker image to DockerHub.
- The database has no exposed ports. The application connects to the database via a docker network.
- The workflow deploys the application's Docker image to Heroku, the app is up and running.
#### Frontend
- The frontend is deployed with a production build.
- There is a health-check endpoint which shows the status of the backend (up or not).
- If the backend is unavailable, the offline status is displayed on the frontend.
#### OPTIONAL TASK:
A refreshing sight - UX updates
You need to fulfill a couple of technical requirements defined below related to refreshing the page.
- When a user is logged in, refreshing the page does not logs them out.
- When the page is refreshed, the route (and the rendered components) stay as before refreshing.
#### OPTIONAL TASK:
Ooops, something went wrong
Your page should be ready to hide any malfunction - whether a missing field of the response, or a network error, it should not be displayed explicitly on the page. Use a nice error page instead, so you would not expose any weakness or vulnerability of your page, and may show a fun message instead.
- There is a non-descriptive error page displayed for any error.
- If the user would navigate on a non-existing route, there is a fancy Page not found-page displayed.
