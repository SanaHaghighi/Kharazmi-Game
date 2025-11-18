using System;
using RTLTMPro;
using UnityEngine;
using UnityEngine.UI;
public class TutorialManager : SingletonBehaviour<TutorialManager>
{
    public Action onPauseGame;
    public Action onResumeGame;
    [SerializeField] GameObject TutorialArea;
    [SerializeField] GameObject TutorialButtonParent;
    [SerializeField] Button Level1TutorialButton;
    [SerializeField] Button Level2TutorialButton;
    [SerializeField] Button Level3TutorialButton;
    [SerializeField] Button Level4TutorialButton;
    [SerializeField] Button TutorialBackButton;
    [SerializeField] RTLTextMeshPro TutorialText;
    [SerializeField] Image tutorialImage;


    int mapLevel;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        TutorialBackButton.onClick.AddListener(TutorialBackButtonClicked);
        Level1TutorialButton.onClick.AddListener(Level1TutorialButtonClicked);
        Level2TutorialButton.onClick.AddListener(Level2TutorialButtonClicked);
        Level3TutorialButton.onClick.AddListener(Level3TutorialButtonClicked);
        Level4TutorialButton.onClick.AddListener(Level4TutorialButtonClicked);
        gameObject.SetActive(false);
    }
    private void Level4TutorialButtonClicked()
    {
        TutorialButtonParent.SetActive(false);
        tutorialImage.gameObject.SetActive(false);
        TutorialArea.SetActive(true);
        TutorialText.text = "ماز \r\nرسیدی به مرحله آخر، یعنی چالش تصویری معماهای! تو وارد یک ماز (هزارتوی پیچیده) می‌شوی که پر از معما و شکل‌های مختلف است.\r\nاینجا دیگر خبری از معادله‌های جبری نیست! در این چالش جذاب، باید مسائل را به صورت تصویری حل کنی و گزینه صحیح را انتخاب کنی.\r\n\r\n📐 مثال:\r\nتصور کن از تو می‌خواهند مساحت یک مستطیل را به دست بیاوری، یا زاویه‌ درست یک شکل را تشخیص دهی. سؤال‌های ساده و جذاب که باید با تمرکز حلشان کنی.\r\n\r\n🕒 پاداش:\r\nبه ازای هر جواب درست، امتیاز بیشتری به دست می‌آوری.\r\n\r\n👑 ویژه:\r\nدر این مرحله برنده شدن به دقت و شجاعت ریاضی تو بستگی دارد.\r\n«نشان‌های خوارزمی» آخرین فرصتت برای تصاحب امتیاز ویژه است!\r\n";
    }

    private void Level3TutorialButtonClicked()
    {
        TutorialButtonParent.SetActive(false);
        tutorialImage.gameObject.SetActive(false);
        TutorialArea.SetActive(true);
        TutorialText.text = "بارش عبارت‌های جبری، چیدمان کامل!\r\nحالا وقت آن رسیده که مهارت‌هایت در ساختن عبارت‌های جبری نشان داده شود. آسمان ریاضیات به روی تو باز شده و عبارت‌های کوچک جبری مثل دانه‌های باران از آسمان می‌ریزند.\r\nمأموریت تو چیست؟ تو باید با دقت و هوش، این عبارت‌ها را جمع‌آوری کنی و کنار هم بچینی تا یک عبارت جبری کامل و خواسته‌شده بسازی.\r\n\r\n🔑 نکته طلایی:\r\nکمی سرعت بیشتری به خودت بده! قطرات بارانی که نخ جمع کنی و عبارت را ناتمام بگذاری، به سرعت ناپدید می‌شوند. اگر سرعت عمل نداشته باشی، شانس برنده شدن در این مرحله را از دست خواهی داد.\r\n\r\n🎯 هدف این مرحله:\r\nعبارت‌های درست و تکمیل شده می‌توانند تو را به جایگاه یک ریاضي‌دان حرفه‌ای برسانند. هر عبارت درستی که تکمیل کنی، یک نشان خوارزمی دریافت می‌کنی!";
    }

    private void Level2TutorialButtonClicked()
    {
        TutorialButtonParent.SetActive(false);
        tutorialImage.gameObject.SetActive(false);
        TutorialArea.SetActive(true);
        TutorialText.text = "ترازو، چالش معادلات!\r\nتبریک! تو از مرحله اول با موفقیت عبور کردی و حالا باید وارد یکی از جدی‌ترین چالش‌ها شوی: معادلات جبری!\r\nدر این مرحله، مقابلت یک ترازو ظاهر می‌شود که نشان‌دهنده یک معادله جبری است. تو باید با حل درست معادلات، ترازو را به تعادل برسانی. هرچه سرعت و دقت بیشتری داشته باشی، زمان بیشتری برنده می‌شوی و نشان خوارزمی بیشتری بدست می‌آوری.\r\n\r\n🔑 نکته طلایی:\r\nیادت باشد که هم دقت مهم است و هم سرعت! اگر جوابت درست نباشد، نشان از دست می‌دهی. پس معادله‌ها را خوب تحلیل کن و با منطق پیش برو.\r\n\r\n🕒 پاداش:\r\nبه ازای هر معادله‌ای که درست حل می‌کنی، X ثانیه زمان اضافه دریافت می‌کنی.\r\nهر جواب درست یک نشان خوارزمی دارد!";
    }

    private void Level1TutorialButtonClicked()
    {
        TutorialButtonParent.SetActive(false);
        tutorialImage.gameObject.SetActive(false);
        TutorialArea.SetActive(true);
        TutorialText.text = "مرحله‌ی اول: شکار عبارت های جبری مار پرواز با کایت رؤیایی\r\nاولین چالش این بازی، پرواز هیجان‌انگیز با کایت شگفت‌انگیزت است. تو باید کایتت را هدایت کنی و عبارت‌های جبری مشابه را پیدا کنی تا دم کایتت طولانی‌تر و زیباتر شود. هرچه بتوانی عبارت‌های مشابه بیشتری را جمع کنی، زمان بیشتری برای حل چالش‌های بعدی بدست می‌آوری!\r\n\r\n🔑 نکته طلایی:\r\nتمرکز کن و به سرعت عمل کن! کایت زمانی زیبا دیده می‌شود که تو بتوانی عبارت‌های درست و متناسب را شناسایی و جمع‌آوری کنی.\r\n\r\n🕒 پاداش:\r\nبه ازای هر عبارت متشابه که به درستی پیدا می‌کنی، X ثانیه زمان اضافه دریافت می‌کنی که در مراحل بعد به دردت می‌خورد!";
    }
    public void OpenTutorial(int level = 0)
    {
        mapLevel = level;
        onPauseGame?.Invoke();
        gameObject.SetActive(true);
        TutorialButtonParent.SetActive(true);
        tutorialImage.gameObject.SetActive(true);
        TutorialArea.SetActive(false);
        if (level != 0) 
            TutorialChooser();
    }
    private void TutorialBackButtonClicked()
    {
        if (TutorialArea.activeSelf)
        {
            gameObject.SetActive(true);
            TutorialArea.SetActive(false);
            TutorialButtonParent.SetActive(true);
            tutorialImage.gameObject.SetActive(true);
            TutorialText.text = string.Empty;

        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void TutorialChooser()
    {
        switch (mapLevel) {
            case 1:
                Level1TutorialButtonClicked();
                break;
            case 2:
                Level2TutorialButtonClicked();
                break;
            case 3:
                Level3TutorialButtonClicked();
                break;
            case 4:
                Level4TutorialButtonClicked();
                break;
        }

    }

    public void CloseTutorial()
    {
        onResumeGame?.Invoke();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
