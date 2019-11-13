# CatchaRide

##### A. Business Scenario: 

* The structure of the program consists of consumers making decision based on request & received data via. an app and a map API to reserve a ride/taxi to a desired location. 

* Data formats/structures consists of JSON, but XML or PDF are undergoing considerations. 

* Our process design is listed and can be found under the Diagrams folder in this exact repo.


##### B. Software Implementation: 
* What loosely coupled sub-systems do we make use of, if any? (...)

* Does our sub-systems utilize an asynchronous communication between them? (...) 

* What EIPs do we make use of? 

We make use of an point-to-point channel and Message Channel between the customer and driver for our requested and received data via an NodeJS Express Server that acts as a Middleware. 
We use this to execute the code, make changes to the request and response objects, and most notably to call the next middleware in the stack. 

The next EIP is the Authentication Middleware (Auth) which requires the customer/client to make an order for the process to continue otherwise it will fail the Authentication response and exit the process with an error response. When an successful authentication occurs, it gets parsed via. next() to our route middleware.

Our Route Middleware takes care of the GET and POST requests routes, to receive an ID and Name of the client and successful confirmation order. 

(...) Check into Message Channels, Message Construction, Message Router, Message Transformation and Endpoint. To verify if we utilize any other EIPs. 


##### Installation Instructions: 
