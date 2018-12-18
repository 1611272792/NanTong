package entity;

/**
 * Created by guhh on 2018/5/17.
 */
public class GetPwdResultEntity {

    /**
     * StateCode : 100
     * Reason :
     * pwd : 123456
     */

    private int StateCode;
    private String Reason;
    private String pwd;

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

    public String getPwd() {
        return pwd;
    }

    public void setPwd(String pwd) {
        this.pwd = pwd;
    }
}
