# CatchaRide

##### A. Business Scenario: 

* The structure of the program consists of consumers making decision based on request & received data via. an app and a map API to reserve a ride/taxi to a desired location. 

* Data formats/structures consists of JSON, but XML or PDF are undergoing considerations. 

* Our process design is listed and can be found under the Diagrams folder in this exact repo.


##### B. Software Implementation: 
* Integration Platform: 
We're using two integration platforms, BPNM and (MOM) by utilizing NodeJS.

* What EIPs do we make use of? 

Simply put: 

* Point-to-Point, one client per reservation
* Request/Reply pattern
* (Adapter) 
* History, call and organize list of ID, clients/orders
* (Events)
* Invalid Message


##### Middleware 

We make use of an point-to-point channel between the customer and driver for our requested and received data via an NodeJS Express Server that acts as a Middleware. 
We use this to execute the code, make changes to the request and response objects, and most notably to call the next middleware in the stack. 

 Authentication Middleware (Auth) which requires the customer/client to make an order for the process to continue otherwise it will fail the Authentication response and exit the process with an error response. When an successful authentication occurs, it gets parsed via. next() to our route middleware.

Our Route Middleware takes care of the GET and POST requests routes, to receive an ID and Name of the client or clientlist and successful confirmation order. 

