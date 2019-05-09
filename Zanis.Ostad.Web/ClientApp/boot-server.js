const prerendering = require('aspnet-prerendering')
process.env.VUE_ENV = 'server';
const bundleJson = require('../wwwroot/dist/vue-ssr-server-bundle.json')
const bundleRenderer = require('vue-server-renderer').createBundleRenderer(bundleJson)
module.exports = prerendering.createServerRenderer(function (params) {
  return new Promise(function (resolve, reject) {
    let context = {
      url: params.url,
      absoluteUrl: params.absoluteUrl,
      baseUrl: params.baseUrl,
      data: params.data,
      domainTasks: params.domainTasks,
      location: params.location,
      origin: params.origin,
    };
    bundleRenderer.renderToString(context, (err, resultHtml) => {
      if (err) {
        reject(err.message);
      }
      resolve({
        html: resultHtml,
      });
    })
  })
});
