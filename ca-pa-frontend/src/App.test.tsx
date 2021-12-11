import React from 'react';
import { render, screen } from '@testing-library/react';
import App from './App';
import { Provider } from 'react-redux'
import { BrowserRouter as Router } from 'react-router-dom'
import store from './store/store';

test('renders App without crashing', () => {
  const app = (
    <Provider store={store}>
      <Router>
        <App />
      </Router>
    </Provider>
  )
  render(app);
});
