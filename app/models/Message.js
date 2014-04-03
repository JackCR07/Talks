var mongoose = require('mongoose');
var Schema=mongoose.Schema;
var message=new Schema({
    user : String,
    body : String
  },{collection:'Message'});
  module.exports = mongoose.model('Message',message);