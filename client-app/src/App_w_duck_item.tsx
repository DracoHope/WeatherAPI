/*
Section 3
For this simple client web page we do not need all this libraries for now

import { useState } from "react";
import reactLogo from "./assets/react.svg";
import viteLogo from "/vite.svg";
*/
import "./App.css";
import DuckItem from "./DuckItem";
import { ducks } from "./demo";

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
  return (
    <div>
      <h1>Weather API Application</h1>
      {ducks.map((duck) => (
        <DuckItem key={duck.name} duck={duck} />
        // <div key={duck.name}>
        //   <span>{duck.name}</span>
        //   <button onClick={() => alert(duck.makeSound(duck.name + "quak"))}>
        //     Make Sound
        //   </button>
        // </div>
      ))}
    </div>
  );
}

export default App;
