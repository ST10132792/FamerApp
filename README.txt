
# Farmer and Product Management System

## Description
This Farmer and Product Management System is a web application designed to help farmers manage their products efficiently. It provides features for adding, editing, and deleting products, as well as managing farmer accounts.

## Features
- **Product Management**: Farmers can add new products, update existing ones, and delete products that are no longer needed.
- **Farmer Management**: Employees can manage user accounts, including creating new user accounts, updating user information, and deleting user accounts.
- **Product Filtering**: Employees can filter products based on category and production date to easily find the products they are looking for.

## Technologies Used
- ASP.NET: The application is built using ASP.NET framework, providing a robust and scalable web development platform.
- C#: C# programming language is used for server-side logic and business logic implementation.
- Entity Framework: Entity Framework is used for data access and database management, providing a convenient way to interact with the underlying database.
- Bootstrap: Bootstrap framework is used for front-end design and styling, ensuring a responsive and visually appealing user interface.

## Installation
1. Download the zipped folder to your computer
2. Unzip the folder
3. Open the solution file (.sln) in Visual Studio.
4. Build the solution to restore dependencies and compile the project.
5. Update the database connection string in the `Web.config` file to point to your local database if the original one doesnt work. Migrate products and userdata then run the query provided with the project of that is the case.
6. Run the application in Visual Studio by pressing F5 or clicking on the Start button.

## Usage
1. Navigate to the URL where the application is hosted (e.g., http://localhost:port).
2. Log in with an existing account if you are an employee or farmer, there is no registration option.
3. Use the navigation menu to access different features such as product management or farmer management.
4. Add, edit, or delete products as needed if you are a farmer.
5. Manage farmer accounts by creating new accounts, updating user information, or deleting accounts if you are an employee.




Sign in details
Email:	farmer1@example.com	Password:	Farmer1@
Email:	farmer2@example.com	Password:	Farmer2@
Email:	Employee1@example.com	Password:	Employee1@

Github repo: https://github.com/ST10132792/FamerApp


Note: Sometimes the database causes issues and sometimes it doesnt. Im not sure why but if it does happen to in this case i have provided the queries that fixed it for me when the issues did happen. sometimes the userroles table doesnt generate at all.
