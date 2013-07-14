FooTable.Editable
=================

* FooTable.Editable is a jQuery plugin for FooTable that makes all of your FooTables editable.
* FooTable.Editable requires <a href="http://jquery.com/">jQuery</a> 
* Footable.Editable requires <a href="https://github.com/bradvin/FooTable">Footable</a> 

	

Index
-----

* [How Does It Work?](#HowDoesItWork)
* [Quick Start](#QuickStart)


<h2 id="HowDoesItWork">How Does It Work?</h2>

* FooTable.Editable is a Footable extensible plugin that makes all of your Footables editable. Simply add the "fooEditable" class to any <pre><th></pre> tag table heading, and that column suddenly becomes editable.
* The plugin also transports FooTable data and changes via AJAX between the client and server in JSON format which should be suitable for just about scnerio.
* All JSON updates are sent to the server via a dataHandlerURL: provided by the user.
* The dataHandlerURL can also respond back to the client with "Success", "Error", "Load", or "Update" including any JSON data or messages for the client.
* A special data attribute can be added to any th tag <pre><th data-ft-buttons="Add,Delete"></pre> that creates add and/or delete buttons on each row.
* When changes are made to Footable cells, update transactions are also sent to the server. 
* FooTable columns can also be marked with the fooId class to ensure that Primary and Composite key fields are sent to the server with every transaction.  These fooId fields can also be hidden using standard footable data attributes.


<h2 id="QuickStart">Quick Start</h2>

1. Place a referece to footable.editable.js under footable.js. 
2. Place a referece to footable.editable-1.0.css under footable-0.1.css.
3. 
