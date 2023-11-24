## Weather Data API

This project implements a REST API to create, retrieve, and analyze weather data stored in a MongoDB database.

### API Endpoints

The API has the following endpoints:

### POST /weather/:year/:month/:date

Adds a new weather record for the given date.

### GET /weather/:year/:month/:date

Retrieve the weather record for the given date.

### GET /weather/temp/:year/:month

Retrieve the monthly temperature summary for the given year and month.

### GET /weather/humi/:year/:month

Retrieve the monthly humidity summary for the given year and month.

### GET /weather/rain/:year/:month

Retrieve the monthly rainfall summary for the given year and month.

### Other paths

Returns an error for any unexpected paths.

### Usage

To run the API:

```
npm install
node index.js
```

The API will be available at http://localhost:8000.

Example requests can be found in curl-cmd.txt

### Database

The weather records are stored in a MongoDB database called "weather" with a collection named "wrecords".

The database is pre-populated by importing the HKO_2022.csv dataset using mongoimport.

### Testing

The API can be tested using curl or a browser. Example curl commands are provided in curl-cmd.txt.

Let me know if you need any other sections added to the README file!
