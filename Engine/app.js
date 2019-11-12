const express = require("express");
const app = express();
const bodyParser = require('body-parser');
const cors = require('cors');
const routes =require( './routes');
app.use(bodyParser.json({ type: 'application/json' }));
app.use(cors());

app.listen(3001, () => {
  console.log("Server running on port 3001");
});

app.use('/api', routes);
module.exports = app;