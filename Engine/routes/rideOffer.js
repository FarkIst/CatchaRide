const express = require("express");
const router = express.Router(); // eslint-disable-line new-cap

// Endpoint called by the drivers to see a client to pick up for the ride
router.get("/", function(req, res) {
  var db = require('../db');
  var Offer = db.Mongoose.model('offer', db.OfferSchema, 'offer');
  var offers = Offer.find({}).lean().exec(function(e,docs){
    res.statusCode = 200;
    docs = docs.map(function (obj) {
      return {id: obj._id, clientName: obj.clientName}
    });
    res.json(docs);
    res.end();
  });
});

// Endpoint called the driver to get back status of ride (if the client accepted or not)
router.get("/:id/status", function(req, res) {

  // Change
  let answer = true;

  res.statusCode = 200;
  res.json(answer);
});

//Endpoint used by the driver to give a response to the client
router.post("/:rideOfferId", function(req, res) {
  let id = req.params.rideOfferId;
  let answer = req.body.answer;

  res.statusCode = 200;
  res.json(answer);
});

module.exports = router;
