import React from 'react';
import ReactDOM from 'react-dom';
import App from './components/App.jsx';

import './styles.css';

ReactDOM.render(<App server='https://localhost:44366' />, document.getElementById('app'));
