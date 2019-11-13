const io = require("socket.io")({
  transports: ["websocket"]
})

let sequenceNumberByClient = new Map();
io.attach(4571);

io.sockets.on("connection", function(socket) {
  sequenceNumberByClient.set(socket, 1);
  console.log("Client has joined: " + socket.id);
  socket.emit("Connection success");

  socket.on("test", function() {
    socket.emit("Accepted");
  });

  socket.on("sendinfotoserver", function(msg) {
    console.log("message from unity: " + msg);
  });

  socket.emit("sendinfotounity", {
    name: "smth",
    id: "smth"
  });

    socket.on("disconnect", () => {
      sequenceNumberByClient.delete(socket);
      console.info(`Client has left: ` +socket.id);
    });

  socket.on("availableclients", () => {
  var db = require("./db");
  var Offer = db.Mongoose.model("offer", db.OfferSchema, "offer");
  var offers = Offer.find({}, { "clients.$": 1, _id: 0 })
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
         let jsonArr = { clients: docs };
      socket.emit("returnavailableclients", {
        clients: jsonArr,
      });
    });
    })

});

// from client to status did he accept or not

// one from driver says accepted or not

// one client to server that is saying coordinates user info

// driver to server gives driver info 

// driver to client name and distance
//driver server
// server client
// device id from socket