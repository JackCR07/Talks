angular.module('appRoutes', []).config(['$routeProvider', '$locationProvider', function($routeProvider, $locationProvider) {

	$routeProvider

		// home page
		.when('/', {
			templateUrl: 'views/home.html',
			controller: 'MainController'
		})

		.when('/messages', {
			templateUrl: 'views/message.html',
			controller: 'MessageController'
		});

	$locationProvider.html5Mode(true);

}]);