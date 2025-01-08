# User Authentication API - Developed with Strategy Pattern
This project provides an API that performs user authentication using the Strategy Pattern. With this pattern, different authentication strategies (such as username-based authentication or email-based authentication) can be easily added or changed.

**Project Overview**

This API includes two main endpoints for logging in and logging out users:
- Login: Authenticates the user with a username/email and password.
- Logout: Logs the user out.
- 
***The Strategy Pattern abstracts** the authentication process, offering a flexible and extensible architecture. As a result, new authentication methods can be easily integrated, and existing strategies can be modified with minimal effort.*

Technologies Used
- C# (.NET Core)
- ASP.NET Core API
- Entity Framework Core
- JWT (JSON Web Tokens) for session management
- HTTPS

**Project Structure**
1. IAuthenticationStrategy:
An interface that abstracts the authentication process. Different strategies for user authentication implement this interface.

2. UsernameAuthenticationStrategy:
A strategy that handles authentication using the username. It verifies the username and password.

3. EmailAuthenticationStrategy:
A strategy that performs authentication using the email address. It verifies the email and password.

4. AuthenticationContext:
Manages the authentication strategy. This class is responsible for selecting and applying the correct authentication strategy.

5. AuthService:
The service layer that handles the authentication logic. This class selects the appropriate strategy and performs the authentication.

6. AuthController:
Provides the login and logout endpoints. It invokes the appropriate authentication strategy based on the user's input.

7. TokenService:
Handles the generation and validation of JSON Web Tokens (JWT).

8. UserService:
Manages user-related operations such as:
