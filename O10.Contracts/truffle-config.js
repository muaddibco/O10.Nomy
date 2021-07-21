const HDWalletProvider = require('@truffle/hdwallet-provider');

const fs = require('fs');
const mnemonic = fs.readFileSync(".secret").toString().trim();

module.exports = {
  networks: {
    development: {
     host: "127.0.0.1",
     port: 8545,
     network_id: "*"
    },
    rinkeby: {
        provider: function() { 
         return new HDWalletProvider(mnemonic, "https://rinkeby.infura.io/v3/d0885183e6324ff59afba069175ee4cf");
        },
        network_id: 4,
        gas: 4500000,
        gasPrice: 10000000000,
    }
   },  
  compilers: {
    solc: {
      version: "0.5.2",
    }
  }
}