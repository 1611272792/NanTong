package entity;

import java.util.List;

/**
 * Created by guhh on 2018/3/21.
 */

public class ProductLineDataEntity {

    /**
     * StateCode : 100
     * ReaSon :
     * State : [{"StateId":1,"StateName":"未装配","StateColor":"#f02222"},{"StateId":2,"StateName":"正在装配","StateColor":"#4ec2a3"},{"StateId":3,"StateName":"装配完成","StateColor":"#84f230"}]
     * LineData : [{"ProductionPlanID":"10","ProductionPlanName":"数据1","ProductionPlanVersion":"zzzzz","PlanNum":"50","RealNum":"0","StartTime":"2018-08-23","EndTime":"","State":1,"TerminalID":"3","c1":"","c2":"","c3":"","c4":"","c5":"","c6":"","c7":"","c8":"","c9":"","c10":"","c11":"","c12":"","c13":"","c14":"","c15":"","c16":"","c17":"","c18":"","c19":"","c20":""}]
     */

    private int StateCode;
    private String ReaSon;
    private List<StateBean> State;
    private List<LineDataBean> LineData;

    public int getStateCode() {
        return StateCode;
    }

    public void setStateCode(int StateCode) {
        this.StateCode = StateCode;
    }

    public String getReaSon() {
        return ReaSon;
    }

    public void setReaSon(String ReaSon) {
        this.ReaSon = ReaSon;
    }

    public List<StateBean> getState() {
        return State;
    }

    public void setState(List<StateBean> State) {
        this.State = State;
    }

    public List<LineDataBean> getLineData() {
        return LineData;
    }

    public void setLineData(List<LineDataBean> LineData) {
        this.LineData = LineData;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;

        ProductLineDataEntity that = (ProductLineDataEntity) o;

        if (StateCode != that.StateCode) return false;
        if (ReaSon != null ? !ReaSon.equals(that.ReaSon) : that.ReaSon != null) return false;
        if (State != null ? !State.equals(that.State) : that.State != null) return false;
        return LineData != null ? LineData.equals(that.LineData) : that.LineData == null;
    }

    @Override
    public int hashCode() {
        int result = StateCode;
        result = 31 * result + (ReaSon != null ? ReaSon.hashCode() : 0);
        result = 31 * result + (State != null ? State.hashCode() : 0);
        result = 31 * result + (LineData != null ? LineData.hashCode() : 0);
        return result;
    }

    public static class StateBean {
        /**
         * StateId : 1
         * StateName : 未装配
         * StateColor : #f02222
         */

        private int StateId;
        private String StateName;
        private String StateColor;

        public int getStateId() {
            return StateId;
        }

        public void setStateId(int StateId) {
            this.StateId = StateId;
        }

        public String getStateName() {
            return StateName;
        }

        public void setStateName(String StateName) {
            this.StateName = StateName;
        }

        public String getStateColor() {
            return StateColor;
        }

        public void setStateColor(String StateColor) {
            this.StateColor = StateColor;
        }

        @Override
        public boolean equals(Object o) {
            if (this == o) return true;
            if (o == null || getClass() != o.getClass()) return false;

            StateBean stateBean = (StateBean) o;

            if (StateId != stateBean.StateId) return false;
            if (StateName != null ? !StateName.equals(stateBean.StateName) : stateBean.StateName != null)
                return false;
            return StateColor != null ? StateColor.equals(stateBean.StateColor) : stateBean.StateColor == null;
        }

        @Override
        public int hashCode() {
            int result = StateId;
            result = 31 * result + (StateName != null ? StateName.hashCode() : 0);
            result = 31 * result + (StateColor != null ? StateColor.hashCode() : 0);
            return result;
        }
    }

    public static class LineDataBean {
        /**
         * ProductionPlanID : 10
         * ProductionPlanName : 数据1
         * ProductionPlanVersion : zzzzz
         * PlanNum : 50
         * RealNum : 0
         * StartTime : 2018-08-23
         * EndTime :
         * State : 1
         * TerminalID : 3
         * c1 :
         * c2 :
         * c3 :
         * c4 :
         * c5 :
         * c6 :
         * c7 :
         * c8 :
         * c9 :
         * c10 :
         * c11 :
         * c12 :
         * c13 :
         * c14 :
         * c15 :
         * c16 :
         * c17 :
         * c18 :
         * c19 :
         * c20 :
         */

        private String ProductionPlanID;
        private String ProductionPlanName;
        private String ProductionPlanVersion;
        private String PlanNum;
        private String RealNum;
        private String StartTime;
        private String EndTime;
        private String Customer;
        private String ProjectNo;
        private int State;
        private String TerminalID;
        private String c1;
        private String c2;
        private String c3;
        private String c4;
        private String c5;
        private String c6;
        private String c7;
        private String c8;
        private String c9;
        private String c10;
        private String c11;
        private String c12;
        private String c13;
        private String c14;
        private String c15;
        private String c16;
        private String c17;
        private String c18;
        private String c19;
        private String c20;
        private String c21;
        private String c22;
        private String c23;
        private String c24;
        private String c25;
        private String c26;
        private String c27;
        private String c28;
        private String c29;
        private String c30;
        private String c31;
        private String c32;
        private String c33;
        private String c34;
        private String c35;
        private String c36;
        private String c37;
        private String c38;
        private String c39;
        private String c40;
        private String c41;
        private String c42;
        private String c43;
        private String c44;
        private String c45;
        private String c46;
        private String c47;
        private String c48;
        private String c49;
        private String c50;




        public int ProcessId ;
        public String NextProcessId;
        public String NextTerminalId ;
        public String NextStateId ;

        public String getCustomer() {
            return Customer;
        }

        public void setCustomer(String customer) {
            Customer = customer;
        }

        public String getProjectNo() {
            return ProjectNo;
        }

        public void setProjectNo(String projectNo) {
            ProjectNo = projectNo;
        }

        public int getProcessId() {
            return ProcessId;
        }

        public void setProcessId(int processId) {
            ProcessId = processId;
        }

        public String getNextProcessId() {
            return NextProcessId;
        }

        public void setNextProcessId(String nextProcessId) {
            NextProcessId = nextProcessId;
        }

        public String getNextTerminalId() {
            return NextTerminalId;
        }

        public void setNextTerminalId(String nextTerminalId) {
            NextTerminalId = nextTerminalId;
        }

        public String getNextStateId() {
            return NextStateId;
        }

        public void setNextStateId(String nextStateId) {
            NextStateId = nextStateId;
        }

        public String getProductionPlanID() {
            return ProductionPlanID;
        }

        public void setProductionPlanID(String ProductionPlanID) {
            this.ProductionPlanID = ProductionPlanID;
        }

        public String getProductionPlanName() {
            return ProductionPlanName;
        }

        public void setProductionPlanName(String ProductionPlanName) {
            this.ProductionPlanName = ProductionPlanName;
        }

        public String getProductionPlanVersion() {
            return ProductionPlanVersion;
        }

        public void setProductionPlanVersion(String ProductionPlanVersion) {
            this.ProductionPlanVersion = ProductionPlanVersion;
        }

        public String getPlanNum() {
            return PlanNum;
        }

        public void setPlanNum(String PlanNum) {
            this.PlanNum = PlanNum;
        }

        public String getRealNum() {
            return RealNum;
        }

        public void setRealNum(String RealNum) {
            this.RealNum = RealNum;
        }

        public String getStartTime() {
            return StartTime;
        }

        public void setStartTime(String StartTime) {
            this.StartTime = StartTime;
        }

        public String getEndTime() {
            return EndTime;
        }

        public void setEndTime(String EndTime) {
            this.EndTime = EndTime;
        }

        public int getState() {
            return State;
        }

        public void setState(int State) {
            this.State = State;
        }

        public String getTerminalID() {
            return TerminalID;
        }

        public void setTerminalID(String TerminalID) {
            this.TerminalID = TerminalID;
        }

        public String getC1() {
            return c1;
        }

        public void setC1(String c1) {
            this.c1 = c1;
        }

        public String getC2() {
            return c2;
        }

        public void setC2(String c2) {
            this.c2 = c2;
        }

        public String getC3() {
            return c3;
        }

        public void setC3(String c3) {
            this.c3 = c3;
        }

        public String getC4() {
            return c4;
        }

        public void setC4(String c4) {
            this.c4 = c4;
        }

        public String getC5() {
            return c5;
        }

        public void setC5(String c5) {
            this.c5 = c5;
        }

        public String getC6() {
            return c6;
        }

        public void setC6(String c6) {
            this.c6 = c6;
        }

        public String getC7() {
            return c7;
        }

        public void setC7(String c7) {
            this.c7 = c7;
        }

        public String getC8() {
            return c8;
        }

        public void setC8(String c8) {
            this.c8 = c8;
        }

        public String getC9() {
            return c9;
        }

        public void setC9(String c9) {
            this.c9 = c9;
        }

        public String getC10() {
            return c10;
        }

        public void setC10(String c10) {
            this.c10 = c10;
        }

        public String getC11() {
            return c11;
        }

        public void setC11(String c11) {
            this.c11 = c11;
        }

        public String getC12() {
            return c12;
        }

        public void setC12(String c12) {
            this.c12 = c12;
        }

        public String getC13() {
            return c13;
        }

        public void setC13(String c13) {
            this.c13 = c13;
        }

        public String getC14() {
            return c14;
        }

        public void setC14(String c14) {
            this.c14 = c14;
        }

        public String getC15() {
            return c15;
        }

        public void setC15(String c15) {
            this.c15 = c15;
        }

        public String getC16() {
            return c16;
        }

        public void setC16(String c16) {
            this.c16 = c16;
        }

        public String getC17() {
            return c17;
        }

        public void setC17(String c17) {
            this.c17 = c17;
        }

        public String getC18() {
            return c18;
        }

        public void setC18(String c18) {
            this.c18 = c18;
        }

        public String getC19() {
            return c19;
        }

        public void setC19(String c19) {
            this.c19 = c19;
        }

        public String getC20() {
            return c20;
        }

        public void setC20(String c20) {
            this.c20 = c20;
        }

        public String getC21() {
            return c21;
        }

        public void setC21(String c21) {
            this.c21 = c21;
        }

        public String getC22() {
            return c22;
        }

        public void setC22(String c22) {
            this.c22 = c22;
        }

        public String getC23() {
            return c23;
        }

        public void setC23(String c23) {
            this.c23 = c23;
        }

        public String getC24() {
            return c24;
        }

        public void setC24(String c24) {
            this.c24 = c24;
        }

        public String getC25() {
            return c25;
        }

        public void setC25(String c25) {
            this.c25 = c25;
        }

        public String getC26() {
            return c26;
        }

        public void setC26(String c26) {
            this.c26 = c26;
        }

        public String getC27() {
            return c27;
        }

        public void setC27(String c27) {
            this.c27 = c27;
        }

        public String getC28() {
            return c28;
        }

        public void setC28(String c28) {
            this.c28 = c28;
        }

        public String getC29() {
            return c29;
        }

        public void setC29(String c29) {
            this.c29 = c29;
        }

        public String getC30() {
            return c30;
        }

        public void setC30(String c30) {
            this.c30 = c30;
        }

        public String getC31() {
            return c31;
        }

        public void setC31(String c31) {
            this.c31 = c31;
        }

        public String getC32() {
            return c32;
        }

        public void setC32(String c32) {
            this.c32 = c32;
        }

        public String getC33() {
            return c33;
        }

        public void setC33(String c33) {
            this.c33 = c33;
        }

        public String getC34() {
            return c34;
        }

        public void setC34(String c34) {
            this.c34 = c34;
        }

        public String getC35() {
            return c35;
        }

        public void setC35(String c35) {
            this.c35 = c35;
        }

        public String getC36() {
            return c36;
        }

        public void setC36(String c36) {
            this.c36 = c36;
        }

        public String getC37() {
            return c37;
        }

        public void setC37(String c37) {
            this.c37 = c37;
        }

        public String getC38() {
            return c38;
        }

        public void setC38(String c38) {
            this.c38 = c38;
        }

        public String getC39() {
            return c39;
        }

        public void setC39(String c39) {
            this.c39 = c39;
        }

        public String getC40() {
            return c40;
        }

        public void setC40(String c40) {
            this.c40 = c40;
        }

        public String getC41() {
            return c41;
        }

        public void setC41(String c41) {
            this.c41 = c41;
        }

        public String getC42() {
            return c42;
        }

        public void setC42(String c42) {
            this.c42 = c42;
        }

        public String getC43() {
            return c43;
        }

        public void setC43(String c43) {
            this.c43 = c43;
        }

        public String getC44() {
            return c44;
        }

        public void setC44(String c44) {
            this.c44 = c44;
        }

        public String getC45() {
            return c45;
        }

        public void setC45(String c45) {
            this.c45 = c45;
        }

        public String getC46() {
            return c46;
        }

        public void setC46(String c46) {
            this.c46 = c46;
        }

        public String getC47() {
            return c47;
        }

        public void setC47(String c47) {
            this.c47 = c47;
        }

        public String getC48() {
            return c48;
        }

        public void setC48(String c48) {
            this.c48 = c48;
        }

        public String getC49() {
            return c49;
        }

        public void setC49(String c49) {
            this.c49 = c49;
        }

        public String getC50() {
            return c50;
        }

        public void setC50(String c50) {
            this.c50 = c50;
        }

        @Override
        public boolean equals(Object o) {
            if (this == o) return true;
            if (o == null || getClass() != o.getClass()) return false;

            LineDataBean that = (LineDataBean) o;

            if (State != that.State) return false;
            if (ProcessId != that.ProcessId) return false;
            if (ProductionPlanID != null ? !ProductionPlanID.equals(that.ProductionPlanID) : that.ProductionPlanID != null)
                return false;
            if (ProductionPlanName != null ? !ProductionPlanName.equals(that.ProductionPlanName) : that.ProductionPlanName != null)
                return false;
            if (ProductionPlanVersion != null ? !ProductionPlanVersion.equals(that.ProductionPlanVersion) : that.ProductionPlanVersion != null)
                return false;
            if (PlanNum != null ? !PlanNum.equals(that.PlanNum) : that.PlanNum != null)
                return false;
            if (RealNum != null ? !RealNum.equals(that.RealNum) : that.RealNum != null)
                return false;
            if (StartTime != null ? !StartTime.equals(that.StartTime) : that.StartTime != null)
                return false;
            if (EndTime != null ? !EndTime.equals(that.EndTime) : that.EndTime != null)
                return false;
            if (Customer != null ? !Customer.equals(that.Customer) : that.Customer != null)
                return false;
            if (ProjectNo != null ? !ProjectNo.equals(that.ProjectNo) : that.ProjectNo != null)
                return false;
            if (TerminalID != null ? !TerminalID.equals(that.TerminalID) : that.TerminalID != null)
                return false;
            if (c1 != null ? !c1.equals(that.c1) : that.c1 != null) return false;
            if (c2 != null ? !c2.equals(that.c2) : that.c2 != null) return false;
            if (c3 != null ? !c3.equals(that.c3) : that.c3 != null) return false;
            if (c4 != null ? !c4.equals(that.c4) : that.c4 != null) return false;
            if (c5 != null ? !c5.equals(that.c5) : that.c5 != null) return false;
            if (c6 != null ? !c6.equals(that.c6) : that.c6 != null) return false;
            if (c7 != null ? !c7.equals(that.c7) : that.c7 != null) return false;
            if (c8 != null ? !c8.equals(that.c8) : that.c8 != null) return false;
            if (c9 != null ? !c9.equals(that.c9) : that.c9 != null) return false;
            if (c10 != null ? !c10.equals(that.c10) : that.c10 != null) return false;
            if (c11 != null ? !c11.equals(that.c11) : that.c11 != null) return false;
            if (c12 != null ? !c12.equals(that.c12) : that.c12 != null) return false;
            if (c13 != null ? !c13.equals(that.c13) : that.c13 != null) return false;
            if (c14 != null ? !c14.equals(that.c14) : that.c14 != null) return false;
            if (c15 != null ? !c15.equals(that.c15) : that.c15 != null) return false;
            if (c16 != null ? !c16.equals(that.c16) : that.c16 != null) return false;
            if (c17 != null ? !c17.equals(that.c17) : that.c17 != null) return false;
            if (c18 != null ? !c18.equals(that.c18) : that.c18 != null) return false;
            if (c19 != null ? !c19.equals(that.c19) : that.c19 != null) return false;
            if (c20 != null ? !c20.equals(that.c20) : that.c20 != null) return false;
            if (c21 != null ? !c21.equals(that.c21) : that.c21 != null) return false;
            if (c22 != null ? !c22.equals(that.c22) : that.c22 != null) return false;
            if (c23 != null ? !c23.equals(that.c23) : that.c23 != null) return false;
            if (c24 != null ? !c24.equals(that.c24) : that.c24 != null) return false;
            if (c25 != null ? !c25.equals(that.c25) : that.c25 != null) return false;
            if (c26 != null ? !c26.equals(that.c26) : that.c26 != null) return false;
            if (c27 != null ? !c27.equals(that.c27) : that.c27 != null) return false;
            if (c28 != null ? !c28.equals(that.c28) : that.c28 != null) return false;
            if (c29 != null ? !c29.equals(that.c29) : that.c29 != null) return false;
            if (c30 != null ? !c30.equals(that.c30) : that.c30 != null) return false;
            if (c31 != null ? !c31.equals(that.c31) : that.c31 != null) return false;
            if (c32 != null ? !c32.equals(that.c32) : that.c32 != null) return false;
            if (c33 != null ? !c33.equals(that.c33) : that.c33 != null) return false;
            if (c34 != null ? !c34.equals(that.c34) : that.c34 != null) return false;
            if (c35 != null ? !c35.equals(that.c35) : that.c35 != null) return false;
            if (c36 != null ? !c36.equals(that.c36) : that.c36 != null) return false;
            if (c37 != null ? !c37.equals(that.c37) : that.c37 != null) return false;
            if (c38 != null ? !c38.equals(that.c38) : that.c38 != null) return false;
            if (c39 != null ? !c39.equals(that.c39) : that.c39 != null) return false;
            if (c40 != null ? !c40.equals(that.c40) : that.c40 != null) return false;
            if (c41 != null ? !c41.equals(that.c41) : that.c41 != null) return false;
            if (c42 != null ? !c42.equals(that.c42) : that.c42 != null) return false;
            if (c43 != null ? !c43.equals(that.c43) : that.c43 != null) return false;
            if (c44 != null ? !c44.equals(that.c44) : that.c44 != null) return false;
            if (c45 != null ? !c45.equals(that.c45) : that.c45 != null) return false;
            if (c46 != null ? !c46.equals(that.c46) : that.c46 != null) return false;
            if (c47 != null ? !c47.equals(that.c47) : that.c47 != null) return false;
            if (c48 != null ? !c48.equals(that.c48) : that.c48 != null) return false;
            if (c49 != null ? !c49.equals(that.c49) : that.c49 != null) return false;
            if (c50 != null ? !c50.equals(that.c50) : that.c50 != null) return false;
            if (NextProcessId != null ? !NextProcessId.equals(that.NextProcessId) : that.NextProcessId != null)
                return false;
            if (NextTerminalId != null ? !NextTerminalId.equals(that.NextTerminalId) : that.NextTerminalId != null)
                return false;
            return NextStateId != null ? NextStateId.equals(that.NextStateId) : that.NextStateId == null;
        }

        @Override
        public int hashCode() {
            int result = ProductionPlanID != null ? ProductionPlanID.hashCode() : 0;
            result = 31 * result + (ProductionPlanName != null ? ProductionPlanName.hashCode() : 0);
            result = 31 * result + (ProductionPlanVersion != null ? ProductionPlanVersion.hashCode() : 0);
            result = 31 * result + (PlanNum != null ? PlanNum.hashCode() : 0);
            result = 31 * result + (RealNum != null ? RealNum.hashCode() : 0);
            result = 31 * result + (StartTime != null ? StartTime.hashCode() : 0);
            result = 31 * result + (EndTime != null ? EndTime.hashCode() : 0);
            result = 31 * result + (Customer != null ? Customer.hashCode() : 0);
            result = 31 * result + (ProjectNo != null ? ProjectNo.hashCode() : 0);
            result = 31 * result + State;
            result = 31 * result + (TerminalID != null ? TerminalID.hashCode() : 0);
            result = 31 * result + (c1 != null ? c1.hashCode() : 0);
            result = 31 * result + (c2 != null ? c2.hashCode() : 0);
            result = 31 * result + (c3 != null ? c3.hashCode() : 0);
            result = 31 * result + (c4 != null ? c4.hashCode() : 0);
            result = 31 * result + (c5 != null ? c5.hashCode() : 0);
            result = 31 * result + (c6 != null ? c6.hashCode() : 0);
            result = 31 * result + (c7 != null ? c7.hashCode() : 0);
            result = 31 * result + (c8 != null ? c8.hashCode() : 0);
            result = 31 * result + (c9 != null ? c9.hashCode() : 0);
            result = 31 * result + (c10 != null ? c10.hashCode() : 0);
            result = 31 * result + (c11 != null ? c11.hashCode() : 0);
            result = 31 * result + (c12 != null ? c12.hashCode() : 0);
            result = 31 * result + (c13 != null ? c13.hashCode() : 0);
            result = 31 * result + (c14 != null ? c14.hashCode() : 0);
            result = 31 * result + (c15 != null ? c15.hashCode() : 0);
            result = 31 * result + (c16 != null ? c16.hashCode() : 0);
            result = 31 * result + (c17 != null ? c17.hashCode() : 0);
            result = 31 * result + (c18 != null ? c18.hashCode() : 0);
            result = 31 * result + (c19 != null ? c19.hashCode() : 0);
            result = 31 * result + (c20 != null ? c20.hashCode() : 0);
            result = 31 * result + (c21 != null ? c21.hashCode() : 0);
            result = 31 * result + (c22 != null ? c22.hashCode() : 0);
            result = 31 * result + (c23 != null ? c23.hashCode() : 0);
            result = 31 * result + (c24 != null ? c24.hashCode() : 0);
            result = 31 * result + (c25 != null ? c25.hashCode() : 0);
            result = 31 * result + (c26 != null ? c26.hashCode() : 0);
            result = 31 * result + (c27 != null ? c27.hashCode() : 0);
            result = 31 * result + (c28 != null ? c28.hashCode() : 0);
            result = 31 * result + (c29 != null ? c29.hashCode() : 0);
            result = 31 * result + (c30 != null ? c30.hashCode() : 0);
            result = 31 * result + (c31 != null ? c31.hashCode() : 0);
            result = 31 * result + (c32 != null ? c32.hashCode() : 0);
            result = 31 * result + (c33 != null ? c33.hashCode() : 0);
            result = 31 * result + (c34 != null ? c34.hashCode() : 0);
            result = 31 * result + (c35 != null ? c35.hashCode() : 0);
            result = 31 * result + (c36 != null ? c36.hashCode() : 0);
            result = 31 * result + (c37 != null ? c37.hashCode() : 0);
            result = 31 * result + (c38 != null ? c38.hashCode() : 0);
            result = 31 * result + (c39 != null ? c39.hashCode() : 0);
            result = 31 * result + (c40 != null ? c40.hashCode() : 0);
            result = 31 * result + (c41 != null ? c41.hashCode() : 0);
            result = 31 * result + (c42 != null ? c42.hashCode() : 0);
            result = 31 * result + (c43 != null ? c43.hashCode() : 0);
            result = 31 * result + (c44 != null ? c44.hashCode() : 0);
            result = 31 * result + (c45 != null ? c45.hashCode() : 0);
            result = 31 * result + (c46 != null ? c46.hashCode() : 0);
            result = 31 * result + (c47 != null ? c47.hashCode() : 0);
            result = 31 * result + (c48 != null ? c48.hashCode() : 0);
            result = 31 * result + (c49 != null ? c49.hashCode() : 0);
            result = 31 * result + (c50 != null ? c50.hashCode() : 0);
            result = 31 * result + ProcessId;
            result = 31 * result + (NextProcessId != null ? NextProcessId.hashCode() : 0);
            result = 31 * result + (NextTerminalId != null ? NextTerminalId.hashCode() : 0);
            result = 31 * result + (NextStateId != null ? NextStateId.hashCode() : 0);
            return result;
        }
    }
}