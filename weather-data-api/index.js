var express = require('express');
var app = express();

/* Implement the logic here */
/* Task A:  */

//Database
var mongoose = require('mongoose');
const dbserver = "mongodb://mongodb/weather";

mongoose.connect(dbserver, { useNewUrlParser: true, useUnifiedTopology: true })
.then(() => {
  console.log("Connected to MongoDB");
})
.catch(err => {
  console.log("MongoDB connection error: "+err);
});

//Set the Schema
const weatherSchema = new mongoose.Schema({
  date: String,
  meanT: Number,
  maxT: Number,
  minT: Number,
  humidity: Number,
  rain: Number
});
//Create my model
const weather = mongoose.model("wrecord", weatherSchema, "wrecords");


// monitor the database connection and terminate the program if lost the database connection.
const db = mongoose.connection;
db.on('error', console.error.bind(console, 'Connection error:'));
db.on('disconnected', () => {
  console.log('Lost database connection. Exiting...');
  process.exit(1);
});

/* Task B: */

// check if it is a valid date
function isValidDate(year, month, date) {
  if (year < 1000){
    return false;
  }
  const d = new Date(year, month - 1, date);
  return d.getFullYear() === year && d.getMonth() === month - 1 && d.getDate() === date;
}

app.use(express.json());

app.post('/weather/:year/:month/:date', (req, res)=> {
  const year = parseInt(req.params.year);
  const month = parseInt(req.params.month);
  const date = parseInt(req.params.date);

  if (isNaN(year) || isNaN(month) || isNaN(date) || !isValidDate(year, month, date)) {
    return res.status(400).json({ error: 'not a valid year/month/date' });
  }

  const dateString = `${year}${month.toString().padStart(2, '0')}${date.toString().padStart(2, '0')}`;
  
  weather.findOne({ date: dateString })
    .then((weatherData) => {
      if (weatherData) {
        return res.status(403).json({ error: "find an existing record. Cannot override!!" });
      }
      const { meanT, maxT, minT, humidity, rain } = req.body;
      const newWeather = new weather({ date: dateString, meanT, maxT, minT, humidity, rain });
      return newWeather.save()
        .then(() => {
          return res.status(200).json({ okay: "record added"});
        })
        .catch((err) => {
          return res.status(500).json({ error: err.message });
        });
    })
    .catch((err) => {
      return res.status(500).json({ error: err.message });
    });
});

/* Task D: */

// check if it is a valid month
function isValidMonth(year, month) {
  if (month < 1 || month > 12 || year < 1000) {
    return false;
  }
  const startDate = new Date(year, month - 1, 1);
  if (startDate.getFullYear() !== year || startDate.getMonth() !== month - 1) {
    return false;
  }
  return true;
}

// Implement for temp
app.get('/weather/temp/:year/:month', (req, res) => {
  const year = parseInt(req.params.year);
  const month = parseInt(req.params.month);

  if (isNaN(year) || isNaN(month) || !isValidMonth(year, month)) {
    return res.status(400).json({ error: 'not a valid year/month' });
  }

  const endDate = new Date(year, month, 0);
  const startString = `${year}${month.toString().padStart(2, '0')}01`;
  const endString = `${year}${month.toString().padStart(2, '0')}${endDate.getDate().toString().padStart(2, '0')}`;

  weather.find({ date: { $gte: startString, $lte: endString } })
    .then((weatherData) => {
      if (weatherData.length === 0) {
        return res.status(404).json({ error: 'not found' });
      }

      let avgTemp = 0;
      let maxTemp = -1;   
      let minTemp = 1000;
      let numDays = 0;

      weatherData.forEach((day) => {
        if (day.meanT) {
          avgTemp += day.meanT;
          numDays++;
          if (day.maxT > maxTemp) {
            maxTemp = day.maxT;
          }
          if (day.minT < minTemp) {
            minTemp = day.minT;
          }
        }
      });

      if (numDays > 0) {
        avgTemp = avgTemp / numDays;
      }

      const result = {
        Year: year,
        Month: month,
        "Avg Temp": Number(avgTemp.toFixed(1)),
        "Max Temp": Number(maxTemp.toFixed(1)),
        "Min Temp": Number(minTemp.toFixed(1))
      };

      return res.status(200).json(result);
    })
    .catch((err) => {
      return res.status(500).json({ error: err.message });
    });
});

// Implement for humi
app.get('/weather/humi/:year/:month', (req, res) => {
  const year = parseInt(req.params.year);
  const month = parseInt(req.params.month);

  if (isNaN(year) || isNaN(month) || !isValidMonth(year, month)) {
    return res.status(400).json({ error: 'not a valid year/month' });
  }

  const endDate = new Date(year, month, 0);
  const startString = `${year}${month.toString().padStart(2, '0')}01`;
  const endString = `${year}${month.toString().padStart(2, '0')}${endDate.getDate().toString().padStart(2, '0')}`;

  weather.find({ date: { $gte: startString, $lte: endString } })
    .then((weatherData) => {
      if (weatherData.length === 0) {
        return res.status(404).json({ error: 'not found' });
      }

      let avgHumi = 0;
      let maxHumi = -1;
      let minHumi = 1000;
      let numDays = 0;

      weatherData.forEach((day) => {
        if (day.humidity) {
          avgHumi += day.humidity;
          numDays++;
          if (day.humidity > maxHumi) {
            maxHumi = day.humidity;
          }
          if (day.humidity < minHumi) {
            minHumi = day.humidity;
          }
        }
      });

      if (numDays > 0) {
        avgHumi = avgHumi / numDays;
      }

      const result = {
        Year: year,
        Month: month,
        "Avg Humidity": Number(avgHumi.toFixed(2)),
        "Max Humidity": Number(maxHumi.toFixed(1)),
        "Min Humidity": Number(minHumi.toFixed(1))
      };

      return res.status(200).json(result);
    })
    .catch((err) => {
      return res.status(500).json({ error: err.message });
    });
});

// Implement for rain
app.get('/weather/rain/:year/:month', (req, res) => {
  const year = parseInt(req.params.year);
  const month = parseInt(req.params.month);

  if (isNaN(year) || isNaN(month) || !isValidMonth(year, month)) {
    return res.status(400).json({ error: 'not a valid year/month' });
  }
  const endDate = new Date(year, month, 0);
  const startString = `${year}${month.toString().padStart(2, '0')}01`;
  const endString = `${year}${month.toString().padStart(2, '0')}${endDate.getDate().toString().padStart(2, '0')}`;

  weather.find({ date: { $gte: startString, $lte: endString } })
    .then((weatherData) => {
      if (weatherData.length === 0) {
        return res.status(404).json({ error: 'not found' });
      }

      let avgRain = 0;
      let maxRain = 0;
      let numDays = 0;

      weatherData.forEach((day) => {
        if (day.rain) {
          avgRain += day.rain;
          numDays++;
          if (day.rain > maxRain) {
            maxRain = day.rain;
          }
        }
      });

      if (numDays > 0) {
        avgRain = avgRain / numDays;
      }

      const result = {
        Year: year,
        Month: month,
        "Avg Rainfall": Number(avgRain.toFixed(2)),
        "Max Daily Rainfall": Number(maxRain)
      };

      return res.status(200).json(result);
    })
    .catch((err) => {
      return res.status(500).json({ error: err.message });
    });
});


/* Task C: */
app.get('/weather/:year/:month/:date', (req, res) => {
  const year = parseInt(req.params.year);
  const month = parseInt(req.params.month);
  const date = parseInt(req.params.date);
  
  if (isNaN(year) || isNaN(month) || isNaN(date) || !isValidDate(year, month, date)) {
    return res.status(400).json({ error: 'not a valid year/month/date' });
  }

  const dateString = `${year}${month.toString().padStart(2, '0')}${date.toString().padStart(2, '0')}`;
  
  weather.findOne({ date: dateString })
    .then((weatherData) => {
      if (!weatherData) {
        return res.status(404).json({ error: 'not found' });
      }

      // Return the weather data as a JSON string
      return res.status(200).json({
        Year: year,
        Month: month,
        Date: date,
        'Avg Temp': weatherData.meanT,
        'Max Temp': weatherData.maxT,
        'Min Temp': weatherData.minT,
        Humidity: weatherData.humidity,
        Rainfall: weatherData.rain
      });
    })
    .catch((err) => {
      return res.status(500).json({ error: err.message });
    });
});

/* Task E: */
app.all('*', (req, res) => {
  return res.status(400).json({ error: `Cannot ${req.method} ${req.originalUrl}` });
});

// error handler
app.use(function(err, req, res, next) {
  res.status(err.status || 500);
  res.json({'error': err.message});
});

app.listen(8000, () => {
  console.log('Weather app listening on port 8000!')
});
