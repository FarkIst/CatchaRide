const express = require("express");
const router = express.Router(); // eslint-disable-line new-cap

// Endpoint called by the drivers to choose a client to pick up for the ride
router.get("/", function(req, res) {

let dist1 = req.body[0].distance
let dist2 = req.body[1].distance;
  // Change
  let list1 = getList(dist1, x)
  let list2 = getList(dist2, x)
  let answer = true;

  res.statusCode = 200;
  res.json(answer);
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
