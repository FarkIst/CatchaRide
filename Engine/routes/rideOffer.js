const express = require("express");
const router = express.Router(); // eslint-disable-line new-cap

// Endpoint called by the drivers to see a client to pick up for the ride
router.get("/", function(req, res) {
  
  var db = require('../db');
  var Offer = db.Mongoose.model('offer', db.OfferSchema, 'offer');
  var offers = Offer.find({})
    .lean()
    .exec(function(e, docs) {
      res.statusCode = 200;
      if (docs) {
        docs = docs.map(function(obj) {
          return {
            id: obj._id,
            clientName: obj.clientName,
            distance:
              getDistanceFromLatLonInKm(
                obj.clientCoordinates.latitude,
                obj.clientCoordinates.longitude,
                req.query.latitude,
                req.query.longitude
              ) + "km"
          };
        });
      }
        res.json({clients: docs});
        res.end();
    });


});

// Endpoint called the driver to get back status of ride (if the client accepted or not)
router.get("/:id/status", function(req, res) {
  let id = req.params.id;
  var db = require('../db');
  var Offer = db.Mongoose.model('offer', db.OfferSchema, 'offer');
  Offer.find({_id: id}).lean().exec(function(err, docs) {
    res.statusCode = 200;
    res.json({ acceptedByClient: docs[0].acceptedByClient});
    res.end();
  });
});

//Endpoint used by the driver to give a response to the client
router.post("/:id", function(req, res) {
  let id = req.params.id;
  let answer = req.body.answer;

  if(answer == "Accepted") {
    var db = require('../db');
    var Offer = db.Mongoose.model('offer', db.OfferSchema, 'offer');
    Offer.findOneAndUpdate({_id: id}, {acceptedByDriver: answer}).lean().exec(function(err, docs){
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

function getDistanceFromLatLonInKm(latitude1,longitude1,latitude2,longitude2) {
  var p = 0.017453292519943295;    //This is  Math.PI / 180
  var c = Math.cos;
  var a = 0.5 - c((latitude2 - latitude1) * p)/2 + 
          c(latitude1 * p) * c(latitude2 * p) * 
          (1 - c((longitude2 - longitude1) * p))/2;
  var R = 6371; //  Earth distance in km so it will return the distance in km
  var dist = 2 * R * Math.asin(Math.sqrt(a)); 
  return dist;
}

module.exports = router;
