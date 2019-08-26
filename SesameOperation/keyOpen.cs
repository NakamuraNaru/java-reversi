using System;
//using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
using System.Net.Http;  //HttpClient���g�����߂ɕK�v?
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace SesameOperation
{

    //�ʃN���X
    //private HttpClient client = new HttpClient();  //���M�@�\
    public static class KeyStateChange
    {
        private const String url = "https://api.candyhouse.co/public";    //necessary format
        private const String api_key = "A9QDCiB-RMyOhunAvJLQFqOZW10ALdydLg42xoMySOUqhWTmrBU1fyAgRZoiQYFxCfducc2HEiWa";    //���i�ɂ���ă��j�[�N�Ȓl

        [FunctionName("KeyStateChange")]

        /*Sesame�̋@������擾����*/
        //public static async Task<HttpResponseMessage> receive_sesame_info(HttpClient client) {  //async�ɂ��Ă͌�ŏڂ���
        //    var request = new HttpRequestMessage(HttpMethod.Get, url + "/sesames");    //���b�Z�[�W(�v���N�G��)
        //    request.Headers.Add("Authorization", api_key);              //�w�b�_�[��ǉ�
        //    var response = await client.SendAsync(request);             //�񓯊������HTTP�v���𑗐M(await�͌��ʑ҂��̏�Ԃ�\���B���ʂ𓾂�ƈȍ~�̏������i�ށB)

        //    return response;
        //}

        ///*Sesame�̌���Ԃ��擾����*/
        //public HttpClient receive_sesame_state()
        //{

        //    return
        //}

        ///*Sesame�𑀍�(lock/unlock)����*/
        //public HttpClient receive_sesame_operate()
        //{

        //    return
        //}

        ///*Sesame�̃^�X�N�󋵂��擾����*/
        //public HttpClient receive_sesame_task_state()
        //{

        //    return
        //}

        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            //String[] id_array = new string[0]; //�f�o�C�XID�݂̂��i�[����ϒ��̈ꎟ���z��
            String device_id="";
            //API�L�[��p���āA�f�o�C�XID���擾����(��Ŋ֐��ɕ�����)
            var client = new HttpClient();  //���M�@�\
              var request = new HttpRequestMessage(HttpMethod.Get, url + "/sesames");    //���b�Z�[�W(�v���N�G��)
            request.Headers.Add("Authorization", api_key);              //�w�b�_�[��ǉ�
            var response = await client.SendAsync(request);             //�񓯊������HTTP�v���𑗐M(await�͌��ʑ҂��̏�Ԃ�\���B���ʂ𓾂�ƈȍ~�̏������i�ށB)

            //var response = await receive_sesame_info(client);

            /*�Z�T�~�̏��擾������*/
            if ((int)response.StatusCode == 200)
            {
                string response_body = await response.Content.ReadAsStringAsync();   //HTTP�ʐM�œ���body(device id, serial, nickname)���擾����
                //�Z�T�~�̃f�o�C�XID���擾����
                string temp = response_body.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "");   //�s�K�v�ȕ������폜
                String[] array = temp.Split(",");    //device id, serial, nickname�̕�������i�[���Ă���z��(�g���ɂ�split���K�v)
                device_id = array[0].Split(":")[1].Replace("\"", "").Replace(" ", "");  //�s�K�v�ȋ󔒂ƃ_�u���N�H�[�e�[�V���������������f�o�C�XID
                //�S�Z�T�~�̃f�o�C�XID���i�[����
                //int j = 0;
                //for (int i = 0; i < array.Length; i++)
                //{
                //    if (i % 3 == 0)
                //    {
                //        Array.Resize(ref id_array, id_array.Length + 1);    //�v�f���𑝂₷
                //        id_array[j] = array[i].Split(":")[1];
                //        j += 1;
                //    }
                //}
            }
            /*http�ʐM���s��*/
            else
            {
                return (ActionResult)new OkObjectResult($"Get Sesame informaition Failed");
            }

            string user_select = req.Query["state"];    //���[�U��URL�ɓ��͂����J���߂��擾����
            /*URL��lock��������unlock�����͂��ꂽ�ꍇ*/
            if (user_select == "lock" || user_select == "unlock")
            {
                //Sesame�̊J�� +�{��HTTP Client)
                var json = $"{{\"command\":\"{user_select}\"}}";
                var request2 = new HttpRequestMessage(HttpMethod.Post, url + $"/sesame/{device_id}");   //url(�v���N�G��)�̔��s
                request2.Content = new StringContent(json, Encoding.UTF8, "application/json");          //Content-Type+json�̑I��
                request2.Headers.Add("Authorization", api_key);                                         //�w�b�_�[�̎w��
                var response2 = await client.SendAsync(request2);                                       //�񓯊������HTTP�v���𑗐M(await�͌��ʑ҂��̏�Ԃ�\���B���ʂ𓾂�ƈȍ~�̏������i�ށB)
                string response_body = await response2.Content.ReadAsStringAsync();                     //HTTP�ʐM�œ���body(�^�X�NID)���擾����
                //Console.WriteLine(url + $"/sesame/{device_id}");
                //Console.WriteLine(response_body);
                //Console.WriteLine(response2);
                //Console.WriteLine(json);
                /*lock,unlock�̗v�����ʂ�����*/
                if ((int)response2.StatusCode == 200)
                {
                    if (user_select == "lock")
                    {
                        return (ActionResult)new OkObjectResult($"Key {user_select} complete");

                    }
                    else if (user_select == "unlock")
                    {
                        return (ActionResult)new OkObjectResult($"Key {user_select} complete");
                    }
                }
                else {
                    return (ActionResult)new OkObjectResult($"{response_body}");
                }
            }
            //lock,unlock�̂ǂ������������Ȃ��ꍇ
            return new BadRequestObjectResult("Please pass a operation on the query string or in the request body");
        }
    }
}
