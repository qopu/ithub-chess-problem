import Game from '@components/Game';
import type { RouteObject } from 'react-router-dom';

const routes: Array<RouteObject> = [
    {
        path: '/',
        element: <Game />
    },
];

export default routes;
