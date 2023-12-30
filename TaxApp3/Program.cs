using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Clients;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Abstraction.Cryptography;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Algorithms;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Dto;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Factories;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Models;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Properties;
using TaxCollectDataWithPrivateKeyAndCertificateKey.Library.Providers;
using static System.Console;
using Newtonsoft.Json;


namespace TaxApp3
{
    public static class TaxClient
    {
        private const string MemoryId = "A2G781";
        private const string ApiUrl = "https://tp.tax.gov.ir/requestsmanager";
        private static string PrivateKeyPath = Environment.CurrentDirectory + "\\Files\\Private.txt";
        private static string PublicKeyPath = Environment.CurrentDirectory + "\\Files\\Public.txt";
        private static string CertificatePath = Environment.CurrentDirectory + "\\Files\\CSR.txt";

        public static void Main()
        {
            WriteLine("Load Tax Api Settings...");
            ITaxApi taxApi = CreateTaxApi();
            WriteLine("Loading Valid Invoices ...");
            InvoiceDto validInvoice = CreateValidInvoice();
            WriteLine(JsonConvert.SerializeObject(validInvoice, Formatting.Indented));
            WriteLine("Loading Invalid Invoices...");
            InvoiceDto invalidInvoice = CreateInvalidInvoice();
            WriteLine(JsonConvert.SerializeObject(invalidInvoice, Formatting.Indented));
            WriteLine("Sendig Invoices ...");
            List<InvoiceDto> invoiceList = new List<InvoiceDto>()
            {
             validInvoice,
             invalidInvoice
            };
            List<InvoiceResponseModel> responseModels = new List<InvoiceResponseModel>();
            responseModels = taxApi.SendInvoices(invoiceList);
            WriteLine("Please wait...");
            Thread.Sleep(10_000);
            WriteLine("Inquery DTOs...");
            InquiryByReferenceNumberDto inquiryDto = new InquiryByReferenceNumberDto(responseModels.Select(r => r.ReferenceNumber).ToList());
            WriteLine("Inquery By RefrenceId ...");
            List<InquiryResultModel> inquiryResults = taxApi.InquiryByReferenceId(inquiryDto);
            WriteLine("Printing Results...");
            PrintInquiryResult(inquiryResults);
        }
        private static void PrintInquiryResult(List<InquiryResultModel> inquiryResults)
        {
            foreach (var result in inquiryResults)
            {
                Console.ForegroundColor = ConsoleColor.White;
                WriteLine("========================================");
                if (result.Status.ToUpper() == "PENDING")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else if (result.Status.ToUpper() == "FAILED")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (result.Status.ToUpper() == "SUCCESS")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (result.Status.ToUpper() == "TIMEOUT")
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                WriteLine("Status = " + result.Status);
                WriteLine("ReferenceId = " + result.ReferenceNumber);
                WriteLine("Errors:");
                var errors = result.Data.Error;
                foreach (var error in errors)
                {
                    var code = error.Code;
                    var message = error.Message;
                    WriteLine("Code: " + code + ", Message: " + message);
                }
                WriteLine("Warnings:");
                var warnings = result.Data.Warning;
                foreach (var warning in warnings)
                {
                    var code = warning.Code;
                    var message = warning.Message;
                    WriteLine("Code: " + code + ", Message: " + message);
                }
            }
        }
        private static ITaxApi CreateTaxApi()
        {
            Pkcs8SignatoryFactory pkcs8SignatoryFactory = new Pkcs8SignatoryFactory();
            EncryptorFactory encryptorFactory = new EncryptorFactory();
            TaxProperties properties = new TaxProperties(MemoryId);
            TaxApiFactory taxApiFactory = new TaxApiFactory(ApiUrl, properties);
            ISignatory signatory = pkcs8SignatoryFactory.Create(PrivateKeyPath, CertificatePath);
            ITaxPublicApi publicApi = taxApiFactory.CreatePublicApi(signatory);
            IEncryptor encryptor = encryptorFactory.Create(publicApi);
            return taxApiFactory.CreateApi(signatory, encryptor);
        }
        private static InvoiceDto CreateValidInvoice()
        {
            Random random = new Random();
            long serial = random.NextInt64(1_000_000_000);
            DateTime now = DateTime.Now;
            string taxId = GenerateTaxId(serial, now);
            string inno = serial.ToString("X").PadLeft(10, '0');
            long indatim = new DateTimeOffset(now).ToUnixTimeMilliseconds();
            InvoiceDto invoice = new InvoiceDto()
            {
                Header = new HeaderDto()
                {
                    taxid = taxId,
                    inno = inno,
                    indatim = indatim,
                    inty = 1,
                    inp = 1,
                    ins = 1,
                    tins = "14003778990",
                    tinb = "10100302746",
                    tprdis = 20_000,
                    tdis = 500,
                    tadis = 19_500,
                    tvam = 1_755,
                    todam = 0,
                    tbill = 21_255,
                    setm = 2
                },
                Body = new List<BodyItemDto>()
                {
                    new()
                    {
                        sstid = "2710000138624",
                        sstt = "سازی فوالد صنعت قطعات سرسیلندر",
                        mu = "164",
                        am = 2,
                        fee = 10_000,
                        prdis = 20_000,
                        dis = 500,
                        adis = 19_500,
                        vra = 9,
                        vam = 1_755,
                        tsstam = 21255
                    }
                }
            };
            return invoice;
        }
        private static InvoiceDto CreateInvalidInvoice()
        {
            Random random = new Random();
            long serial = random.NextInt64(1_000_000_000);
            DateTime now = DateTime.Now;
            string taxId = GenerateTaxId(serial, now);
            string inno = serial.ToString("X").PadLeft(10, '0');
            long indatim = new DateTimeOffset(now).ToUnixTimeMilliseconds();

            InvoiceDto invoice = new InvoiceDto()
            {
                Header = new HeaderDto()
                {
                    taxid = taxId,
                    inno = inno,
                    indatim = indatim,
                    inty = 1,
                    inp = 7,
                    ins = 1,
                    tins = "14003778990",
                    tinb = "10100302746",
                    tprdis = 30_000,
                    tdis = 500,
                    tadis = 19_500,
                    tvam = 1_765,
                    todam = 1_000,
                    tbill = 21_255,
                    setm = 3
                },
                Body = new List<BodyItemDto>()
                {
                     new()
                     {
                         sstid = "1710000138624",
                         sstt = "اشتباه کاالی",
                         mu = "164",
                         am = 2,
                         fee = 10_000,
                         prdis = 20_000,
                         dis = 0,
                         adis = 19_500,
                         vra = 10,
                         vam = 0,
                         tsstam = 20_000
                     }
                }
            };
            return invoice;
        }
        private static string GenerateTaxId(long serial, DateTime now)
        {
            TaxIdProvider taxIdProvider = new TaxIdProvider(new
           VerhoeffAlgorithm());
            return taxIdProvider.GenerateTaxId(MemoryId, serial, now);
        }
    }

}

