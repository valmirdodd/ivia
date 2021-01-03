import './App.css';
import { Home } from './components/Home';
import { Agendamento } from './components/Agendamento';
import {InserirAgendamento} from './components/InserirAgendamento';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import { Navigation } from './components/Navigation';

function App() {
  return (
    <BrowserRouter>
      <div className="container">

        <h3 className="m-3 d-flex justify-content-center">IVIA</h3>

        <Navigation />

        <Switch>
          <Route path='/' component={Home} exact />
          <Route path='/home' component={Home} />
          <Route path='/agendamento' component={Agendamento} />
        </Switch>
      </div>
    </BrowserRouter>
  );
}

export default App;
