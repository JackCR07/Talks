angular.module('MessageCtrl', []).controller('MessageController', function($scope , $http) {
        $scope.formData = {};

        // when landing on the page, get all todos and show them
        $http.get('/api/messages')
                .success(function(data) {
                  var message="";
                  for(var i=0;i<data.length;i++){
                        message += 'User: '+data[i].user+' Message: '+data[i].body+'\r\n';
                      }
                      $scope.tagline=message;
                })
                .error(function(data) {
                        console.log('Error: ' + data);
                });





});