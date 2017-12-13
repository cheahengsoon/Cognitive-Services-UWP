using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CogsExplorer.Modules.Translation
{ 
    public class SpeechLanguageResult
    {
        public Speech speech { get; set; }
        public Text text { get; set; }
        public Tts tts { get; set; }
    }

    public class Speech
    {
        public ArEG arEG { get; set; }
        public DeDE deDE { get; set; }
        public EnUS enUS { get; set; }
        public EsES esES { get; set; }
        public FrFR frFR { get; set; }
        public ItIT itIT { get; set; }
        public JaJP jaJP { get; set; }
        public PtBR ptBR { get; set; }
        public RuRU ruRU { get; set; }
        public ZhCN zhCN { get; set; }
        public ZhTW zhTW { get; set; }
    }

    public class ArEG
    {
        public string name { get; set; }
        public string language { get; set; }
    }

    public class DeDE
    {
        public string name { get; set; }
        public string language { get; set; }
    }

    public class EnUS
    {
        public string name { get; set; }
        public string language { get; set; }
    }

    public class EsES
    {
        public string name { get; set; }
        public string language { get; set; }
    }

    public class FrFR
    {
        public string name { get; set; }
        public string language { get; set; }
    }

    public class ItIT
    {
        public string name { get; set; }
        public string language { get; set; }
    }

    public class JaJP
    {
        public string name { get; set; }
        public string language { get; set; }
    }

    public class PtBR
    {
        public string name { get; set; }
        public string language { get; set; }
    }

    public class RuRU
    {
        public string name { get; set; }
        public string language { get; set; }
    }

    public class ZhCN
    {
        public string name { get; set; }
        public string language { get; set; }
    }

    public class ZhTW
    {
        public string name { get; set; }
        public string language { get; set; }
    }

    public class Text
    {
        public Af af { get; set; }
        public Ar ar { get; set; }
        public Bg bg { get; set; }
        public Bn bn { get; set; }
        public Bs bs { get; set; }
        public Ca ca { get; set; }
        public Cs cs { get; set; }
        public Cy cy { get; set; }
        public Da da { get; set; }
        public De de { get; set; }
        public El el { get; set; }
        public En en { get; set; }
        public Es es { get; set; }
        public Et et { get; set; }
        public Fa fa { get; set; }
        public Fi fi { get; set; }
        public Fil fil { get; set; }
        public Fj fj { get; set; }
        public Fr fr { get; set; }
        public He he { get; set; }
        public Hi hi { get; set; }
        public Hr hr { get; set; }
        public Ht ht { get; set; }
        public Hu hu { get; set; }
        public Id id { get; set; }
        public It it { get; set; }
        public Ja ja { get; set; }
        public Ko ko { get; set; }
        public Lt lt { get; set; }
        public Lv lv { get; set; }
        public Mg mg { get; set; }
        public Ms ms { get; set; }
        public Mt mt { get; set; }
        public Mww mww { get; set; }
        public Nb nb { get; set; }
        public Nl nl { get; set; }
        public Otq otq { get; set; }
        public Pl pl { get; set; }
        public Pt pt { get; set; }
        public Ro ro { get; set; }
        public Ru ru { get; set; }
        public Sk sk { get; set; }
        public Sl sl { get; set; }
        public Sm sm { get; set; }
        public SrCyrl srCyrl { get; set; }
        public SrLatn srLatn { get; set; }
        public Sv sv { get; set; }
        public Sw sw { get; set; }
        public Th th { get; set; }
        public Tlh tlh { get; set; }
        public To to { get; set; }
        public Tr tr { get; set; }
        public Ty ty { get; set; }
        public Uk uk { get; set; }
        public Ur ur { get; set; }
        public Vi vi { get; set; }
        public Yua yua { get; set; }
        public Yue yue { get; set; }
        public ZhHans zhHans { get; set; }
        public ZhHant zhHant { get; set; }
    }

    public class Af
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Ar
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Bg
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Bn
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Bs
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Ca
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Cs
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Cy
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Da
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class De
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class El
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class En
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Es
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Et
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Fa
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Fi
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Fil
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Fj
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Fr
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class He
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Hi
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Hr
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Ht
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Hu
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Id
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class It
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Ja
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Ko
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Lt
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Lv
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Mg
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Ms
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Mt
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Mww
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Nb
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Nl
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Otq
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Pl
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Pt
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Ro
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Ru
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Sk
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Sl
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Sm
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class SrCyrl
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class SrLatn
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Sv
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Sw
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Th
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Tlh
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class To
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Tr
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Ty
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Uk
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Ur
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Vi
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Yua
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Yue
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class ZhHans
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class ZhHant
    {
        public string name { get; set; }
        public string dir { get; set; }
    }

    public class Tts
    {
        public ArEGHoda arEGHoda { get; set; }
        public CaESHerena caESHerena { get; set; }
        public DaDKHelle daDKHelle { get; set; }
        public DeDEHedda deDEHedda { get; set; }
        public DeDEStefan deDEStefan { get; set; }
        public DeDEKatja deDEKatja { get; set; }
        public EnAUCatherine enAUCatherine { get; set; }
        public EnAUJames enAUJames { get; set; }
        public EnCALinda enCALinda { get; set; }
        public EnCARichard enCARichard { get; set; }
        public EnGBGeorge enGBGeorge { get; set; }
        public EnGBSusan enGBSusan { get; set; }
        public EnINHeera enINHeera { get; set; }
        public EnINRavi enINRavi { get; set; }
        public EnUSBenjaminrus enUSBenjaminRUS { get; set; }
        public EnUSJessarus enUSJessaRUS { get; set; }
        public EnUSMark enUSMark { get; set; }
        public EnUSZira enUSZira { get; set; }
        public EnUSZirarus enUSZiraRUS { get; set; }
        public EsESLaura esESLaura { get; set; }
        public EsESPablo esESPablo { get; set; }
        public EsMXRaul esMXRaul { get; set; }
        public EsMXSabina esMXSabina { get; set; }
        public FiFIHeidi fiFIHeidi { get; set; }
        public FrCACaroline frCACaroline { get; set; }
        public FrCAClaude frCAClaude { get; set; }
        public FrFRJulie frFRJulie { get; set; }
        public FrFRPaul frFRPaul { get; set; }
        public HiINKalpana hiINKalpana { get; set; }
        public ItITCosimo itITCosimo { get; set; }
        public ItITElsa itITElsa { get; set; }
        public JaJPAyumi jaJPAyumi { get; set; }
        public JaJPIchiro jaJPIchiro { get; set; }
        public JaJPWatanabe jaJPWatanabe { get; set; }
        public KoKRMinjoon koKRMinjoon { get; set; }
        public KoKRSeohyun koKRSeohyun { get; set; }
        public NbNOJon nbNOJon { get; set; }
        public NbNONina nbNONina { get; set; }
        public NlNLFrank nlNLFrank { get; set; }
        public NlNLMarijke nlNLMarijke { get; set; }
        public PlPLAdam plPLAdam { get; set; }
        public PlPLPaulina plPLPaulina { get; set; }
        public PtBRDaniel ptBRDaniel { get; set; }
        public PtBRMaria ptBRMaria { get; set; }
        public PtPTHelia ptPTHelia { get; set; }
        public RuRUIrina ruRUIrina { get; set; }
        public RuRUPavel ruRUPavel { get; set; }
        public SvSEBengt svSEBengt { get; set; }
        public SvSEKarin svSEKarin { get; set; }
        public TrTRSeda trTRSeda { get; set; }
        public TrTRTolga trTRTolga { get; set; }
        public ZhCNKangkang zhCNKangkang { get; set; }
        public ZhCNYaoyao zhCNYaoyao { get; set; }
        public ZhHKDanny zhHKDanny { get; set; }
        public ZhHKTracy zhHKTracy { get; set; }
        public ZhTWYating zhTWYating { get; set; }
        public ZhTWZhiwei zhTWZhiwei { get; set; }
    }

    public class ArEGHoda
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class CaESHerena
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class DaDKHelle
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class DeDEHedda
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class DeDEStefan
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class DeDEKatja
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class EnAUCatherine
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class EnAUJames
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class EnCALinda
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class EnCARichard
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class EnGBGeorge
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class EnGBSusan
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class EnINHeera
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class EnINRavi
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class EnUSBenjaminrus
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class EnUSJessarus
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class EnUSMark
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class EnUSZira
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class EnUSZirarus
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class EsESLaura
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class EsESPablo
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class EsMXRaul
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class EsMXSabina
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class FiFIHeidi
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class FrCACaroline
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class FrCAClaude
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class FrFRJulie
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class FrFRPaul
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class HiINKalpana
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class ItITCosimo
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class ItITElsa
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class JaJPAyumi
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class JaJPIchiro
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class JaJPWatanabe
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class KoKRMinjoon
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class KoKRSeohyun
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class NbNOJon
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class NbNONina
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class NlNLFrank
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class NlNLMarijke
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class PlPLAdam
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class PlPLPaulina
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class PtBRDaniel
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class PtBRMaria
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class PtPTHelia
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class RuRUIrina
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class RuRUPavel
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class SvSEBengt
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class SvSEKarin
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class TrTRSeda
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class TrTRTolga
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class ZhCNKangkang
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class ZhCNYaoyao
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class ZhHKDanny
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class ZhHKTracy
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class ZhTWYating
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

    public class ZhTWZhiwei
    {
        public string gender { get; set; }
        public string locale { get; set; }
        public string languageName { get; set; }
        public string displayName { get; set; }
        public string regionName { get; set; }
        public string language { get; set; }
    }

}
