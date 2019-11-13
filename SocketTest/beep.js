var io = require('socket.io')({
	transports: ['websocket'],
});

io.attach(4567);

io.on('connection', function(socket){
	socket.on('beep', function(){
		
		socket.emit('boop');
	});

	socket.on('test', function(){
		socket.emit('Accepted');
	});

	socket.on('sendinfotoserver', function(msg){
		console.log('message from unity: '  + msg);

	});

	socket.emit('sendinfotounity',{
		name: "smth",
		id: "smth"
	});
})
