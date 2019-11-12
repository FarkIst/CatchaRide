const express = require("express");
const router = express.Router(); // eslint-disable-line new-cap

// Endpoint called by the drivers to choose a client to pick up for the ride
router.get("/", function(req, res) {

  var db = require('../db');
  var Offer = db.Mongoose.model('offer', db.OfferSchema, 'offer');
  var offers = Offer.find({}).lean().exec(function(e,docs){
    res.statusCode = 200;
    res.json(docs);
    res.end();
 });
});

// Endpoint called by both the driver to get back status of ride (if the client accepted or not)
router.get("/:id/status", function(req, res) {

  // Change
  let answer = true;

  res.statusCode = 200;
  res.json(answer);
});

//Endpoint used by the client give a response to the driver he has been assigned
router.post("/:rideOfferId", function(req, res) {
  let id = req.params.rideOfferId;
  // Change
  let answer = true;

  res.statusCode = 200;
  res.json(answer);
});

module.exports = router;
