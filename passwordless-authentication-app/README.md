### Passwordless Authentication Application
This is a passwordless authentication application. It allows users to log in using their email address without a password. An authentication token is sent to the user's email, granting access to the site for 60 seconds. The application is built using PHP, MySQL database, and the PHPMailer library. After authentication, users can view their course assessment scores.

https://github.com/Fung1117/web-development/assets/99673664/e8f13318-7fe8-4132-b544-c335c00b6c0c

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
