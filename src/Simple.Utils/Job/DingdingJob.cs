﻿using Simple.Core.Jobs;
using Simple.Utils.Domain.Queues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Core.Extensions;
using Simple.Core.Encryption;
using Simple.Core.Helper;
using Newtonsoft.Json;
using Simple.Core.Domain;
using Simple.Utils.Domain.Model;

namespace Simple.Utils.Job
{
    public class DingdingJob : JobServiceBase
    {
        public override int Time => 1000;

        public override void Invoke()
        {
            if (DingdingQueue.Queue.Count == 0) return;
            DingdingModel msg = DingdingQueue.Queue.Dequeue();
            long timestamp = WebAgent.GetTimestamps();
            string message = $"{timestamp}\n{msg.Secret}";
            string sign = SHA256Encryption.HMACSHA256(message, msg.Secret);
            string api = $"https://oapi.dingtalk.com/robot/send?access_token={msg.Access_Token}&timestamp={timestamp}&sign={sign}";
            string response = string.Empty;
            object? json = null;
            try
            {
                switch (msg.MsgType)
                {
                    case "text":
                        json = new { msgtype = "text", text = new { content = msg.Text } };
                        break;
                    case "link":
                        json = new
                        {
                            msgtype = "link",
                            link = new
                            {
                                text = msg.Text,
                                title = msg.Title,
                                picUrl = msg.PicUrl,
                                messageUrl = msg.MessageUrl,
                            }
                        };
                        break;
                    case "markdown":
                        json = new
                        {
                            msgtype = "markdown",
                            markdown = new
                            {
                                text = msg.Text,
                                title = msg.Title,
                            }
                        };
                        break;
                    case "actionCard":
                        json = new
                        {
                            msgtype = "actionCard",
                            actionCard = new
                            {
                                title = msg.Title,
                                text = msg.Text,
                                btnOrientation = msg.BtnOrientation,
                            }
                        };
                        break;
                }
                if (json != null)
                {
                    response = NetHelper.Post(api, JsonConvert.SerializeObject(json));
                    Console.WriteLine(response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now}]发送错误 \n {ex.Message} \n {response}");
            }

        }
    }
}
