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

    

});

// from client to status did he accept or not

// one from driver says accepted or not

// one client to server that is saying coordinates user info

// driver to server gives driver info 

// driver to client name and distance
//driver server
// server client
// device id from socket