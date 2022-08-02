const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    "/booking","/room","/guest","/available"
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'https://localhost:7128',
        secure: false
    });

    app.use(appProxy);
};
