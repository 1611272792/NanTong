package entity;

import java.util.List;

/**
 * Created by guhh on 2018/3/22.
 */

public class ProccessEntity {

    /**
     * StateCode : 100
     * ReaSon :
     * ProcessList : [{"ProcessId":2,"ProcessName":"装配","TerminalList":[{"TerminalID":2,"TerminalName":"装配一线"},{"TerminalID":3,"TerminalName":"装配二线"}]},{"ProcessId":2,"ProcessName":"仓库","TerminalList":[{"TerminalID":2,"TerminalName":"仓库一线"},{"TerminalID":3,"TerminalName":"仓库二线"}]}]
     */

    private int StateCode;
    private String ReaSon;
    private List<ProcessListBean> ProcessList;

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

    public List<ProcessListBean> getProcessList() {
        return ProcessList;
    }

    public void setProcessList(List<ProcessListBean> ProcessList) {
        this.ProcessList = ProcessList;
    }

    public static class ProcessListBean {
        /**
         * ProcessId : 2
         * ProcessName : 装配
         * TerminalList : [{"TerminalID":2,"TerminalName":"装配一线"},{"TerminalID":3,"TerminalName":"装配二线"}]
         */

        private int ProcessId;
        private String ProcessName;
        private List<TerminalListBean> TerminalList;

        public int getProcessId() {
            return ProcessId;
        }

        public void setProcessId(int ProcessId) {
            this.ProcessId = ProcessId;
        }

        public String getProcessName() {
            return ProcessName;
        }

        public void setProcessName(String ProcessName) {
            this.ProcessName = ProcessName;
        }

        public List<TerminalListBean> getTerminalList() {
            return TerminalList;
        }

        public void setTerminalList(List<TerminalListBean> TerminalList) {
            this.TerminalList = TerminalList;
        }

        public static class TerminalListBean {
            /**
             * TerminalID : 2
             * TerminalName : 装配一线
             */

            private int TerminalID;
            private String TerminalName;
            private int PageTime;

            public int getPageTime() {
                return PageTime;
            }

            public void setPageTime(int pageTime) {
                PageTime = pageTime;
            }

            public int getTerminalID() {
                return TerminalID;
            }

            public void setTerminalID(int TerminalID) {
                this.TerminalID = TerminalID;
            }

            public String getTerminalName() {
                return TerminalName;
            }

            public void setTerminalName(String TerminalName) {
                this.TerminalName = TerminalName;
            }
        }
    }
}
