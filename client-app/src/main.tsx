import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";
/* 
    Import Semantic React UI
    Place this import Sematic React UI on top of our "./index.css" because we may want to have owe customization to be apply and override some of the Sematic React UI CSS
*/
// Import all Semantic React UI CSS
import "semantic-ui-css/semantic.min.css";
// Import the Semantic UI [Header] to replace the <h1></h1> element
// import { Header } from "semantic-ui-react";

import "./index.css";

/*
    We be using the Semantic React CSS for the styling
    https://react.semantic-ui.com/usage

    Take note: 
    Their version 2.0 does not support <React.StrictMode> Mode.
    But the version 3.0 does support <React.StrictMode> mode. WE must install the version 3.0 or above

    Command to install SEmantic React CSS
    npm install semantic-ui-react semantic-ui-css
    But we need to specify the version to be 3.0.0
    npm install semantic-ui-react@3.0.0-beta.2 semantic-ui-css

    How to import Semantic React CSS into our project?
    using => import 'semantic-ui-css/semantic.min.css'

    Description for Semantic React UI
    The Semantic UI CSS package is automatically synced with the main Semantic UI repository to provide a lightweight CSS only version of Semantic UI.
    If you are using TypeScript, you don't need to install anything, typings are included with the package.
    Basically this means it will support Typescript for React
*/

/*
    There are many feature can be use in Semantic UI suchh as Button, Form, Icon Header ...etc
    The documentation is quite clear for such styling.

    We be using Icon Header and List Option (Displaying the Activity), 
*/

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <App />
  </React.StrictMode>
);
