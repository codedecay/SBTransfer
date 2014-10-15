namespace ClientAPI
    {
        public class DefaultSettings
            {
                public static ClientAPISettings settings()
                {
                    var clientApiSettings = new ClientAPISettings();
                    //clientApiSettings.HTTPS_ADDRESS_FOR_FILE_TRANSFER = "localhost";
                    var checksumPillars = new System.Collections.Specialized.StringCollection { "checksum2" };
                    var pillars = new System.Collections.Specialized.StringCollection();
                    //pillars.Add("sbtape2");
                    //pillars.Add("reference2");
                    pillars.Add("jonas-testpillar");
                    pillars.Add("kims-testpillar");
                    //pillars.Add("kbpillar2");
                    clientApiSettings.CHECKSUMPILLAR_IDS = pillars;
                    clientApiSettings.CLIENT_ID = "Jonas-client";
                    clientApiSettings.CLIENT_QUEUE = @"queue://jonas_test_client";
                    //b.CLIENT_TOPIC_QUEUE = "topic://sa_test_client";
                    clientApiSettings.COLLECTION_DESTINATION = "topic://integrationtest1";
                    clientApiSettings.COLLECTION_ID = "integrationtest1";
                    //b.COLLECTION_SETTINGS_XSD_FILE_PATH = @"C:\BitMagStuff\xsdFiles\bitrepository-repository-settings-10\xsd\RepositorySettings.xsd";
                    //b.COMMON_QUEUE = "";
                    clientApiSettings.DEFAULT_CHECKSUM_TYPE = "MD5";
                    clientApiSettings.IDENTIFICATION_TIMEOUT = 7000;
                    clientApiSettings.MAX_EXECUTING_ITEMS = 10;
                    clientApiSettings.MAX_NUMBER_OF_REQUEST_RETRIES = 3;
                    clientApiSettings.MESSAGE_BUS_CONFIGURATION_NAME = "Integration Test Broker Network A";
                    clientApiSettings.MESSAGE_XSD_FILE_PATH = @"D:\bitrepository-message-xml-24\xsd\BitRepositoryMessages.xsd";
                    clientApiSettings.PILLAR_IDS = pillars;
                    const int port = 443;
                    //clientApiSettings.ACTIVEMQ_BROKER = @"ssl://localhost:61616?transport.clientcertfilename=D:/Downloads/Apache/apache-activemq-5.5.1/conf/client.cer&transport.acceptInvalidBrokerCert=true"; // grøntnet
                    clientApiSettings.ACTIVEMQ_BROKER = @"ssl://217.198.211.150:61616?transport.clientcertfilename=D:/Downloads/Apache/apache-activemq-5.5.1/conf/client.cer&transport.acceptInvalidBrokerCert=true";
                    //clientApiSettings.ACTIVEMQ_BROKER = @"tcp://localhost:61617";
                    clientApiSettings.TIME_TO_WAIT_AFTER_TIMEOUT = 10000;
                    clientApiSettings.VALID_FILENAME_REGEX = @"[a-zA-Z_\.\-0-9]+";
                    clientApiSettings.XML_NAMESPACE = "http://bitrepository.org/BitRepositoryMessages.xsd";
                    clientApiSettings.XSD_MINIMUM_VERSION = "24";
                    clientApiSettings.XSD_VERSION = "24";
                    clientApiSettings.OPERATION_TIMEOUT = 120000; // 2 min.  
                    clientApiSettings.PRIVATE_CERTIFICATE_THUMBPRINT = "B60843F8A3BFBD2944EAF9043D9C2443B936A981";
                    clientApiSettings.PUBLIC_CERTIFICATE_THUMBPRINT = "B60843F8A3BFBD2944EAF9043D9C2443B936A981";
                    clientApiSettings.USER_CERTIFICATES_STORE = "My";
                    clientApiSettings.WEBDAV_HTTP_PORT = port;
                    clientApiSettings.WEBDAV_BASEFOLDERNAME = "dav";
                    clientApiSettings.WEBDAV_URI_SCHEME = "HTTPS";
                    clientApiSettings.WEBDAV_IP_ADDRESS = "217.198.211.150";
                    clientApiSettings.WEBDAV_CLIENT_CERTIFICATE_THUMBPRINT = "147cafe2c7d0b07d3a61653977a87ff7fe91f5c6";
                    return clientApiSettings;
                }
            }
    }