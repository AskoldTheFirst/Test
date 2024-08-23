import ReactDOM from 'react-dom/client'
import './styles.css'

import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import { RouterProvider } from 'react-router-dom';
import { router } from './Routes.tsx';
import { Provider } from 'react-redux';
import { store } from './configureStore.ts';


ReactDOM.createRoot(document.getElementById('root')!).render(

  <Provider store={store}>
    <RouterProvider router={router} />
  </Provider>
  
)
