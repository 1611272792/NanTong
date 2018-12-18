package entity;

import java.util.List;

/**
 * Created by guhh on 2018/3/21.
 */

public class ProductLineInfoEntity {

    /**
     * StateCode : 100
     * Reason :
     * LineInfo : {"Name":"一产线","Manager":"小红","Password":"123456","Fields":[{"Field":"\u201dProductionPlanID\u201d","FieldName":"生产计划编号"},{"Field":"\u201dProductionPlanName\u201d","FieldName":"生产计划名称"},{"Field":"\u201dProductionPlanVersion\u201d","FieldName":"产品型号"},{"Field":"\u201dc1\u201d","FieldName":"添加的字段1"},{"Field":"\u201dc2\u201d","FieldName":"添加的字段2"}]}
     */

    private int StateCode;
    private String Reason;
    private LineInfoBean LineInfo;

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

    public LineInfoBean getLineInfo() {
        return LineInfo;
    }

    public void setLineInfo(LineInfoBean LineInfo) {
        this.LineInfo = LineInfo;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;

        ProductLineInfoEntity that = (ProductLineInfoEntity) o;

        if (StateCode != that.StateCode) return false;
        if (Reason != null ? !Reason.equals(that.Reason) : that.Reason != null) return false;
        return LineInfo != null ? LineInfo.equals(that.LineInfo) : that.LineInfo == null;
    }

    @Override
    public int hashCode() {
        int result = StateCode;
        result = 31 * result + (Reason != null ? Reason.hashCode() : 0);
        result = 31 * result + (LineInfo != null ? LineInfo.hashCode() : 0);
        return result;
    }

    public static class LineInfoBean {
        /**
         * Name : 一产线
         * Manager : 小红
         * Password : 123456
         * Fields : [{"Field":"\u201dProductionPlanID\u201d","FieldName":"生产计划编号"},{"Field":"\u201dProductionPlanName\u201d","FieldName":"生产计划名称"},{"Field":"\u201dProductionPlanVersion\u201d","FieldName":"产品型号"},{"Field":"\u201dc1\u201d","FieldName":"添加的字段1"},{"Field":"\u201dc2\u201d","FieldName":"添加的字段2"}]
         */

        private String Name;
        private String Manager;
        private String Password;
        private List<FieldsBean> Fields;

        public String getName() {
            return Name;
        }

        public void setName(String Name) {
            this.Name = Name;
        }

        public String getManager() {
            return Manager;
        }

        public void setManager(String Manager) {
            this.Manager = Manager;
        }

        public String getPassword() {
            return Password;
        }

        public void setPassword(String Password) {
            this.Password = Password;
        }

        public List<FieldsBean> getFields() {
            return Fields;
        }

        public void setFields(List<FieldsBean> Fields) {
            this.Fields = Fields;
        }

        @Override
        public boolean equals(Object o) {
            if (this == o) return true;
            if (o == null || getClass() != o.getClass()) return false;

            LineInfoBean that = (LineInfoBean) o;

            if (Name != null ? !Name.equals(that.Name) : that.Name != null) return false;
            if (Manager != null ? !Manager.equals(that.Manager) : that.Manager != null)
                return false;
            if (Password != null ? !Password.equals(that.Password) : that.Password != null)
                return false;
            return Fields != null ? Fields.equals(that.Fields) : that.Fields == null;
        }

        @Override
        public int hashCode() {
            int result = Name != null ? Name.hashCode() : 0;
            result = 31 * result + (Manager != null ? Manager.hashCode() : 0);
            result = 31 * result + (Password != null ? Password.hashCode() : 0);
            result = 31 * result + (Fields != null ? Fields.hashCode() : 0);
            return result;
        }

        public static class FieldsBean {
            /**
             * Field : ”ProductionPlanID”
             * FieldName : 生产计划编号
             */

            private String Field;
            private String FieldName;
            private int FieldWidth;

            public int getFieldWidth() {
                return FieldWidth;
            }

            public void setFieldWidth(int fieldWidth) {
                FieldWidth = fieldWidth;
            }

            public String getField() {
                return Field;
            }

            public void setField(String Field) {
                this.Field = Field;
            }

            public String getFieldName() {
                return FieldName;
            }

            public void setFieldName(String FieldName) {
                this.FieldName = FieldName;
            }

            @Override
            public boolean equals(Object o) {
                if (this == o) return true;
                if (o == null || getClass() != o.getClass()) return false;

                FieldsBean that = (FieldsBean) o;

                if (FieldWidth != that.FieldWidth) return false;
                if (Field != null ? !Field.equals(that.Field) : that.Field != null) return false;
                return FieldName != null ? FieldName.equals(that.FieldName) : that.FieldName == null;
            }

            @Override
            public int hashCode() {
                int result = Field != null ? Field.hashCode() : 0;
                result = 31 * result + (FieldName != null ? FieldName.hashCode() : 0);
                result = 31 * result + FieldWidth;
                return result;
            }
        }
    }
}
