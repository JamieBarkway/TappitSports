TappitSports API

This is an API for the Tappi technical test. The API provides three end points:
  - A list of sports and how many times they have been favourited.
  - A list of people and their favourite sports as well as further information about the person such as if they are enabled or valid.
  - A single person record with their favourite sports matching a provided ID or last name parameter.

Once the API is running, it should automatically launch on http://localhost:5000/swagger/index.html. The following endpoints are available:

  - GET /personwithfavouritesport?personId={personId}&personLastName={personLastName} - returns a person and their favorite sports based on the provided person ID or last name
  - GET /sportswithfavouritecount - returns a list of sports and how many times they have been favorited.
  - GET /peoplewithfavouritesports - returns a list of people with their favourite sports, whether they are valid, enabled and authorised.

Authentication is not required to access the API.



Acknowledgments

This project was created as part of a technical interview for Tappit.
