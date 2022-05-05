using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{

    public string AppID;
    public string BannerAddID;
    public string InterstitialAddID;

    public AdPosition BanerPosition;
    public bool TestDevice = false;

    public static AdManager instance;

    private BannerView _bannerView;
    private InterstitialAd _interstitial;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private string GetAppID()
    {
        return AppID;
    }


    // Start is called before the first frame update
    void Start()
    {
        if (TestDevice)
        {
            List<string> devices = new List<string>();
            devices.Add(SystemInfo.deviceUniqueIdentifier);
            RequestConfiguration configuration = new RequestConfiguration.Builder().SetTestDeviceIds(devices).build();
            MobileAds.SetRequestConfiguration(configuration);
            //request = new AdRequest.Builder().Build();
        }
        else
        {
            RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetSameAppKeyEnabled(true).build();
            MobileAds.SetRequestConfiguration(requestConfiguration);
        }
        MobileAds.Initialize(initStatus => {
            this.CreateBanner(CreateRequest());
            this.CreateInterstitialAd(CreateRequest());
        });

        

    }

    private AdRequest CreateRequest()
    {
        AdRequest request;
        request = new AdRequest.Builder().Build();
        return request;
    }

    #region InterstitialAd

    public void CreateInterstitialAd(AdRequest request)
    {
        this._interstitial = new InterstitialAd(InterstitialAddID);
        this._interstitial.LoadAd(request);
    }

    public void ShowInterstitialAd()
    {
        if (this._interstitial.IsLoaded())
        {
            this._interstitial.Show();
        }
        this._interstitial.LoadAd(CreateRequest());
    }

    #endregion

    #region BannerAd

    public void CreateBanner(AdRequest request)
    {
        this._bannerView = new BannerView(BannerAddID,AdSize.SmartBanner,BanerPosition);
        this._bannerView.LoadAd(CreateRequest());
        HideBanner();
    }

    public void HideBanner()
    {
        _bannerView.Hide();
    }

    public void ShowBanner()
    {
        _bannerView.Show();
    }

    #endregion
}
