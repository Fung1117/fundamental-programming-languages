### Passwordless Authentication Application
This is a passwordless authentication application. It allows users to log in using their email address without a password. An authentication token is sent to the user's email, granting access to the site for 60 seconds. The application is built using PHP, MySQL database, and the PHPMailer library. After authentication, users can view their course assessment scores.

https://github.com/Fung1117/web-development/assets/99673664/516bd328-e518-4f31-8a5c-ab3345552156

https://github.com/Fung1117/web-development/assets/99673664/1258e99b-2d8c-4b3a-80cc-7bfc53dff9a6

### sending an email to the email account:
![image](https://github.com/Fung1117/web-development/assets/99673664/dc229666-6f43-4503-b9dd-21592cf46580)
### after you click the link:
https://github.com/Fung1117/web-development/assets/99673664/1009f4b6-82bb-46df-a89d-be375df7f66c
### After a few minutes, an automatic logout occurs:
https://github.com/Fung1117/web-development/assets/99673664/59e09395-01b2-4c88-a79b-7ca6855578cc

### Features
- Passwordless login using email address
- Authentication token sent to user's email for access
- Token expires after 60 seconds
- Utilizes PHP, MySQL database, and PHPMailer library

### Technologies Used
- PHP
- MySQL
- PHPMailer library

### Usage
1. Download the zip file and extract its contents.
2. Create a database called "db3322" and import the user.sql and courseinfo.sql files.
3. Start the Apache and MySQL docker containers.
4. Visit http://localhost:9080/login.php ↗.
5. Enter your email address to receive the authentication token.
6. Click the link in the email to access the internal pages.
7. The token will expire after 60 seconds.
