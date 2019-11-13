const express = require("express");
const app = express();
const bodyParser = require('body-parser');
const cors = require('cors');
const routes = require( './routes');
const socket = require('./socket');
require("dotenv").config();

app.use(bodyParser.json({ type: 'application/json' }));
app.use(cors());

const port = process.env.port || 3001;

app.listen(port , () => {
  console.log(`Server running on ${port}`);
});

app.use('/api', routes);
module.exports = app;