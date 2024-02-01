/*
    Importing the Duck into this function
    Create an interface with the data type Duck from the demo.ts
*/
import { Duck } from "./demo";

/*
    We be passing down the <duck> to the child component
*/
interface Props {
  duck: Duck;
}

/* 
    We need to pass the <props> into this function so that we can process the data and pass it to the child component. 
    A child component is one who call for this React Component
    We also need to define what datatype actually is the <props>?
*/
export default function DuckItem({ duck }: Props) {
  return (
    <div key={duck.name}>
      <span>{duck.name}</span>
      <button onClick={() => alert(duck.makeSound(duck.name + "quak"))}>
        Make Sound
      </button>
    </div>
  );
}
