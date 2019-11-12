const express = require('express');
const router = express.Router(); // eslint-disable-line new-cap

// Endpoint called by the client to check if a driver has selected his ride offer and wants to pick him up
router.get('/:bookingId/status', function (req, res) {
  let id = req.params.bookingId;

  // Change
  let answer = true;

  res.statusCode = 200;
  res.json(answer);
});

// Endpoint called by the client to make a ride offer for the drivers
router.post('/', function (req, res) {
  var db = require('../db');

  let name = req.body.name;
  let lat = req.body.coordinates.lat;
  let lon = req.body.coordinates.lon;

  var Offer = db.Mongoose.model('offer', db.OfferSchema, 'offer');
  var newOffer = new Offer({
      clientName: name,
      coordinates: {
        latitude: lat,
        longitude: lon
      },
      acceptedByDriver: "Pending",
      acceptedByClient: "Pending"
    }
  );

  newOffer.save(function (err) {
      if (err) {
          res.status(500).json({ error: err.message });
          res.end();
          return;
      }
      res.json({id: newOffer.id});
      res.end();
  });

  // Change
  // let answer = true;

  // res.statusCode = 200;
  // res.json('hi');
});

//Endpoint used by the client give a response to the driver he has been assigned
router.post("/:bookingId", function(req, res) {
  let name = req.body.name;
  let lat = req.body.coordinates.lat;
  let lon = req.body.coordinates.lon;

  // Change
  let answer = true;

  res.statusCode = 200;
  res.json(answer);
});


module.exports = router;