const { createProxyMiddleware } = require('http-proxy-middleware')

const context = ['/bookingapi', '/roomapi', '/guestapi', '/account']

module.exports = function (app) {
  const appProxy = createProxyMiddleware(context, {
    target: 'https://nonfiction-backend.herokuapp.com/',
    secure: false,
    changeOrigin: true
  })

  app.use(appProxy)
}
