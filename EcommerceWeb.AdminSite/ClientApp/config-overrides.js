const { alias } = require("react-app-rewire-alias");

module.exports = function override(config) {
  alias({
    "@layout": "src/layout",
    "@components": "src/components",
    "@asset": "src/asset",
    "@pages": "src/pages",
    "@router": "src/router",
    "@api": "src/api",
  })(config);
  return config;
};
