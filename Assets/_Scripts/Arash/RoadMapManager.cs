using System;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoadMapManager : SingletonBehaviour<RoadMapManager>
{
    public Action onPauseGame;
    public Action onResumeGame;
    [SerializeField] GameObject MapPanel;
    [SerializeField] GameObject RoadMapPanel;
    [SerializeField] GameObject DescriptionPanel;
    //[SerializeField] GameObject TutorialPanel;
    [SerializeField] Button MapButton;
    [SerializeField] Button DescrptionButton;
    [SerializeField] Button MapBackButton;
    [SerializeField] Button PanelBackButton;
    [SerializeField] Button DescriptionBackButton;

    [SerializeField] Sprite level0sprite;
    [SerializeField] Sprite level1sprite;
    [SerializeField] Sprite level2sprite;
    [SerializeField] Sprite level3sprite;
    [SerializeField] Sprite level4sprite;
    [SerializeField] Image mapImage;
    
    [SerializeField] RTLTextMeshPro descriptionText;
    /*[SerializeField] GameObject TutorialButtonParent;
    [SerializeField] Image tutorialImage;
    [SerializeField] GameObject TutorialArea;
    [SerializeField] RTLTextMeshPro TutorialText;
    [SerializeField] Button Level1TutorialButton;
    [SerializeField] Button Level2TutorialButton;
    [SerializeField] Button Level3TutorialButton;
    [SerializeField] Button Level4TutorialButton;
    [SerializeField] Button TutorialBackButton;*/
    int mapLevel;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        MapBackButton.onClick.AddListener(MapBackButtonClicked);
        MapButton.onClick.AddListener(ShowMap);
        PanelBackButton.onClick.AddListener(CloseRoadMap);
        DescrptionButton.onClick.AddListener(ShowDescription);
        DescriptionBackButton.onClick.AddListener(DescriptionBackButtonClicked);
        /*Level1TutorialButton.onClick.AddListener(Level1TutorialButtonClicked);
        Level2TutorialButton.onClick.AddListener(Level2TutorialButtonClicked);
        Level3TutorialButton.onClick.AddListener(Level3TutorialButtonClicked);
        Level4TutorialButton.onClick.AddListener(Level4TutorialButtonClicked);
        TutorialBackButton.onClick.AddListener(TutorialBackButtonClicked);*/
        gameObject.SetActive(false);
    }

    /*private void TutorialBackButtonClicked()
    {
        if (TutorialArea.activeSelf)
        {
            //TutorialPanel.SetActive(true);
            TutorialArea.SetActive(false);
            TutorialButtonParent.SetActive(true);
            tutorialImage.gameObject.SetActive(true);
            TutorialText.text = string.Empty;

        }
        else
        {
            //TutorialPanel.SetActive(false);
            CloseRoadMap();
        }
    }
    private void Level4TutorialButtonClicked()
    {
        TutorialButtonParent.SetActive(false);
        tutorialImage.gameObject.SetActive(false);
        TutorialArea.SetActive(true);
        TutorialText.text = "";
    }

    private void Level3TutorialButtonClicked()
    {
        TutorialButtonParent.SetActive(false);
        tutorialImage.gameObject.SetActive(false);
        TutorialArea.SetActive(true);
        TutorialText.text = "مرحله‌ی سوم: بارش عبارت‌های جبری\r\nآسمان ریاضیات به روی تو باز شده و عبارت‌های جبری مثل بارون میبارن! تو باید با دقت قطرات بارون رو جمع‌آوری کنی و با چیدنشون کنار هم، عبارت‌ جبری خواسته شده رو بسازی. هر عبارت درستی که بسازی، یک نشان خوارزمی می گیری. حواست باشه که باید سریع عمل کنی، چون قطرات بارون به سرعت ناپدید میشن!\r\n";
    }

    private void Level2TutorialButtonClicked()
    {
        TutorialButtonParent.SetActive(false);
        tutorialImage.gameObject.SetActive(false);
        TutorialArea.SetActive(true);
        TutorialText.text = "مرحله‌ی دوم: نبرد با معادلات\r\nحالا نوبت به حل کردن معادلات جبری رسیده! با حل هر معادله، زمان بیشتری به دست میاری تا در مرحله‌ی بعدی ازش استفاده کنی. هر چه سریع‌تر و دقیق‌تر معادلات رو حل کنی، امتیاز بیشتری کسب می‌کنی. (به ازای هر جواب درست ۱۰ ثانیه دریافت میکنی)";
    }

    private void Level1TutorialButtonClicked()
    {
        TutorialButtonParent.SetActive(false);
        tutorialImage.gameObject.SetActive(false);
        TutorialArea.SetActive(true);
        TutorialText.text = "مرحله‌ی اول: شکار عبارت های جبری مار\r\nدر این مرحله، یک مار شیطون دنبال عبارت‌های جبری متشابه میگرده که بخوره! تو باید هدایتش کنی تا بیشترین عبارت جبری رو بخوره. هر عبارت جبری که مار بخوره، این بازی زمان بیشتری برای حل مساله های بعدی بهت میده. (به ازای هر عبارت متشابه درست ۵ ثانیه دریافت میکنی)";
    }*/
    public void OpenRoadMap(int openType=0,int level=0)
    {
        mapLevel = level;
        onPauseGame?.Invoke();
        RoadMapPanel.SetActive(true);
        gameObject.SetActive(true);
        MapButtonClicked();
    }
    public void CloseRoadMap()
    {
        onResumeGame?.Invoke();
        gameObject.SetActive(false);
    }
    private void DescriptionBackButtonClicked()
    {
        //TutorialPanel.SetActive(false);
        RoadMapPanel.SetActive(true);
        DescriptionPanel.SetActive(false);
        MapPanel.SetActive(false);
    }
    private void DescriptionButtonClicked()
    {
        //TutorialPanel.SetActive(true);
        RoadMapPanel.SetActive(false);
        DescriptionPanel.SetActive(false);
        MapPanel.SetActive(false);
    }
    public void ShowDescription()
    {
        //TutorialPanel.SetActive(false);
        RoadMapPanel.SetActive(false);
        DescriptionPanel.SetActive(true);
        MapPanel.SetActive(false);
    }
    public void ShowMap()
    {
        //TutorialPanel.SetActive(false);
        RoadMapPanel.SetActive(false);
        DescriptionPanel.SetActive(false);
        MapPanel.SetActive(true);
    }
    private void MapButtonClicked()
    {
        RoadMapPanel.SetActive(true);
        switch (mapLevel)
        {
            case 0:
                mapImage.sprite = level0sprite;
                break;
            case 1:
                mapImage.sprite = level1sprite;
                break;
            case 2:
                mapImage.sprite = level2sprite;
                break;
            case 3:
                mapImage.sprite = level3sprite;
                break;
            case 4:
                mapImage.sprite = level4sprite;
                break;
        }
        switch (mapLevel)
        {
            case 0:
                descriptionText.text = "سلام دوست ریاضی‌دان من!\r\n\r\nتصور کن تو، یک ریاضی‌دان باهوش و کنجکاو هستی که عاشق حل کردن مساله‌های ریاضیاتی که میخوای نشان خوارزمی بگیری! تو باید با استفاده از دانش و مهارت خودت در مورد عبارت‌های جبری، مساله های مختلف رو حل کنی و بیشترین تعداد نشان خوارزمی رو دریافت کنی.";
                break;
            case 1:
                descriptionText.text = "مرحله‌ی اول: شکار عبارت های جبری مار پرواز با کایت رؤیایی\r\nاولین چالش این بازی، پرواز هیجان‌انگیز با کایت شگفت‌انگیزت است. تو باید کایتت را هدایت کنی و عبارت‌های جبری مشابه را پیدا کنی تا دم کایتت طولانی‌تر و زیباتر شود. هرچه بتوانی عبارت‌های مشابه بیشتری را جمع کنی، زمان بیشتری برای حل چالش‌های بعدی بدست می‌آوری!\r\n\r\n🔑 نکته طلایی:\r\nتمرکز کن و به سرعت عمل کن! کایت زمانی زیبا دیده می‌شود که تو بتوانی عبارت‌های درست و متناسب را شناسایی و جمع‌آوری کنی.\r\n\r\n🕒 پاداش:\r\nبه ازای هر عبارت متشابه که به درستی پیدا می‌کنی، X ثانیه زمان اضافه دریافت می‌کنی که در مراحل بعد به دردت می‌خورد!";
                break;
            case 2:
                descriptionText.text = "ترازو، چالش معادلات!\r\nتبریک! تو از مرحله اول با موفقیت عبور کردی و حالا باید وارد یکی از جدی‌ترین چالش‌ها شوی: معادلات جبری!\r\nدر این مرحله، مقابلت یک ترازو ظاهر می‌شود که نشان‌دهنده یک معادله جبری است. تو باید با حل درست معادلات، ترازو را به تعادل برسانی. هرچه سرعت و دقت بیشتری داشته باشی، زمان بیشتری برنده می‌شوی و نشان خوارزمی بیشتری بدست می‌آوری.\r\n\r\n🔑 نکته طلایی:\r\nیادت باشد که هم دقت مهم است و هم سرعت! اگر جوابت درست نباشد، نشان از دست می‌دهی. پس معادله‌ها را خوب تحلیل کن و با منطق پیش برو.\r\n\r\n🕒 پاداش:\r\nبه ازای هر معادله‌ای که درست حل می‌کنی، X ثانیه زمان اضافه دریافت می‌کنی.\r\nهر جواب درست یک نشان خوارزمی دارد!";
                break;
            case 3:
                descriptionText.text = "بارش عبارت‌های جبری، چیدمان کامل!\r\nحالا وقت آن رسیده که مهارت‌هایت در ساختن عبارت‌های جبری نشان داده شود. آسمان ریاضیات به روی تو باز شده و عبارت‌های کوچک جبری مثل دانه‌های باران از آسمان می‌ریزند.\r\nمأموریت تو چیست؟ تو باید با دقت و هوش، این عبارت‌ها را جمع‌آوری کنی و کنار هم بچینی تا یک عبارت جبری کامل و خواسته‌شده بسازی.\r\n\r\n🔑 نکته طلایی:\r\nکمی سرعت بیشتری به خودت بده! قطرات بارانی که نخ جمع کنی و عبارت را ناتمام بگذاری، به سرعت ناپدید می‌شوند. اگر سرعت عمل نداشته باشی، شانس برنده شدن در این مرحله را از دست خواهی داد.\r\n\r\n🎯 هدف این مرحله:\r\nعبارت‌های درست و تکمیل شده می‌توانند تو را به جایگاه یک ریاضي‌دان حرفه‌ای برسانند. هر عبارت درستی که تکمیل کنی، یک نشان خوارزمی دریافت می‌کنی!";
                break;
            case 4:
                descriptionText.text = "ماز \r\nرسیدی به مرحله آخر، یعنی چالش تصویری معماهای! تو وارد یک ماز (هزارتوی پیچیده) می‌شوی که پر از معما و شکل‌های مختلف است.\r\nاینجا دیگر خبری از معادله‌های جبری نیست! در این چالش جذاب، باید مسائل را به صورت تصویری حل کنی و گزینه صحیح را انتخاب کنی.\r\n\r\n📐 مثال:\r\nتصور کن از تو می‌خواهند مساحت یک مستطیل را به دست بیاوری، یا زاویه‌ درست یک شکل را تشخیص دهی. سؤال‌های ساده و جذاب که باید با تمرکز حلشان کنی.\r\n\r\n🕒 پاداش:\r\nبه ازای هر جواب درست، امتیاز بیشتری به دست می‌آوری.\r\n\r\n👑 ویژه:\r\nدر این مرحله برنده شدن به دقت و شجاعت ریاضی تو بستگی دارد.\r\n«نشان‌های خوارزمی» آخرین فرصتت برای تصاحب امتیاز ویژه است!\r\n";
                break;
        }
        //TutorialPanel.SetActive(false);
        DescriptionPanel.SetActive(false);
        MapPanel.SetActive(false);
    }

    private void MapBackButtonClicked()
    {
        //TutorialPanel.SetActive(false);
        RoadMapPanel.SetActive(true);
        DescriptionPanel.SetActive(false);
        MapPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
