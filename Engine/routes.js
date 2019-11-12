const express = require('express');
const bookingRoutes = require('./routes/booking');
const rideOfferRoutes = require("./routes/rideOffer");
const router = express.Router();

router.use('/booking', bookingRoutes);
router.use('/ride-offers', rideOfferRoutes);

module.exports = router;