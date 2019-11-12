const express = require('express');
const bookingRoutes = require('./routes/booking');
const rideOfferRoutes = require("./routes/rideOffer");
const router = express.Router();

router.use('/booking', bookingRoutes);

module.exports = router;