package entity;

/**
 * Created by guhh on 2018/2/27.
 */

public class BaseResultBean {

    /**
     * StateCode : 100
     * Reason :
     */

    private int StateCode;
    private String Reason;



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

    @Override
    public String toString() {
        return "BaseResultBean{" +
                "StateCode=" + StateCode +
                ", Reason='" + Reason + '\'' +
                '}';
    }
}
