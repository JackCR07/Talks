angular.module('MessageService', []).factory('Message', ['$http', function($http) {

return {
    // call to get all message
    get : function() {
      return $http.get('/api/messages');
    },

    // call to POST and create a new message
    create : function(messageData) {
      return $http.post('/api/messages', messageData);
    }
  }

}]);