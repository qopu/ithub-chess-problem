import routes from '@configs/routes';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';

function App() {
    const router = createBrowserRouter(routes);

    return <RouterProvider router={router} />;
}

export default App;
