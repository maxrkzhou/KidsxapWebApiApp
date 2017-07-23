# README #

This is a n-tier application:
1. .NET MVC as the presentation tier
2. BusinessLogic as the BLL
3. Repository as the DAL
4. EntityModel presents Database
5. ViewModel presesnt view logic
6. DependencyResolver is for managing the dependencies relations
7. SettingConstant is where to store settings values

Note:
1. The web api applies the soft delete as required 
2. Attribute is created, and stored in KidsapWebApi/Filters. It can verify the code for either HTTP DELETE Request or Non HTTP DELETE Request
3. Access Token(for POST, GET, and PUT) and Delete Token are stored in SettingConstant project
