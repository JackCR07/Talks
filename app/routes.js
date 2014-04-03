var Message=require('./models/Message');

module.exports = function(app) {

	// server routes ===========================================================
	// handle things like api calls
	// authentication routes
  app.get('/api/messages', function(req, res) {
      // use mongoose to get all nerds in the database
      Message.find(function(err, messages) {

        // if there is an error retrieving, send the error. nothing after res.send(err) will execute
        if (err)
          res.send(err);

        res.json(messages); // return all nerds in JSON format
      });
    });
  app.post('/api/messages', function(req, res) {
    //res.send(req.body);
    // create a todo, information comes from AJAX request from Angular
    Message.create({
      user : req.body.user,
      body : req.body.body
    }, function(err, message) {
      if (err)
        res.send(err);

      // get and return all the todos after you create another
      Message.find(function(err, messages) {
        if (err)
          res.send(err)
        res.json(messages);
      });
    });

  });
	// frontend routes =========================================================
	// route to handle all angular requests
	app.get('*', function(req, res) {
		res.sendfile('./public/index.html');
	});

};