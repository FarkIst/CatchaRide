const express = require('express');
const router = express.Router(); // eslint-disable-line new-cap

// Endpoint called by the client to check if a driver has selected his ride offer and wants to pick him up
router.get('/:id/', function (req, res) {
  let id = req.params.id;
  var db = require('../db');
  var Offer = db.Mongoose.model('offer', db.OfferSchema, 'offer');
  Offer.find({_id: id}).lean().exec(function(err, docs) {
    if(docs.length) {
      res.statusCode = 200;
      res.json({ acceptedByDriver: docs[0].acceptedByDriver});
    } else {
      res.statusCode = 400;
      res.json("Invalid id");
    }
    res.end();
  });
});

// Endpoint called by the client to make a ride offer for the drivers
router.post('/', function (req, res) {
  var db = require('../db');

  let name = req.body.name;
  let latitude = req.body.coordinates.latitude;
  let longitude = req.body.coordinates.longitude;

  var Offer = db.Mongoose.model('offer', db.OfferSchema, 'offer');
  var newOffer = new Offer({
      clientName: name,
      clientCoordinates: {
        latitude: latitude,
        longitude: longitude
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
});

//Endpoint used by the client give a response to the driver he has been assigned
router.post("/:id", function(req, res) {
  let id = req.params.id;
  let answer = req.body.answer;


  if(answer == "Accepted" || answer == "Denied") {
    var db = require('../db');
    var Offer = db.Mongoose.model('offer', db.OfferSchema, 'offer');
    Offer.findOneAndUpdate({_id: id}, {acceptedByClient: answer}).lean().exec(function(err, docs){
      if (err) {
        res.status(400).json("Invalid id");
        res.end();
        return;
      }
      res.statusCode = 200;
      res.end();
    });
  } else {
    res.statusCode = 400;
    res.json("Invalid answer");
  }
});


module.exports = router;