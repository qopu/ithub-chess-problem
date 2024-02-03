import react from '@vitejs/plugin-react';
import * as path from 'node:path';
import { defineConfig, loadEnv } from 'vite';

// https://vitejs.dev/config/
export default defineConfig(({ mode }) => {
    const env = loadEnv(mode, process.cwd());

    return {
        plugins: [react()],
        resolve: {
            alias: {
                '@assets': path.resolve(__dirname, './src/assets'),
                '@components': path.resolve(__dirname, './src/components'),
                '@configs': path.resolve(__dirname, './src/configs'),
                '@contexts': path.resolve(__dirname, './src/contexts'),
                '@interfaces': path.resolve(__dirname, './src/interfaces'),
                '@pages': path.resolve(__dirname, './src/pages'),
                '@services': path.resolve(__dirname, './src/services'),
                '@stores': path.resolve(__dirname, './src/stores'),
                '@hooks': path.resolve(__dirname, './src/hooks'),
                '@utils': path.resolve(__dirname, './src/utils'),
            },
        },
    };
});
