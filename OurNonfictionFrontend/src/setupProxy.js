const { createProxyMiddleware } = require('http-proxy-middleware')

const context = ['/bookingapi', '/roomapi', '/guestapi', '/account']

module.exports = function (app) {
  const appProxy = createProxyMiddleware(context, {
    target: 'https://localhost:7128',
    secure: false
  })

  app.use(appProxy)
}
