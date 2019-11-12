const mongoose = require('mongoose');
mongoose.connect('mongodb://localhost:27017/catch-a-ride',
    {
        useNewUrlParser:true,
        useUnifiedTopology: true
    }
);

var offerSchema = new mongoose.Schema({
    clientName: String,
    coordinates: {
        latitude: Number,
        longitude: Number
    },
    acceptedByDriver: String,
    acceptedByClient: String
}, { collection: 'offer' }
);
 
module.exports = { Mongoose: mongoose, OfferSchema: offerSchema }