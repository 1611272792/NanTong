package entity;

import java.util.List;

/**
 * Created by guhh on 2018/5/18.
 */
public class CheckPwdEntity {

    /**
     * StationCode : A001
     * Mac : 2A:FF:FF:FF
     * CheckResult : [{"ID":1,"Result":0},{"ID":2,"Result":1},{"ID":3,"Result":1}]
     * CheckPerson : 小明
     * CheckDate : 2018-08-09
     */

    private String StationCode;
    private String Mac;
    private String CheckPerson;
    private String CheckDate;
    private List<CheckResultBean> CheckResult;

    public String getStationCode() {
        return StationCode;
    }

    public void setStationCode(String StationCode) {
        this.StationCode = StationCode;
    }

    public String getMac() {
        return Mac;
    }

    public void setMac(String Mac) {
        this.Mac = Mac;
    }

    public String getCheckPerson() {
        return CheckPerson;
    }

    public void setCheckPerson(String CheckPerson) {
        this.CheckPerson = CheckPerson;
    }

    public String getCheckDate() {
        return CheckDate;
    }

    public void setCheckDate(String CheckDate) {
        this.CheckDate = CheckDate;
    }

    public List<CheckResultBean> getCheckResult() {
        return CheckResult;
    }

    public void setCheckResult(List<CheckResultBean> CheckResult) {
        this.CheckResult = CheckResult;
    }

    public static class CheckResultBean {
        /**
         * ID : 1
         * Result : 0
         */

        private int ID;
        private int Result;

        public int getID() {
            return ID;
        }

        public void setID(int ID) {
            this.ID = ID;
        }

        public int getResult() {
            return Result;
        }

        public void setResult(int Result) {
            this.Result = Result;
        }
    }
}
