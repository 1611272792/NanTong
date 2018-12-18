package entity;

/**
 * Created by guhh on 2018/3/21.
 */

public class CompanyInfoEntity {

    /**
     * StateCode : 100
     * Reason :
     * Info : {"LogoUrl":"http://xxxx//xxxxxx.jpg","CompanyName":"123"}
     */

    private int StateCode;
    private String Reason;
    private InfoBean Info;

    public int getStateCode() {
        return StateCode;
    }

    public void setStateCode(int StateCode) {
        this.StateCode = StateCode;
    }

    public String getReason() {
        return Reason;
    }

    public void setReason(String Reason) {
        this.Reason = Reason;
    }

    public InfoBean getInfo() {
        return Info;
    }

    public void setInfo(InfoBean Info) {
        this.Info = Info;
    }

    public static class InfoBean {
        /**
         * LogoUrl : http://xxxx//xxxxxx.jpg
         * CompanyName : 123
         */

        private String LogoUrl;
        private String CompanyName;

        public String getLogoUrl() {
            return LogoUrl;
        }

        public void setLogoUrl(String LogoUrl) {
            this.LogoUrl = LogoUrl;
        }

        public String getCompanyName() {
            return CompanyName;
        }

        public void setCompanyName(String CompanyName) {
            this.CompanyName = CompanyName;
        }
    }
}
