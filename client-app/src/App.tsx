/*
Section 3
For this simple client web page we do not need all this libraries for now

import { useState } from "react";
import reactLogo from "./assets/react.svg";
import viteLogo from "/vite.svg";
*/
/*
  Need to install Axios => Promise based HTTP client for the browser and node.js
  https://axios-http.com/docs/intro
  npm install axios

  Axios is better than the builtin NodeJS HTTP Request package for example the <fethch()> function.

  Bu using Axios will help in reducing the amount of coding for HTTP Request as well.

  The [Interceptors] is one of the useful feature which will be using for this project.
  The function of [Interceptor]
  intercept requests or responses before they are handled by the promise (then or catch)

  We will be installing the Axios package into the <Client-app> folder which will be doing the HTTP request. By doing so , the Axios package will not be installed in the main <weatherAPI> project folder
  1. Goto the <client-app> folder
  2. execute npm install axios command
*/

/*
  Summary description of how this <App></App> React Component work with the application
  1. When the application start, the <main.tsx> will execute first and will call for this <App> React Component
  2. When this <App> Function is executing, it will sent a HTTP request to fetch data and pass the data to the React component. This <App> compinent will save those data in the component.
  3. The data will be save in the form of React State. The component uses the React Hook which is "useState()" to save these data. Each of every React component can have its own state.
  4. Finally the browser will display the element code in JSX

*/
import { useEffect, useState } from "react";
import "./App.css";
import axios from "axios";
// Import all Semantic React UI CSS
import "semantic-ui-css/semantic.min.css";
/* 
  Import the Semantic UI [Header Icon] to replace the HTML header element such as <h6> to <h6> element and can insert an image as well
*/
import { Header, List } from "semantic-ui-react";

function App() {
  // The original React Hook will be disable for now
  // const [count, setCount] = useState(0);

  /*
      Similar to normal React App, this Function component will return the JSX or TSX for this case to the call application and display onto the web broswer.

      Take note:
      All variable such as number and object such as list must be within a pair of curly braces.

      React can only return one conponent.
      Therefore need to place all the JSX code under within a <div></div>
  */

  /*
    Using the React "useState" Hook for getting the data from the "state"

    Once this <App> React App component is been loaded, It will make a HTTP request and save the data into the React "state". Then we can retrieve those data using the "useState()" function and assign those data to our declared variable.

    Making a HTTP request to get data from <API> app
    1. We will declare 2 variable [activities, setActivities] and create a "useState([])" with an empty array. Take note: The variable [setActivities] is actually a method function name. The function name can be of any name actually.
    2. Create an "setEffect()" which will be executed when this <App> component is loaded 
  */

  const [activities, setActivitiesData] = useState([]);

  /*
    This <useEffect(f=callback function)> require a callback function as the parameter.
    We will be making a Get Http Request to the Database
    Reacll that is we enter "http://localhost:5000/api/activities", this will retrieve all the available data in the SQLite database.

    1. The [axios.get("http://localhost:5000/api/activities");] will return a promise.
    The promise is the "response" from the HTTP request.

    2. We need to follow up with [.then()] function to capture this "response" and process this "response" by any method we want.

    3. We will be using the declared function name[setActivitiesData] to process the response and get the data objest out from the "response". 

    5. [setActivities(response.data)] => this actually means the Axios will get the response and place into its "data" axios object then assign this "data" object to the variable [setActivities]

    6. Finally we can use trhese "Activities" to set the data value of our component.

    Important:
    Notice the [useEffect()] actually need to have 2 input parameter.
    The first parameter is the [.then()] function which will process the "promise"
    The second parameter is also require know as the "Dependency" for this [useEffect()] function. By placing an "[]" empty array as the parameter, this means this [useEffect()] function will only execute once only when its loaded.
    But this "Dependancy" can be change to some other value so that this "useEffect()" function will automatically execute again whenever there's changes in this "Dependency" 

    Finally in the <App> comppnent return code, we can use the [Activities] to populate the data on the browser.

    We be using the [map()] function to loop through our list of data. the [activities] is the variable we declare here and the [activity] is the individual object in the [activities] list.
  */
  /*
  useEffect(() => {
    axios.get("http://localhost:5000/api/activities").then((response) => {
      setActivitiesData(response.data);
    });
  }, []);
*/
  /*
    For some unknown reason our End Point URL is not the same as the Tutor version 
    Tutor version:
    "http://localhost:5000/api/activities"
    Our working version:
    http://localhost:5000/Activities
*/
  /*
    Function description for React <useEffect()> function
    When Strict Mode is on, React will run one extra development-only setup+cleanup cycle before the first real setup. This is a stress-test that ensures that your cleanup logic “mirrors” your setup logic and that it stops or undoes whatever the setup is doing. If this causes a problem, implement the cleanup function.
    Basically it mean the React <useEffect()> function will execute twice instead of once when the <React Strict Mode> is On.
      useEffect(() => {
        axios.get("http://localhost:5000/Activities").then((response) => {
          setActivitiesData(response.data);
        });
      }, []);
    For our case, there will have 2 HTTP been send instead of one request when this React <App> component been loaded.
    We can monitor this by using the console.log() to verify
    {data: Array(10), status: 200, statusText: 'OK', headers: AxiosHeaders, config: {…}, …}
    {data: Array(10), status: 200, statusText: 'OK', headers: AxiosHeaders, config: {…}, …}

    From the console log, we can see there are two [response] been loghed twice instead of once. This means two HTTP request hav ebeen send hence we received two [response]

    If we going to remove the React Stricy Mode <React.StrictMode> from the <main.tsx> then we only see only one [response] been logged when we refresh the web page.
*/

  useEffect(() => {
    axios.get("http://localhost:5000/Activities").then((response) => {
      console.log(response);
      setActivitiesData(response.data);
    });
  }, []);

  /*
    We encounter the error:
    'http://localhost:5000/api/activities' from origin 'http://localhost:3000' has been blocked by CORS policy: No 'Access-Control-Allow-Origin' header is present on the requested resource.
    This is a CORS policy error which complaint about missing "CORS" header from our reqest original from http://localhost:3000 that's where the client app is call from. But the response is not same as trust origin application port hence need the CORS header.
    
    We can check for this "CORS" header in the "Network" tag from the browser dev tool. checking the [activities] request, no CORS header was found.

    We need to add the "CORS" policy into the <API> application in our project as follow:
    builder.Services.AddCors(opt => {
    opt.AddPolicy("CorsPolicy", policy => {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000/");
    });
    Basically we added a service to the API project to tell the <API> that don't care about CORS Header and Method as long as the HTTP request comes from  WithOrigins("http://localhost:3000/");
});
  */

  /*
    There is actually a warning from the Typescript regarding the expecting datatype of the [activity] object => (activity: any)
    Typescript doesn't like "any" datatype, but the program won't just breakdown hence only give warning. This happen because we are using Typescript strict mode. There is way to disable this Typescript strict mode but its not necessary to do so since it good to have warnong and error whenever there's something not quite right and we are alert on it
*/

  /* 
  Import the Semantic UI [Header Icon] to replace the HTML header element such as <h6> to <h6> element and can insert an image as well. 
  Reference for Semantic UI => <Header> </Header> component
  https://react.semantic-ui.com/elements/header/

  Header properties reference
  https://react.semantic-ui.com/elements/header/
  [as] keyword  => {elementType}	
                  An element type to render as (string or function). Basically can represent a [h1 to h1] HTML element or a React componengt as well as a function.
  [icon] keyword => Add an icon by icon name or pass an Icon.       

  Example replacinh HTML any <h1 to h6></h1 to h6>:
  Repolacing <h1>Weather API Application</h1> 
             with
              <Header as="h2" icon="users">
                Weather API Application
              </Header>

  Replacing HTML Listing <ul></ul> with Semantic UI <List> component as follow:
  Replacing HTML listing
  <ul>
        {activities.map((activity: any) => (
          <li key={activity.id}>{activity.title}</li>
        ))}
  </ul>
  with


*/

  return (
    <div>
      <Header as="h2" icon="users" content="Weather API Application" />
      <List>
        {activities.map((activity: any) => (
          <List.Item key={activity.id}>{activity.title}</List.Item>
        ))}
        <List.Item>End of Semantic UI Items</List.Item>
      </List>
    </div>
  );
}

export default App;
