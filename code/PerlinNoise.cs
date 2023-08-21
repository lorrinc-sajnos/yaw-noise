namespace yawNoise
{
    public class PerlinNoise : INoise, IHaveFreq, IFloatFunc2D, IFloatFunc3D, IAsByte
    {
        #region Fields
        public int Seed { get; private set; }
        public float Frequency { get; private set; }


        private const int xPrime = 353594023;
        private const int yPrime = 1465077127;
        private const int zPrime = 2082801727;
        private const int wPrime = 580550587;

        public static readonly float[] UNIT_2D_X = new float[] {

        1f,     0.9999953f,     0.9999812f,     0.9999576f,
        0.9999247f,     0.9998823f,     0.9998306f,     0.9997694f,
        0.9996988f,     0.9996188f,     0.9995294f,     0.9994306f,
        0.9993224f,     0.9992048f,     0.9990777f,     0.9989413f,
        0.9987954f,     0.9986402f,     0.9984756f,     0.9983016f,
        0.9981181f,     0.9979253f,     0.997723f,      0.9975114f,
        0.9972904f,     0.9970601f,     0.9968203f,     0.9965711f,
        0.9963126f,     0.9960447f,     0.9957674f,     0.9954808f,
        0.9951847f,     0.9948793f,     0.9945646f,     0.9942405f,
        0.993907f,      0.9935641f,     0.9932119f,     0.9928504f,
        0.9924796f,     0.9920993f,     0.9917098f,     0.9913108f,
        0.9909027f,     0.9904851f,     0.9900582f,     0.989622f,
        0.9891765f,     0.9887217f,     0.9882576f,     0.9877841f,
        0.9873014f,     0.9868094f,     0.9863081f,     0.9857975f,
        0.9852777f,     0.9847485f,     0.9842101f,     0.9836624f,
        0.9831055f,     0.9825393f,     0.9819639f,     0.9813792f,
        0.9807853f,     0.9801821f,     0.9795698f,     0.9789482f,
        0.9783174f,     0.9776773f,     0.9770281f,     0.9763697f,
        0.9757021f,     0.9750254f,     0.9743394f,     0.9736443f,
        0.97294f,       0.9722265f,     0.9715039f,     0.9707721f,
        0.9700313f,     0.9692813f,     0.9685221f,     0.9677538f,
        0.9669765f,     0.96619f,       0.9653944f,     0.9645898f,
        0.9637761f,     0.9629533f,     0.9621214f,     0.9612805f,
        0.9604305f,     0.9595715f,     0.9587035f,     0.9578264f,
        0.9569404f,     0.9560453f,     0.9551412f,     0.9542281f,
        0.953306f,      0.952375f,      0.951435f,      0.9504861f,
        0.9495282f,     0.9485614f,     0.9475856f,     0.9466009f,
        0.9456073f,     0.9446048f,     0.9435934f,     0.9425732f,
        0.9415441f,     0.940506f,      0.9394592f,     0.9384035f,
        0.937339f,      0.9362656f,     0.9351835f,     0.9340925f,
        0.9329928f,     0.9318843f,     0.9307669f,     0.9296409f,
        0.9285061f,     0.9273625f,     0.9262102f,     0.9250492f,
        0.9238795f,     0.9227011f,     0.921514f,      0.9203183f,
        0.9191139f,     0.9179008f,     0.9166791f,     0.9154487f,
        0.9142098f,     0.9129622f,     0.911706f,      0.9104413f,
        0.909168f,      0.9078861f,     0.9065957f,     0.9052967f,
        0.9039893f,     0.9026733f,     0.9013488f,     0.9000159f,
        0.8986745f,     0.8973246f,     0.8959662f,     0.8945995f,
        0.8932243f,     0.8918407f,     0.8904487f,     0.8890483f,
        0.8876396f,     0.8862225f,     0.8847971f,     0.8833634f,
        0.8819213f,     0.8804709f,     0.8790122f,     0.8775453f,
        0.8760701f,     0.8745866f,     0.873095f,      0.8715951f,
        0.870087f,      0.8685707f,     0.8670462f,     0.8655136f,
        0.8639728f,     0.862424f,      0.860867f,      0.8593018f,
        0.8577286f,     0.8561473f,     0.854558f,      0.8529606f,
        0.8513552f,     0.8497418f,     0.8481203f,     0.8464909f,
        0.8448536f,     0.8432083f,     0.841555f,      0.8398938f,
        0.8382247f,     0.8365477f,     0.8348629f,     0.8331702f,
        0.8314696f,     0.8297612f,     0.8280451f,     0.8263211f,
        0.8245893f,     0.8228498f,     0.8211025f,     0.8193475f,
        0.8175848f,     0.8158144f,     0.8140363f,     0.8122506f,
        0.8104572f,     0.8086562f,     0.8068476f,     0.8050314f,
        0.8032075f,     0.8013762f,     0.7995372f,     0.7976909f,
        0.7958369f,     0.7939755f,     0.7921066f,     0.7902302f,
        0.7883464f,     0.7864552f,     0.7845566f,     0.7826506f,
        0.7807372f,     0.7788165f,     0.7768885f,     0.7749531f,
        0.7730104f,     0.7710605f,     0.7691033f,     0.7671389f,
        0.7651672f,     0.7631884f,     0.7612024f,     0.7592092f,
        0.7572088f,     0.7552014f,     0.7531868f,     0.7511652f,
        0.7491364f,     0.7471006f,     0.7450578f,     0.743008f,
        0.7409511f,     0.7388873f,     0.7368166f,     0.7347389f,
        0.7326543f,     0.7305627f,     0.7284644f,     0.7263591f,
        0.7242471f,     0.7221282f,     0.7200025f,     0.7178701f,
        0.7157308f,     0.7135848f,     0.7114322f,     0.7092728f,
                };
        public static readonly float[] UNIT_2D_Y = new float[]{

        0f,     0.003067957f,   0.006135885f,   0.009203754f,
        0.01227154f,    0.01533921f,    0.01840673f,    0.02147408f,
        0.02454123f,    0.02760815f,    0.0306748f,     0.03374117f,
        0.03680722f,    0.03987293f,    0.04293826f,    0.04600318f,
        0.04906768f,    0.0521317f,     0.05519525f,    0.05825827f,
        0.06132074f,    0.06438263f,    0.06744392f,    0.07050458f,
        0.07356457f,    0.07662386f,    0.07968244f,    0.08274026f,
        0.08579731f,    0.08885355f,    0.09190895f,    0.0949635f,
        0.09801714f,    0.1010699f,     0.1041216f,     0.1071724f,
        0.1102222f,     0.113271f,      0.1163186f,     0.1193652f,
        0.1224107f,     0.125455f,      0.1284981f,     0.13154f,
        0.1345807f,     0.1376201f,     0.1406582f,     0.143695f,
        0.1467305f,     0.1497645f,     0.1527972f,     0.1558284f,
        0.1588582f,     0.1618864f,     0.1649131f,     0.1679383f,
        0.1709619f,     0.1739839f,     0.1770042f,     0.1800229f,
        0.1830399f,     0.1860552f,     0.1890687f,     0.1920804f,
        0.1950903f,     0.1980984f,     0.2011046f,     0.204109f,
        0.2071114f,     0.2101118f,     0.2131103f,     0.2161068f,
        0.2191012f,     0.2220936f,     0.2250839f,     0.2280721f,
        0.2310581f,     0.234042f,      0.2370236f,     0.240003f,
        0.2429802f,     0.245955f,      0.2489276f,     0.2518978f,
        0.2548656f,     0.2578311f,     0.2607941f,     0.2637547f,
        0.2667128f,     0.2696683f,     0.2726214f,     0.2755718f,
        0.2785197f,     0.2814649f,     0.2844075f,     0.2873475f,
        0.2902847f,     0.2932191f,     0.2961509f,     0.2990798f,
        0.3020059f,     0.3049292f,     0.3078496f,     0.3107671f,
        0.3136818f,     0.3165934f,     0.319502f,      0.3224077f,
        0.3253103f,     0.3282098f,     0.3311063f,     0.3339997f,
        0.3368899f,     0.3397769f,     0.3426607f,     0.3455413f,
        0.3484187f,     0.3512928f,     0.3541635f,     0.357031f,
        0.3598951f,     0.3627557f,     0.365613f,      0.3684668f,
        0.3713172f,     0.3741641f,     0.3770074f,     0.3798472f,
        0.3826834f,     0.385516f,      0.388345f,      0.3911704f,
        0.393992f,      0.39681f,       0.3996242f,     0.4024346f,
        0.4052413f,     0.4080442f,     0.4108432f,     0.4136383f,
        0.4164295f,     0.4192169f,     0.4220003f,     0.4247797f,
        0.4275551f,     0.4303265f,     0.4330938f,     0.4358571f,
        0.4386162f,     0.4413713f,     0.4441221f,     0.4468688f,
        0.4496113f,     0.4523496f,     0.4550836f,     0.4578133f,
        0.4605387f,     0.4632598f,     0.4659765f,     0.4686888f,
        0.4713967f,     0.4741002f,     0.4767992f,     0.4794938f,
        0.4821838f,     0.4848692f,     0.4875502f,     0.4902265f,
        0.4928982f,     0.4955653f,     0.4982277f,     0.5008854f,
        0.5035384f,     0.5061867f,     0.5088301f,     0.5114688f,
        0.5141028f,     0.5167318f,     0.519356f,      0.5219753f,
        0.5245897f,     0.5271991f,     0.5298036f,     0.5324031f,
        0.5349976f,     0.537587f,      0.5401714f,     0.5427508f,
        0.545325f,      0.5478941f,     0.550458f,      0.5530167f,
        0.5555702f,     0.5581185f,     0.5606616f,     0.5631993f,
        0.5657318f,     0.5682589f,     0.5707808f,     0.5732971f,
        0.5758082f,     0.5783138f,     0.5808139f,     0.5833086f,
        0.5857978f,     0.5882816f,     0.5907597f,     0.5932323f,
        0.5956993f,     0.5981607f,     0.6006165f,     0.6030666f,
        0.6055111f,     0.6079498f,     0.6103828f,     0.6128101f,
        0.6152316f,     0.6176473f,     0.6200572f,     0.6224613f,
        0.6248595f,     0.6272518f,     0.6296383f,     0.6320187f,
        0.6343933f,     0.6367618f,     0.6391245f,     0.641481f,
        0.6438316f,     0.646176f,      0.6485144f,     0.6508467f,
        0.6531729f,     0.6554928f,     0.6578067f,     0.6601143f,
        0.6624158f,     0.664711f,      0.6669999f,     0.6692826f,
        0.671559f,      0.673829f,      0.6760927f,     0.67835f,
        0.680601f,      0.6828455f,     0.6850837f,     0.6873153f,
        0.6895406f,     0.6917592f,     0.6939715f,     0.6961771f,
        0.6983762f,     0.7005688f,     0.7027547f,     0.7049341f,
                };

        public static readonly float[] UNIT_3D_X = new float[]{

        0.9999f,        0.9966413f,     0.7722031f,     0.6982852f,     0.9836538f,     0.7530656f,     0.8256697f,     0.743047f,      0.936589f,      0.8447217f,     0.8712779f,     0.8908413f,     0.8078733f,     0.958952f,      0.8573205f,     0.8047709f,
        0.8907192f,     0.8414699f,     0.7326233f,     0.961719f,      0.7936085f,     0.6894866f,     0.7955152f,     0.7491307f,     0.9106907f,     0.7960874f,     0.9756427f,     0.8435407f,     0.8848308f,     0.8171422f,     0.888743f,      0.9327761f,
        0.9876525f,     0.7565857f,     0.7328534f,     0.6855674f,     0.973652f,      0.9846603f,     0.691157f,      0.9068085f,     0.8447891f,     0.7167309f,     0.7150477f,     0.6842767f,     0.9879059f,     0.9037883f,     0.6172376f,     0.9540063f,
        0.9159054f,     0.7056647f,     0.9561993f,     0.8993033f,     0.8144354f,     0.7732513f,     0.7936496f,     0.9409977f,     0.7299842f,     0.8492779f,     0.7625135f,     0.6928337f,     0.7423968f,     0.9747982f,     0.6441048f,     0.8010921f,
        0.9740905f,     0.9165932f,     0.993829f,      0.8006068f,     0.7259059f,     0.6752217f,     0.749607f,      0.8836073f,     0.824694f,      0.9916456f,     0.8888112f,     0.777783f,      0.737981f,      0.6944548f,     0.8862727f,     0.9731007f,
        0.9498444f,     0.8355896f,     0.8272627f,     0.6689496f,     0.7170938f,     0.6438413f,     0.9179222f,     0.9814634f,     0.9622986f,     0.9587387f,     0.8524223f,     0.7294408f,     0.940811f,      0.8721445f,     0.7854758f,     0.7453709f,
        0.7041799f,     0.9945427f,     0.9707854f,     0.7909184f,     0.8637092f,     0.6038816f,     0.8830612f,     0.7489796f,     0.934518f,      0.9074249f,     0.7561252f,     0.9847355f,     0.790113f,      0.6608839f,     0.8738953f,     0.7915905f,
        0.7717234f,     0.6580026f,     0.6752651f,     0.962267f,      0.9908634f,     0.9859892f,     0.815308f,      0.7322285f,     0.8382149f,     0.7020156f,     0.8453046f,     0.9899657f,     0.7676985f,     0.7619733f,     0.7319888f,     0.810228f,
        0.9351245f,     0.8716383f,     0.7208273f,     0.9732847f,     0.6701431f,     0.888164f,      0.963111f,      0.7344475f,     0.7238916f,     0.9484353f,     0.8132456f,     0.8585284f,     0.9303901f,     0.6835483f,     0.9231599f,     0.946482f,
        0.9334192f,     0.7519085f,     0.8098992f,     0.7570102f,     0.9112324f,     0.977808f,      0.9270296f,     0.8899443f,     0.8979435f,     0.7231345f,     0.7921979f,     0.8480385f,     0.9012502f,     0.9029198f,     0.7851552f,     0.8896477f,
        0.7792798f,     0.8267265f,     0.6570614f,     0.8549284f,     0.9663315f,     0.9884848f,     0.7891456f,     0.8905352f,     0.9103106f,     0.7559778f,     0.7116407f,     0.9687303f,     0.888167f,      0.6625429f,     0.8025635f,     0.5888452f,
        0.6549245f,     0.6699828f,     0.8968916f,     0.9948872f,     0.7518303f,     0.8750748f,     0.7276911f,     0.946101f,      0.9680679f,     0.9015719f,     0.8552188f,     0.6200085f,     0.783321f,      0.7792976f,     0.9192806f,     0.8622142f,
        0.7358887f,     0.8619644f,     0.8898371f,     0.7895864f,     0.7547674f,     0.9039923f,     0.8821381f,     0.8321781f,     0.9810783f,     0.8978756f,     0.9530674f,     0.9353671f,     0.6295643f,     0.8891271f,     0.7535068f,     0.7122681f,
        0.9237125f,     0.9709055f,     0.7159427f,     0.9956214f,     0.9772235f,     0.9627615f,     0.8873999f,     0.9413171f,     0.9722663f,     0.9242591f,     0.7320173f,     0.7442064f,     0.9299697f,     0.7345034f,     0.9588232f,     0.751237f,
        0.8404082f,     0.9403649f,     0.7924345f,     0.8571462f,     0.8494488f,     0.9365232f,     0.7230896f,     0.9443654f,     0.8602128f,     0.9699747f,     0.7611919f,     0.8435236f,     0.7334132f,     0.9147204f,     0.9180276f,     0.8308641f,
        0.7802943f,     0.7741121f,     0.9552454f,     0.9041751f,     0.8293912f,     0.7180784f,     0.8702018f,     0.8985632f,     0.7765562f,     0.8855184f,     0.9617072f,     0.675914f,      0.8331314f,     0.9842819f,     0.9384696f,     0.7856663f,
                };
        public static readonly float[] UNIT_3D_Y = new float[]{

        0.009999833f,   0.05315669f,    0.3189258f,     0.4961783f,     0.0228067f,     0.2288286f,     0.3819773f,     0.4531597f,     0.1602568f,     0.139134f,      0.3038664f,     0.3044934f,     0.2837799f,     0.1831657f,     0.1902632f,     0.3792938f,
        0.3104884f,     0.05137433f,    0.1186244f,     0.01920586f,    0.02214008f,    0.4436462f,     0.2319368f,     0.432908f,      0.2382693f,     0.400051f,      0.05657052f,    0.3380574f,     0.05337312f,    0.02282507f,    0.1758849f,     0.1894914f,
        0.05114136f,    0.2400674f,     0.1418455f,     0.4792646f,     0.04221635f,    0.1131981f,     0.2257846f,     0.01285391f,    0.2870257f,     0.37715f,       0.4601266f,     0.427889f,      0.05775272f,    0.2674726f,     0.5388131f,     0.1184476f,
        0.2027916f,     0.4346014f,     0.02988255f,    0.2000637f,     0.01534531f,    0.1770239f,     0.2017775f,     0.06243057f,    0.3406668f,     0.131278f,      0.1629261f,     0.4849316f,     0.1482711f,     0.08320025f,    0.5267267f,     0.4131829f,
        0.111453f,      0.2546358f,     0.02921815f,    0.23094f,       0.06817598f,    0.4902599f,     0.1119211f,     0.3273442f,     0.3765483f,     0.08698189f,    0.3126198f,     0.1610955f,     0.01093642f,    0.2824784f,     0.2892543f,     0.0525014f,
        0.06070964f,    0.233634f,      0.2179337f,     0.3836502f,     0.07134722f,    0.5332441f,     0.2498016f,     0.03813357f,    0.03467483f,    0.1350425f,     0.3047842f,     0.2094095f,     0.1361156f,     0.3137822f,     0.1025883f,     0.2389388f,
        0.1966798f,     0.03498428f,    0.01979464f,    0.2096809f,     0.1912156f,     0.5255691f,     0.1802619f,     0.3861297f,     0.05584178f,    0.06742059f,    0.3373349f,     0.05285928f,    0.1654463f,     0.5088003f,     0.1534616f,     0.07626358f,
        0.1320192f,     0.4617086f,     0.440507f,      0.09432823f,    0.08358551f,    0.1196109f,     0.2831396f,     0.312239f,      0.07998031f,    0.2001875f,     0.274884f,      0.01768178f,    0.4048647f,     0.2035187f,     0.4560719f,     0.2694751f,
        0.1186594f,     0.10853f,       0.4840901f,     0.0752452f,     0.3332888f,     0.1675386f,     0.1422592f,     0.1362217f,     0.2525686f,     0.1430717f,     0.4005177f,     0.04491974f,    0.06959977f,    0.2884799f,     0.2396463f,     0.0767364f,
        0.2118908f,     0.02975471f,    0.1195662f,     0.1833726f,     0.1377084f,     0.03168813f,    0.1570116f,     0.1284151f,     0.07129524f,    0.01634788f,    0.1183308f,     0.359021f,      0.2079969f,     0.2643534f,     0.2457965f,     0.1625394f,
        0.08440279f,    0.1662441f,     0.4378462f,     0.09008222f,    0.1586963f,     0.07699944f,    0.0396869f,     0.2819257f,     0.1968764f,     0.09032436f,    0.1557251f,     0.02841522f,    0.2566804f,     0.516108f,      0.0202499f,     0.5671428f,
        0.4983105f,     0.384398f,      0.284262f,      0.01913304f,    0.1834249f,     0.0177016f,     0.1290854f,     0.1601886f,     0.1500015f,     0.08881514f,    0.2463192f,     0.5143253f,     0.3059155f,     0.4283543f,     0.188925f,      0.2524786f,
        0.09714338f,    0.1584308f,     0.2933253f,     0.3135367f,     0.3089018f,     0.2542058f,     0.3122797f,     0.2448043f,     0.1370007f,     0.1403247f,     0.1515408f,     0.1870982f,     0.4983976f,     0.07944988f,    0.1239729f,     0.2700147f,
        0.1107372f,     0.09306253f,    0.1260265f,     0.04991404f,    0.06871628f,    0.1685102f,     0.296181f,      0.2268092f,     0.02994743f,    0.1112809f,     0.2649423f,     0.2507573f,     0.2555324f,     0.4741449f,     0.01795687f,    0.04522858f,
        0.01864028f,    0.1642646f,     0.01666413f,    0.3624852f,     0.04862417f,    0.07355487f,    0.3592844f,     0.01297323f,    0.2229164f,     0.1725571f,     0.4509187f,     0.32884f,       0.3087734f,     0.05865071f,    0.2315183f,     0.3680317f,
        0.1078166f,     0.2644246f,     0.07526769f,    0.298117f,      0.1773641f,     0.1109618f,     0.3309684f,     0.2072884f,     0.08903114f,    0.03057944f,    0.07640176f,    0.4176275f,     0.2346449f,     0.09798937f,    0.1328729f,     0.409318f,
                };
        public static readonly float[] UNIT_3D_Z = new float[]{

        0.009999333f,   0.06229419f,    0.549535f,      0.5159504f,     0.1786198f,     0.6168708f,     0.4151661f,     0.4924708f,     0.311639f,      0.5168046f,     0.3854088f,     0.3371729f,     0.5165362f,     0.2164752f,     0.4783319f,     0.4565961f,
        0.3319882f,     0.5378561f,     0.6702175f,     0.2733639f,     0.6080257f,     0.5725262f,     0.5597864f,     0.501392f,      0.3374465f,     0.4540969f,     0.2119458f,     0.4173203f,     0.4628454f,     0.5759841f,     0.4233206f,     0.3066298f,
        0.1480778f,     0.6082316f,     0.6654366f,     0.5479988f,     0.2240973f,     0.132779f,      0.68653f,       0.4213468f,     0.4516056f,     0.5865617f,     0.5262986f,     0.5904883f,     0.1438976f,     0.3341033f,     0.5733221f,     0.2753945f,
        0.3464f,        0.5596061f,     0.2911872f,     0.3888806f,     0.5800512f,     0.6088883f,     0.5739391f,     0.3326045f,     0.592511f,      0.511364f,      0.6261216f,     0.5336879f,     0.6533473f,     0.2069931f,     0.5546962f,     0.4330487f,
        0.1967892f,     0.3082491f,     0.1070059f,     0.5528973f,     0.6844068f,     0.5511088f,     0.6523519f,     0.3347893f,     0.4220084f,     0.09525289f,    0.3350875f,     0.6075374f,     0.6747329f,     0.6617693f,     0.3617356f,     0.2243183f,
        0.3067734f,     0.4971974f,     0.5178237f,     0.6366466f,     0.6933153f,     0.5487431f,     0.30825f,       0.1878178f,     0.269776f,      0.2501671f,     0.4248327f,     0.6512018f,     0.3103985f,     0.3753728f,     0.6103306f,     0.6223588f,
        0.6822373f,     0.09829023f,    0.2391313f,     0.5748756f,     0.466308f,      0.599253f,      0.4332535f,     0.5384547f,     0.3515077f,     0.4147704f,     0.5607851f,     0.165837f,      0.590211f,      0.5516835f,     0.4612555f,     0.6062742f,
        0.6221044f,     0.5948594f,     0.5915831f,     0.2552338f,     0.1058449f,     0.1162691f,     0.505079f,      0.6052672f,     0.5394432f,     0.6834465f,     0.4581473f,     0.140197f,      0.4967128f,     0.6147982f,     0.5061529f,     0.5204938f,
        0.3338595f,     0.4779832f,     0.4960492f,     0.2169217f,     0.6631944f,     0.4278966f,     0.2284283f,     0.6648538f,     0.6420203f,     0.2828444f,     0.4221577f,     0.5107948f,     0.3599029f,     0.6704782f,     0.3005753f,     0.3135017f,
        0.2895356f,     0.6585956f,     0.5742536f,     0.6271444f,     0.3881906f,     0.2070928f,     0.3405342f,     0.4376171f,     0.4342977f,     0.6905137f,     0.5986821f,     0.389788f,      0.3801123f,     0.3388998f,     0.5684324f,     0.426741f,
        0.6209663f,     0.5374814f,     0.6136457f,     0.5108645f,     0.2025311f,     0.1302646f,     0.6129227f,     0.3570222f,     0.3641074f,     0.6483356f,     0.6850674f,     0.2464837f,     0.3811491f,     0.5428346f,     0.5962228f,     0.5758562f,
        0.5681202f,     0.6351073f,     0.3387928f,     0.09916355f,    0.6333297f,     0.4836639f,     0.6736488f,     0.2814826f,     0.2008583f,     0.4234147f,     0.4559909f,     0.5925024f,     0.5411321f,     0.4573925f,     0.3452978f,     0.4391369f,
        0.6700978f,     0.4815776f,     0.3494999f,     0.5274922f,     0.5787104f,     0.3437693f,     0.3525817f,     0.4975444f,     0.1368068f,     0.417287f,      0.262103f,      0.3001377f,     0.5960274f,     0.4507112f,     0.6456456f,     0.6478937f,
        0.3667322f,     0.2206399f,     0.6866902f,     0.07903611f,    0.2007792f,     0.2114111f,     0.353268f,      0.2499594f,     0.2319512f,     0.3651871f,     0.6276594f,     0.6190942f,     0.2643095f,     0.4854805f,     0.2834353f,     0.6584811f,
        0.5416332f,     0.2978775f,     0.6097293f,     0.3659301f,     0.5254259f,     0.3428031f,     0.5899628f,     0.328642f,      0.4586307f,     0.1713861f,     0.4661107f,     0.4246553f,     0.6056105f,     0.3998083f,     0.3219076f,     0.4173937f,
        0.6160491f,     0.5751783f,     0.2860788f,     0.3059307f,     0.5297663f,     0.6870596f,     0.3649778f,     0.3868018f,     0.6237259f,     0.4635969f,     0.2632148f,     0.6072294f,     0.5008332f,     0.1469259f,     0.3187783f,     0.4638827f,
                };


        //public static readonly vec4[] UNIT_4D = new vec4[] { };

        #endregion

        #region Constructors
        public PerlinNoise(int seed, float freq)
        {
            Seed = seed;
            Frequency = freq;
            //innerNoise = new WhiteNoiseInt(new RawNoise(Seed)[Seed]);
        }
        #endregion

        #region Functions

        //DELETE
        static float EaseF(float t)
        {
            //return t < 0.5f ? 2 * t * t : 4 * t - 2 * t * t - 1;
            return t * t * (3 - 2 * t);
        }
        static float Interpolate1D(float x, float y, float t)
        {
            //if (t < 0 || t > 1) throw new Exception("t (" + t + ") is not a valid value");

            //Ease func
            return x + (y - x) * t * t * (3 - 2 * t);
        }
        static float InterpolatePreCalc(float x, float y, float preT)
        {
            return x + (y - x) * preT;
        }

        //DELETE
        float DotGradient2DOld(int indX, int indY, float x, float y)
        {
            int hash = Seed;
            hash ^= xPrime * indX;
            hash ^= yPrime * indY;

            hash = (hash * hash * hash) >> 16;

            //A gyorsaság érdekében switch statement-eket
            //használok: Az első 3 bitet ignoráljuk, mivel az
            //a szám3as azonosítója
            //és ugye 8 esetre esik szét:
            //Az első bit a sorrendet jelöli,
            //a 2. és a 3. pedig a számok előjelét jelöli (+/-)

            switch (hash & 0b11100000000)
            {
                //case 0b000000:
                default:
                    hash &= 255;
                    return UNIT_2D_X[hash] * x + UNIT_2D_Y[hash] * y;
                case 0b00100000000:
                    hash &= 255;
                    return UNIT_2D_X[hash] * x + -UNIT_2D_Y[hash] * y;
                case 0b01000000000:
                    hash &= 255;
                    return -UNIT_2D_X[hash] * x + UNIT_2D_Y[hash] * y;
                case 0b01100000000:
                    hash &= 255;
                    return -UNIT_2D_X[hash] * x + -UNIT_2D_Y[hash] * y;


                case 0b10000000000:
                    hash &= 255;
                    return UNIT_2D_Y[hash] * x + UNIT_2D_X[hash] * y;
                case 0b10100000000:
                    hash &= 255;
                    return UNIT_2D_Y[hash] * x + -UNIT_2D_X[hash] * y;
                case 0b11000000000:
                    hash &= 255;
                    return -UNIT_2D_Y[hash] * x + UNIT_2D_X[hash] * y;

                case 0b11100000000:
                    hash &= 255;
                    return -UNIT_2D_Y[hash] * x + -UNIT_2D_X[hash] * y;
            }
        }
        float DotGradient2D_Performance(int indX, int indY, float x, float y)
        {
            //Itt nem szorzunk be prímekkel,
            //Hanem eleve az azokkal beszorzott érték
            //érkezik a függvénybe
            int hash = Seed;
            hash ^= xPrime * indX;
            hash ^= yPrime * indY;


            //Az a lényeg hogy iytt a vektorok
            //korrdinátái csak -1,0 és 1, ezért nem
            //is írjuk oda, hanem előjellel helyettesítjük,
            //mivel összeadni gyorsabb mint szorozni

            hash = (hash * hash * hash >> 20) & 7;

            switch (hash)
            {
                default:
                case 0:
                    return y;
                case 1:
                    return x + y;
                case 2:
                    return x;
                case 3:
                    return x - y;
                case 4:
                    return -y;
                case 5:
                    return -x - y;
                case 6:
                    return -x;
                case 7:
                    return y - x;
            }
        }
        //DELETE
        float DotGradient3D(int indX, int indY, int indZ, float x, float y, float z)
        {
            int hash;

            hash = Seed;
            hash ^= xPrime * indX;
            hash ^= yPrime * indY;
            hash ^= zPrime * indZ;

            hash = (hash * hash * hash) >> 16;

            //A gyorsaság érdekében az összes eshetőséget szétbontottam
            //switch statement-ekre:
            //Egyszer ugye lehet a koordinátákat variálni, ami ugye 2^3=8
            //Másrészt a sorrendet lehet variálni, 3! = 3*2*1=6
            //És ezeket összeszorozzuk: 48
            //Majd a hash-t 3 helyértékkel odébb toljuk, mivel a 48 
            //maradékát nem a számhármas azonosítójából vonjuk le

            switch ((hash >> 8) % 48)
            {
                //ORDER: X Y Z
                //case 0:
                default:
                    hash &= 255;
                    return UNIT_3D_X[hash] * x + UNIT_3D_Y[hash] * y + UNIT_3D_Z[hash] * z;

                case 1:
                    hash &= 255;
                    return -UNIT_3D_X[hash] * x + UNIT_3D_Y[hash] * y + UNIT_3D_Z[hash] * z;

                case 2:
                    hash &= 255;
                    return UNIT_3D_X[hash] * x + -UNIT_3D_Y[hash] * y + UNIT_3D_Z[hash] * z;

                case 3:
                    hash &= 255;
                    return -UNIT_3D_X[hash] * x + -UNIT_3D_Y[hash] * y + UNIT_3D_Z[hash] * z;

                case 4:
                    hash &= 255;
                    return UNIT_3D_X[hash] * x + UNIT_3D_Y[hash] * y + -UNIT_3D_Z[hash] * z;

                case 5:
                    hash &= 255;
                    return -UNIT_3D_X[hash] * x + UNIT_3D_Y[hash] * y + -UNIT_3D_Z[hash] * z;

                case 6:
                    hash &= 255;
                    return UNIT_3D_X[hash] * x + -UNIT_3D_Y[hash] * y + -UNIT_3D_Z[hash] * z;

                case 7:
                    hash &= 255;
                    return -UNIT_3D_X[hash] * x + -UNIT_3D_Y[hash] * y + -UNIT_3D_Z[hash] * z;

                case 8:
                    hash &= 255;
                    return UNIT_3D_X[hash] * x + UNIT_3D_Z[hash] * y + UNIT_3D_Y[hash] * z;


                //ORDER: X Z Y
                case 9:
                    hash &= 255;
                    return -UNIT_3D_X[hash] * x + UNIT_3D_Z[hash] * y + UNIT_3D_Y[hash] * z;

                case 10:
                    hash &= 255;
                    return UNIT_3D_X[hash] * x + -UNIT_3D_Z[hash] * y + UNIT_3D_Y[hash] * z;

                case 11:
                    hash &= 255;
                    return -UNIT_3D_X[hash] * x + -UNIT_3D_Z[hash] * y + UNIT_3D_Y[hash] * z;

                case 12:
                    hash &= 255;
                    return UNIT_3D_X[hash] * x + UNIT_3D_Z[hash] * y + -UNIT_3D_Y[hash] * z;

                case 13:
                    hash &= 255;
                    return -UNIT_3D_X[hash] * x + UNIT_3D_Z[hash] * y + -UNIT_3D_Y[hash] * z;

                case 14:
                    hash &= 255;
                    return UNIT_3D_X[hash] * x + -UNIT_3D_Z[hash] * y + -UNIT_3D_Y[hash] * z;

                case 15:
                    hash &= 255;
                    return -UNIT_3D_X[hash] * x + -UNIT_3D_Z[hash] * y + -UNIT_3D_Y[hash] * z;


                //ORDER: Y X Z
                case 16:
                    hash &= 255;
                    return UNIT_3D_Y[hash] * x + UNIT_3D_X[hash] * y + UNIT_3D_Z[hash] * z;

                case 17:
                    hash &= 255;
                    return -UNIT_3D_Y[hash] * x + UNIT_3D_X[hash] * y + UNIT_3D_Z[hash] * z;

                case 18:
                    hash &= 255;
                    return UNIT_3D_Y[hash] * x + -UNIT_3D_X[hash] * y + UNIT_3D_Z[hash] * z;

                case 19:
                    hash &= 255;
                    return -UNIT_3D_Y[hash] * x + -UNIT_3D_X[hash] * y + UNIT_3D_Z[hash] * z;

                case 20:
                    hash &= 255;
                    return UNIT_3D_Y[hash] * x + UNIT_3D_X[hash] * y + -UNIT_3D_Z[hash] * z;

                case 21:
                    hash &= 255;
                    return -UNIT_3D_Y[hash] * x + UNIT_3D_X[hash] * y + -UNIT_3D_Z[hash] * z;

                case 22:
                    hash &= 255;
                    return UNIT_3D_Y[hash] * x + -UNIT_3D_X[hash] * y + -UNIT_3D_Z[hash] * z;

                case 23:
                    hash &= 255;
                    return -UNIT_3D_Y[hash] * x + -UNIT_3D_X[hash] * y + -UNIT_3D_Z[hash] * z;


                //ORDER: Y Z X
                case 24:
                    hash &= 255;
                    return UNIT_3D_Y[hash] * x + UNIT_3D_Z[hash] * y + UNIT_3D_X[hash] * z;

                case 25:
                    hash &= 255;
                    return -UNIT_3D_Y[hash] * x + UNIT_3D_Z[hash] * y + UNIT_3D_X[hash] * z;

                case 26:
                    hash &= 255;
                    return UNIT_3D_Y[hash] * x + -UNIT_3D_Z[hash] * y + UNIT_3D_X[hash] * z;

                case 27:
                    hash &= 255;
                    return -UNIT_3D_Y[hash] * x + -UNIT_3D_Z[hash] * y + UNIT_3D_X[hash] * z;

                case 28:
                    hash &= 255;
                    return UNIT_3D_Y[hash] * x + UNIT_3D_Z[hash] * y + -UNIT_3D_X[hash] * z;

                case 29:
                    hash &= 255;
                    return -UNIT_3D_Y[hash] * x + UNIT_3D_Z[hash] * y + -UNIT_3D_X[hash] * z;

                case 30:
                    hash &= 255;
                    return UNIT_3D_Y[hash] * x + -UNIT_3D_Z[hash] * y + -UNIT_3D_X[hash] * z;

                case 31:
                    hash &= 255;
                    return -UNIT_3D_Y[hash] * x + -UNIT_3D_Z[hash] * y + -UNIT_3D_X[hash] * z;


                //ORDER: Z X Y
                case 32:
                    hash &= 255;
                    return UNIT_3D_Z[hash] * x + UNIT_3D_X[hash] * y + UNIT_3D_Y[hash] * z;

                case 33:
                    hash &= 255;
                    return -UNIT_3D_Z[hash] * x + UNIT_3D_X[hash] * y + UNIT_3D_Y[hash] * z;

                case 34:
                    hash &= 255;
                    return UNIT_3D_Z[hash] * x + -UNIT_3D_X[hash] * y + UNIT_3D_Y[hash] * z;

                case 35:
                    hash &= 255;
                    return -UNIT_3D_Z[hash] * x + -UNIT_3D_X[hash] * y + UNIT_3D_Y[hash] * z;

                case 36:
                    hash &= 255;
                    return UNIT_3D_Z[hash] * x + UNIT_3D_X[hash] * y + -UNIT_3D_Y[hash] * z;

                case 37:
                    hash &= 255;
                    return -UNIT_3D_Z[hash] * x + UNIT_3D_X[hash] * y + -UNIT_3D_Y[hash] * z;

                case 38:
                    hash &= 255;
                    return UNIT_3D_Z[hash] * x + -UNIT_3D_X[hash] * y + -UNIT_3D_Y[hash] * z;

                case 39:
                    hash &= 255;
                    return -UNIT_3D_Z[hash] * x + -UNIT_3D_X[hash] * y + -UNIT_3D_Y[hash] * z;


                //ORDER: Z Y X
                case 40:
                    hash &= 255;
                    return UNIT_3D_Z[hash] * x + UNIT_3D_Y[hash] * y + UNIT_3D_X[hash] * z;

                case 41:
                    hash &= 255;
                    return -UNIT_3D_Z[hash] * x + UNIT_3D_Y[hash] * y + UNIT_3D_X[hash] * z;

                case 42:
                    hash &= 255;
                    return UNIT_3D_Z[hash] * x + -UNIT_3D_Y[hash] * y + UNIT_3D_X[hash] * z;

                case 43:
                    hash &= 255;
                    return -UNIT_3D_Z[hash] * x + -UNIT_3D_Y[hash] * y + UNIT_3D_X[hash] * z;

                case 44:
                    hash &= 255;
                    return UNIT_3D_Z[hash] * x + UNIT_3D_Y[hash] * y + -UNIT_3D_X[hash] * z;

                case 45:
                    hash &= 255;
                    return -UNIT_3D_Z[hash] * x + UNIT_3D_Y[hash] * y + -UNIT_3D_X[hash] * z;

                case 46:
                    hash &= 255;
                    return UNIT_3D_Z[hash] * x + -UNIT_3D_Y[hash] * y + -UNIT_3D_X[hash] * z;

                case 47:
                    hash &= 255;
                    return -UNIT_3D_Z[hash] * x + -UNIT_3D_Y[hash] * y + -UNIT_3D_X[hash] * z;
            }

        }
        //DELETE
        float DotGradient3DPreHashed(int xHash, int yHash, int zHash, float x, float y, float z)
        {
            int hash;

            hash = Seed;
            hash ^= xHash;
            hash ^= yHash;
            hash ^= zHash;

            hash = hash * hash * hash;

            int switchID = hash % 48;

            hash = (hash >> 16) & 255;

            //A gyorsaság érdekében az összes eshetőséget szétbontottam
            //switch statement-ekre:
            //Egyszer ugye lehet a koordinátákat variálni, ami ugye 2^3=8
            //Másrészt a sorrendet lehet variálni, 3! = 3*2*1=6
            //És ezeket összeszorozzuk: 48
            //Majd a hash-t 3 helyértékkel odébb toljuk, mivel a 48 
            //maradékát nem a számhármas azonosítójából vonjuk le

            switch (switchID)
            {
                //ORDER: X Y Z
                //case 0:
                default:
                    return UNIT_3D_X[hash] * x + UNIT_3D_Y[hash] * y + UNIT_3D_Z[hash] * z;

                case 1:
                    return -UNIT_3D_X[hash] * x + UNIT_3D_Y[hash] * y + UNIT_3D_Z[hash] * z;

                case 2:
                    return UNIT_3D_X[hash] * x + -UNIT_3D_Y[hash] * y + UNIT_3D_Z[hash] * z;

                case 3:
                    return -UNIT_3D_X[hash] * x + -UNIT_3D_Y[hash] * y + UNIT_3D_Z[hash] * z;

                case 4:
                    return UNIT_3D_X[hash] * x + UNIT_3D_Y[hash] * y + -UNIT_3D_Z[hash] * z;

                case 5:
                    return -UNIT_3D_X[hash] * x + UNIT_3D_Y[hash] * y + -UNIT_3D_Z[hash] * z;

                case 6:
                    return UNIT_3D_X[hash] * x + -UNIT_3D_Y[hash] * y + -UNIT_3D_Z[hash] * z;

                case 7:
                    return -UNIT_3D_X[hash] * x + -UNIT_3D_Y[hash] * y + -UNIT_3D_Z[hash] * z;

                case 8:
                    return UNIT_3D_X[hash] * x + UNIT_3D_Z[hash] * y + UNIT_3D_Y[hash] * z;


                //ORDER: X Z Y
                case 9:
                    return -UNIT_3D_X[hash] * x + UNIT_3D_Z[hash] * y + UNIT_3D_Y[hash] * z;

                case 10:
                    return UNIT_3D_X[hash] * x + -UNIT_3D_Z[hash] * y + UNIT_3D_Y[hash] * z;

                case 11:
                    return -UNIT_3D_X[hash] * x + -UNIT_3D_Z[hash] * y + UNIT_3D_Y[hash] * z;

                case 12:
                    return UNIT_3D_X[hash] * x + UNIT_3D_Z[hash] * y + -UNIT_3D_Y[hash] * z;

                case 13:
                    return -UNIT_3D_X[hash] * x + UNIT_3D_Z[hash] * y + -UNIT_3D_Y[hash] * z;

                case 14:
                    return UNIT_3D_X[hash] * x + -UNIT_3D_Z[hash] * y + -UNIT_3D_Y[hash] * z;

                case 15:
                    return -UNIT_3D_X[hash] * x + -UNIT_3D_Z[hash] * y + -UNIT_3D_Y[hash] * z;


                //ORDER: Y X Z
                case 16:
                    return UNIT_3D_Y[hash] * x + UNIT_3D_X[hash] * y + UNIT_3D_Z[hash] * z;

                case 17:
                    return -UNIT_3D_Y[hash] * x + UNIT_3D_X[hash] * y + UNIT_3D_Z[hash] * z;

                case 18:
                    return UNIT_3D_Y[hash] * x + -UNIT_3D_X[hash] * y + UNIT_3D_Z[hash] * z;

                case 19:
                    return -UNIT_3D_Y[hash] * x + -UNIT_3D_X[hash] * y + UNIT_3D_Z[hash] * z;

                case 20:
                    return UNIT_3D_Y[hash] * x + UNIT_3D_X[hash] * y + -UNIT_3D_Z[hash] * z;

                case 21:
                    return -UNIT_3D_Y[hash] * x + UNIT_3D_X[hash] * y + -UNIT_3D_Z[hash] * z;

                case 22:
                    return UNIT_3D_Y[hash] * x + -UNIT_3D_X[hash] * y + -UNIT_3D_Z[hash] * z;

                case 23:
                    return -UNIT_3D_Y[hash] * x + -UNIT_3D_X[hash] * y + -UNIT_3D_Z[hash] * z;


                //ORDER: Y Z X
                case 24:
                    return UNIT_3D_Y[hash] * x + UNIT_3D_Z[hash] * y + UNIT_3D_X[hash] * z;

                case 25:
                    return -UNIT_3D_Y[hash] * x + UNIT_3D_Z[hash] * y + UNIT_3D_X[hash] * z;

                case 26:
                    return UNIT_3D_Y[hash] * x + -UNIT_3D_Z[hash] * y + UNIT_3D_X[hash] * z;

                case 27:
                    return -UNIT_3D_Y[hash] * x + -UNIT_3D_Z[hash] * y + UNIT_3D_X[hash] * z;

                case 28:
                    return UNIT_3D_Y[hash] * x + UNIT_3D_Z[hash] * y + -UNIT_3D_X[hash] * z;

                case 29:
                    return -UNIT_3D_Y[hash] * x + UNIT_3D_Z[hash] * y + -UNIT_3D_X[hash] * z;

                case 30:
                    ;
                    return UNIT_3D_Y[hash] * x + -UNIT_3D_Z[hash] * y + -UNIT_3D_X[hash] * z;

                case 31:
                    return -UNIT_3D_Y[hash] * x + -UNIT_3D_Z[hash] * y + -UNIT_3D_X[hash] * z;


                //ORDER: Z X Y
                case 32:
                    return UNIT_3D_Z[hash] * x + UNIT_3D_X[hash] * y + UNIT_3D_Y[hash] * z;

                case 33:
                    return -UNIT_3D_Z[hash] * x + UNIT_3D_X[hash] * y + UNIT_3D_Y[hash] * z;

                case 34:
                    return UNIT_3D_Z[hash] * x + -UNIT_3D_X[hash] * y + UNIT_3D_Y[hash] * z;

                case 35:
                    return -UNIT_3D_Z[hash] * x + -UNIT_3D_X[hash] * y + UNIT_3D_Y[hash] * z;

                case 36:
                    return UNIT_3D_Z[hash] * x + UNIT_3D_X[hash] * y + -UNIT_3D_Y[hash] * z;

                case 37:
                    return -UNIT_3D_Z[hash] * x + UNIT_3D_X[hash] * y + -UNIT_3D_Y[hash] * z;

                case 38:
                    return UNIT_3D_Z[hash] * x + -UNIT_3D_X[hash] * y + -UNIT_3D_Y[hash] * z;

                case 39:
                    return -UNIT_3D_Z[hash] * x + -UNIT_3D_X[hash] * y + -UNIT_3D_Y[hash] * z;


                //ORDER: Z Y X
                case 40:
                    return UNIT_3D_Z[hash] * x + UNIT_3D_Y[hash] * y + UNIT_3D_X[hash] * z;

                case 41:
                    return -UNIT_3D_Z[hash] * x + UNIT_3D_Y[hash] * y + UNIT_3D_X[hash] * z;

                case 42:
                    return UNIT_3D_Z[hash] * x + -UNIT_3D_Y[hash] * y + UNIT_3D_X[hash] * z;

                case 43:
                    return -UNIT_3D_Z[hash] * x + -UNIT_3D_Y[hash] * y + UNIT_3D_X[hash] * z;

                case 44:
                    return UNIT_3D_Z[hash] * x + UNIT_3D_Y[hash] * y + -UNIT_3D_X[hash] * z;

                case 45:
                    return -UNIT_3D_Z[hash] * x + UNIT_3D_Y[hash] * y + -UNIT_3D_X[hash] * z;

                case 46:
                    return UNIT_3D_Z[hash] * x + -UNIT_3D_Y[hash] * y + -UNIT_3D_X[hash] * z;

                case 47:
                    return -UNIT_3D_Z[hash] * x + -UNIT_3D_Y[hash] * y + -UNIT_3D_X[hash] * z;
            }

        }
        //DELETE
        float DotGradient3D_Performance(int indX, int indY, int indZ, float x, float y, float z)
        {
            int hash;

            hash = Seed;
            hash ^= xPrime * indX;
            hash ^= yPrime * indY;
            hash ^= zPrime * indZ;

            hash = ((hash * hash * hash) >> 16) % 26;


            switch (hash)
            {
                default:
                case 0:
                    return -x - y - z;

                case 1:
                    return -x - y;

                case 2:
                    return z - x - y;

                case 3:
                    return -x - z;

                case 4:
                    return -x;

                case 5:
                    return z - x;

                case 6:
                    return y - x - z;

                case 7:
                    return y - x;

                case 8:
                    return y + z - x;

                case 9:
                    return -y - z;

                case 10:
                    return -y;

                case 11:
                    return z - y;

                case 12:
                    return -z;

                case 13:
                    return z;

                case 14:
                    return y - z;

                case 15:
                    return y;

                case 16:
                    return y + z;

                case 17:
                    return x - y - z;

                case 18:
                    return x - y;

                case 19:
                    return x + z - y;

                case 20:
                    return x - z;

                case 21:
                    return x;

                case 22:
                    return x + z;

                case 23:
                    return x + y - z;

                case 24:
                    return x + y;

                case 25:
                    return x + y + z;
            }

        }
        float DotGradient3D_PreHashed_Performance(int indX, int indY, int indZ, float x, float y, float z)
        {
            int hash;

            //Itt nem szorzunk be prímekkel,
            //Hanem eleve az azokkal beszorzott érték
            //érkezik a függvénybe

            hash = Seed;
            hash ^= indX;
            hash ^= indY;
            hash ^= indZ;

            //Az a lényeg hogy iytt a vektorok
            //korrdinátái csak -1,0 és 1, ezért nem
            //is írjuk oda, hanem előjellel helyettesítjük,
            //mivel összeadni gyorsabb mint szorozni

            hash = ((hash * hash * hash) >> 4) % 18;
            hash = hash < 0 ? -hash : hash;
            switch (hash)
            {
                default:
                case 0:
                    return -x - y;

                case 1:
                    return -x - z;

                case 2:
                    return -x;

                case 3:
                    return -x + z;

                case 4:
                    return -x + y;

                case 5:
                    return -y - z;

                case 6:
                    return -y;

                case 7:
                    return -y + z;

                case 8:
                    return -z;

                case 9:
                    return z;

                case 10:
                    return y - z;

                case 11:
                    return y;

                case 12:
                    return y + z;

                case 13:
                    return x - y;

                case 14:
                    return x - z;

                case 15:
                    return x;

                case 16:
                    return x + z;

                case 17:
                    return x + y;
            }

        }

        /*
        float DotGradient4D(int indX, int indY, int indZ, int indW, float x, float y, float z, float w)
        {
            int hash;

            hash = Seed;
            hash ^= xPrime * indX;
            hash ^= yPrime * indY;
            hash ^= zPrime * indZ;
            hash ^= wPrime * indW;

            hash = hash * hash * hash;
            hash = (hash >> 16) & 255;

            return UNIT_4D[hash].x * x + UNIT_4D[hash].y * y + UNIT_4D[hash].z * z + UNIT_4D[hash].w * w;
        }*/


        public float GetFloat(float x, float y)
        {
            float xf = x / Frequency;
            float yf = y / Frequency;

            int xi = x > 0 ? (int)xf : (int)xf - 1;
            int yi = y > 0 ? (int)yf : (int)yf - 1;

            float xr = xf - xi;
            float yr = yf - yi;

            //Calculating dot product for each corner vector
            float d00 = DotGradient2D_Performance(xi, yi, xr, yr);
            float d10 = DotGradient2D_Performance(xi + 1, yi, xr - 1, yr);
            float d01 = DotGradient2D_Performance(xi, yi + 1, xr, yr - 1);
            float d11 = DotGradient2D_Performance(xi + 1, yi + 1, xr - 1, yr - 1);

            //Interpolating between dot products
            float botIntp, topIntp;

            botIntp = Interpolate1D(d00, d10, xr);
            topIntp = Interpolate1D(d01, d11, xr);

            return Interpolate1D(botIntp, topIntp, yr);
        }
        public float GetFloat(float x, float y, float z)
        {
            float xf = x / Frequency;
            float yf = y / Frequency;
            float zf = z / Frequency;

            int xi = x > 0 ? (int)xf : (int)xf - 1;
            int yi = y > 0 ? (int)yf : (int)yf - 1;
            int zi = z > 0 ? (int)zf : (int)zf - 1;

            float xr = xf - xi;
            float yr = yf - yi;
            float zr = zf - zi;

            //int xi1 = xi + 1, yi1 = yi + 1, zi1 = zi + 1;
            float xr_1 = xr - 1, yr_1 = yr - 1, zr_1 = zr - 1;

            int hashX = xi * xPrime, hashX1 = (xi + 1) * xPrime;
            int hashY = yi * yPrime, hashY1 = (yi + 1) * yPrime;
            int hashZ = zi * zPrime, hashZ1 = (zi + 1) * zPrime;

            //Calculating dot product for eahc corner vector
            float d000 = DotGradient3D_PreHashed_Performance(hashX, hashY, hashZ, xr, yr, zr);
            float d100 = DotGradient3D_PreHashed_Performance(hashX1, hashY, hashZ, xr_1, yr, zr);
            float d001 = DotGradient3D_PreHashed_Performance(hashX, hashY, hashZ1, xr, yr, zr_1);
            float d101 = DotGradient3D_PreHashed_Performance(hashX1, hashY, hashZ1, xr_1, yr, zr_1);

            float d010 = DotGradient3D_PreHashed_Performance(hashX, hashY1, hashZ, xr, yr_1, zr);
            float d110 = DotGradient3D_PreHashed_Performance(hashX1, hashY1, hashZ, xr_1, yr_1, zr);
            float d011 = DotGradient3D_PreHashed_Performance(hashX, hashY1, hashZ1, xr, yr_1, zr_1);
            float d111 = DotGradient3D_PreHashed_Performance(hashX1, hashY1, hashZ1, xr_1, yr_1, zr_1);

            float A, B, C, D, P, Q;

            /*
            A = Interpolate1D(d000, d100, xr);
            B = Interpolate1D(d010, d110, xr);
            C = Interpolate1D(d001, d101, xr);
            D = Interpolate1D(d011, d111, xr);
            
            P = Interpolate1D(A, B, yr);
            Q = Interpolate1D(C, D, yr);
            */

            float easeXT = xr * xr * (3 - 2 * xr);
            float easeYT = yr * yr * (3 - 2 * yr);

            A = InterpolatePreCalc(d000, d100, easeXT);
            B = InterpolatePreCalc(d010, d110, easeXT);
            C = InterpolatePreCalc(d001, d101, easeXT);
            D = InterpolatePreCalc(d011, d111, easeXT);

            P = InterpolatePreCalc(A, B, easeYT);
            Q = InterpolatePreCalc(C, D, easeYT);

            return Interpolate1D(P, Q, zr);
        }

        public byte GetByte(int x, int y)
        {
            return (byte)(255 * GetFloat(x, y));
        }

        public int Mod(int a, int m)
        {
            int x = a % m;
            return x > 0 ? x : x + m;
        }
        public float GetFloatLoop(float x, float y, int loop)
        {
            float xf = x / Frequency;
            float yf = y / Frequency;

            int xi = x > 0 ? (int)xf : (int)xf - 1;
            int yi = y > 0 ? (int)yf : (int)yf - 1;

            float xr = xf - xi;
            float yr = yf - yi;

            int xi1 = xi++;
            int yi1 = yi++;

            xi = Mod(xi, loop);
            yi = Mod(yi, loop);
            xi1 = Mod(xi1, loop);
            yi1 = Mod(yi1, loop);

            //Calculating dot product for each corner vector
            float d00 = DotGradient2D_Performance(xi, yi, xr, yr);
            float d10 = DotGradient2D_Performance(xi1, yi, xr - 1, yr);
            float d01 = DotGradient2D_Performance(xi, yi1, xr, yr - 1);
            float d11 = DotGradient2D_Performance(xi1, yi1, xr - 1, yr - 1);

            //Interpolating between dot products
            float botIntp, topIntp;

            botIntp = Interpolate1D(d00, d10, xr);
            topIntp = Interpolate1D(d01, d11, xr);

            return Interpolate1D(botIntp, topIntp, yr);
        }
        public float GetFloatLoop(float x, float y, float z, int loop)
        {
            float xf = x / Frequency;
            float yf = y / Frequency;
            float zf = z / Frequency;

            int xi = x > 0 ? (int)xf : (int)xf - 1;
            int yi = y > 0 ? (int)yf : (int)yf - 1;
            int zi = z > 0 ? (int)zf : (int)zf - 1;

            float xr = xf - xi;
            float yr = yf - yi;
            float zr = zf - zi;

            //int xi1 = xi + 1, yi1 = yi + 1, zi1 = zi + 1;
            float xr_1 = xr - 1, yr_1 = yr - 1, zr_1 = zr - 1;

            int hashX = Mod(xi, loop) * xPrime, hashX1 = Mod(xi + 1, loop) * xPrime;
            int hashY = Mod(yi, loop) * yPrime, hashY1 = Mod(yi + 1, loop) * yPrime;
            int hashZ = Mod(zi, loop) * zPrime, hashZ1 = Mod(zi + 1, loop) * zPrime;

            //Calculating dot product for eahc corner vector
            float d000 = DotGradient3D_PreHashed_Performance(hashX, hashY, hashZ, xr, yr, zr);
            float d100 = DotGradient3D_PreHashed_Performance(hashX1, hashY, hashZ, xr_1, yr, zr);
            float d001 = DotGradient3D_PreHashed_Performance(hashX, hashY, hashZ1, xr, yr, zr_1);
            float d101 = DotGradient3D_PreHashed_Performance(hashX1, hashY, hashZ1, xr_1, yr, zr_1);

            float d010 = DotGradient3D_PreHashed_Performance(hashX, hashY1, hashZ, xr, yr_1, zr);
            float d110 = DotGradient3D_PreHashed_Performance(hashX1, hashY1, hashZ, xr_1, yr_1, zr);
            float d011 = DotGradient3D_PreHashed_Performance(hashX, hashY1, hashZ1, xr, yr_1, zr_1);
            float d111 = DotGradient3D_PreHashed_Performance(hashX1, hashY1, hashZ1, xr_1, yr_1, zr_1);

            float A, B, C, D, P, Q;

            /*
            A = Interpolate1D(d000, d100, xr);
            B = Interpolate1D(d010, d110, xr);
            C = Interpolate1D(d001, d101, xr);
            D = Interpolate1D(d011, d111, xr);
            
            P = Interpolate1D(A, B, yr);
            Q = Interpolate1D(C, D, yr);
            */

            float easeXT = xr * xr * (3 - 2 * xr);
            float easeYT = yr * yr * (3 - 2 * yr);

            A = InterpolatePreCalc(d000, d100, easeXT);
            B = InterpolatePreCalc(d010, d110, easeXT);
            C = InterpolatePreCalc(d001, d101, easeXT);
            D = InterpolatePreCalc(d011, d111, easeXT);

            P = InterpolatePreCalc(A, B, easeYT);
            Q = InterpolatePreCalc(C, D, easeYT);

            return Interpolate1D(P, Q, zr);
        }

        #endregion

        #region Indexers
        public float this[float x, float y] => GetFloat(x, y);

        public float this[float x, float y, float z] => GetFloat(x, y, z);
        #endregion
    }
}
