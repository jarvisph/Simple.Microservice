using Microsoft.AspNetCore.Mvc;
using Simple.Chain;
using Simple.Chain.Protocol;
using Simple.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Utils.Controllers
{
    [Route("[controller]/[action]")]
    public class ChainController : SimpleControllerBase
    {
        [HttpGet]
        public ActionResult GetBalance([FromQuery] string address, [FromQuery] string token)
        {
            var wallet = WalletProvider.GetWalletClient("tron", "rpc://grpc.trongrid.io:50051");
            decimal balance = 0;
            switch (token)
            {
                case "USDT":
                    balance = wallet.GetBalance(address, "TR7NHqjeKQxGTCi8q8ZY4pL8otSzgjLj6t");
                    break;
                default:
                    balance = wallet.GetBalance(address);
                    break;
            }
            return JsonResult(balance);
        }

        [HttpPost]
        public async Task<ActionResult> Transfer([FromForm] string privateKey, [FromForm] string to_address, [FromForm] string token, [FromForm] decimal amount)
        {
            var wallet = WalletProvider.GetWalletClient("tron", "rpc://grpc.trongrid.io:50051");
            string hax = string.Empty;
            switch (token)
            {
                case "USDT":
                    hax = await wallet.TransferAsync(privateKey, to_address, "TR7NHqjeKQxGTCi8q8ZY4pL8otSzgjLj6t", amount);
                    break;
                default:
                    hax = await wallet.TransferAsync(privateKey, to_address, amount);
                    break;
            }
            return JsonResult(hax);
        }

        [HttpGet]
        public async Task<ActionResult> TransferInfo([FromQuery] string txId)
        {
            var wallet = WalletProvider.GetWalletClient("tron", "rpc://grpc.trongrid.io:50051");
            var info = await wallet.GetTransactionInfoAsync(txId);
            return JsonResult(info);
        }
    }
}
