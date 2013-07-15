FooTable.Editable (ALPHA RELEASE!)
==================================

This code is an ALPHA level release.  Please help us work out the bugs and use at your own risk :)

* FooTable.Editable is a jQuery plugin for FooTable that makes all of your FooTables editable.
* FooTable.Editable requires <a href="http://jquery.com/">jQuery</a> 
* Footable.Editable requires <a href="https://github.com/bradvin/FooTable">Footable</a> 

![FooTable.Editable](https://raw.github.com/jakemdrew/FooTable.Editable/master/screenshot.jpg "FooTable")	

Index
-----

* [How Does It Work?](#HowDoesItWork)
* [Quick Start](#QuickStart)
* [ftEditable API](#ftEditableAPI)
* [ftEditable API functions](#ftEditableAPIfunctions)


<h2 id="HowDoesItWork">How Does It Work?</h2>

* FooTable.Editable is a Footable extensible plugin that makes all of your Footables editable. Simply add the "fooEditable" class to any th tag, and that column suddenly becomes editable.
* The plugin also transports FooTable data and changes via AJAX between the client and server in JSON format which should be suitable for just about scnerio.
* All JSON updates are sent to the server via a dataHandlerURL: provided by the user.
* The dataHandlerURL can also respond back to the client with "Success", "Error", "Load", or "Update" including any JSON data or messages for the client.
* A special fooId class can be added to any th tag to ensure that Primary and Composite key fields are sent to the server with every transaction.  These fooId fields can also be hidden using other standard footable data attributes.
* The data attribute (data-ft-buttons="Add,Delete") can be used to include "add" and / or "delete" buttons on each table row.
* Users can create a blank row for new records by simply marking the blank row with the class "fooNewRecord".
* Customer FooTable parsers can be applied to display values sent back from the server using the data-return-type= attribute.
* Finally, an ftEditable API is included to expose many of the internal functions included within the FooTable.Editable plugin providing advanced users with greater flexibility.  


<h2 id="QuickStart">Quick Start</h2>

<strong>The best place to get started is the demo's folder.  There are several html demo markups and a full ASP.net solution including a generic handler written in C#.</strong>

1. Place a referece to footable.editable.js under footable.js. 
2. Place a referece to footable.editable-1.0.css under footable-0.1.css.
3. If no dataHandlerURL is provided, the plugin operates in demo mode and posts alerts showing all JSON that would be transported between the client and the server. 

FooTables are defined just like always, but can include a few new FooTable.Editable classes and data attributes: 
```html
   <table id="tblFootable" class="footable">
      <thead>
        <tr>
          <!--class="fooId" marks this row as the table's primary key-->
          <!--Other Footable data attributes are used to hide the column from view-->
          <th class="fooId" data-hide="all" data-ignore="true">
            EmpId
          </th>
          <!--class="fooEditable" makes all the values in a column editable-->
          <th class="fooEditable" data-class="expand">
            First Name
          </th>
          <th class="fooEditable">
            Last Name
          </th>
          <th class="fooEditable" data-hide="phone,tablet">
            Job Title
          </th>
          <!--data-return-type="JSONDate" apply's a custom Parser to the value displayed-->
          <!--A JSON formatted date /Date(1234....)/ would be displayed in dd/mm/yy format-->
          <th class="fooEditable" data-hide="phone,tablet" data-return-type="JSONDate">
            DOB
          </th>
          <th class="fooEditable" data-hide="phone">
            Status
          </th>
		  <!--data-ft-buttons is used to create add and/or delete buttons for each record-->
		  <th data-ft-buttons="Add,Delete" data-hide="phone, tablet"></th>
        </tr>
      </thead>
      <tbody>
        <!--class="fooNewRecord" defines a blank row for new records at the end of the table-->
        <tr class="fooNewRecord">
          <td></td>
          <td>First</td>
          <td>Last</td>
          <td>Title</td>
          <td>DOB</td>
          <td>Active</td>
        <tr>
      </tbody>
    </table>
```

The FooTable.Editable plugin also includes a few new startup options:
```javascript
    $(function () {
        $('#tblFootable').footable({
            serverTableName: "Employees",             
            dataHandlerURL: "/FooTableHandler.ashx",   
            autoLoad: true                                                          
        });                                                 
    });                                                 
```

* <strong>serverTableName</strong> - The name of the server table you wish to update.
* <strong>dataHandlerURL</strong> - The URL for the generic handler processing requests.
* <strong>autoLoad</strong> - Sends a "Load" request to the server each time the footable is created.  The "Load" request fills up the table with data from the server. 

<h2 id="#ftEditableAPI">ftEditable API</h2>

The ftEditable API can be used for more advanced FooTable.Editable transactions and table manipulations.  All API functions can be accessed through the jQuery ftEditable function which returns the ftEditable object.  The following example demonstrates using the ftEditable function to delete all rows from a footable and add any new rows contained in a javascript Array of dataRecords.  

```javascript
    var ftEditable = $().ftEditable();
    ftEditable.deleteAllRows('#tblFootable');
    ftEditable.addRows('#tblFootable',dataRecords);
```

Using the ftEditable object, a "Load" request is sent to the sever.  If the request is successful, the server responds back with an array of JSON records containing data which is then loaded into the FooTable.  This is the exact same command issued when the autoLoad: option is set to true.

```javascript
    var ftEditable = $().ftEditable();
    ftEditable.processCommand('#tblFootable', 'Load');
```

<h2 id="#ftEditableAPIfunctions">ftEditable API Functions</h2>

This section is still under construction.  Additional details to follow!

<h3>processCommand(target, command)</h3>

<strong>target</strong> - The DOM element generating the command.  The target detemines what record / row in the table data has changed.  The target also determines what record / row will be impacted by any server responses.

<strong>command</strong> - Valid commands are "Load","Add","Update", and "Delete".  These commands determine what required data is packaged in the updateRecord and sent to the server. 

<strong>The processCommand() function generates a command from the client that:</strong>

1. Creates a valid updateRecord that will be sent to the server.
2. Calls the transportData() function sending the updateRecord to the server.
3. Any server resposnse is also processed by processServerResponse() function indirectly called by transportData().

<h3>transportData(target, updateRecord)</h3>

<strong>target</strong> - The DOM element generating the command.  The target detemines what record / row in the table data has changed.  The target also determines what record / row will be impacted by any server responses.

<strong>updateRecord</strong> - This is the valid JavaScript object containing the data transaction information that is transformed into a JSON string and then sent to the server via AJAX.  This object is typically created by the processCommand() function.

<strong>The transportData() function:</strong>

1.  Transforms the updateRecord into a JSON string.
2.  Sends the updateRecord to the handler URL specified by dataHandlerURL.
3.  Calls the processServerResponse() function to process the respsonse send by the server.

<h3>processServerResponse(target, data, updateRecord)</h3>

<h3>updateRow(row, rowData)</h3>

<h3>deleteRow(row)</h3>

<h3>deleteAllRows(table)</h3>

<h3>checkNewEmptyRecord(table)</h3>

<h3>addRows(table, tableRows)</h3>

