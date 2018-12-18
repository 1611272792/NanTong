package entity;

import java.util.List;

/**
 * Created by guhh on 2018/3/27.
 */

public class NewDataEntity {


    /**
     * ProductLineID : 1
     * NewData : [{" PlanID":1," FieldID":1,"Data":"test"},{" PlanID":2," FieldID":1,"Data":"test"},{" PlanID":2," FieldID":2,"Data":"test"},{" PlanID":3," FieldID":1,"Data":"test"}]
     */

    private int ProductLineID;
    private ProductLineDataEntity.LineDataBean NewData;

    public static class NewDataBean {
        /**
         *  PlanID : 1
         *  FieldID : 1
         * Data : test
         */

        private String PlanID;
        private int FieldID;
        private String Data;

        public String getPlanID() {
            return PlanID;
        }

        public void setPlanID(String planID) {
            PlanID = planID;
        }

        public int getFieldID() {
            return FieldID;
        }

        public void setFieldID(int fieldID) {
            FieldID = fieldID;
        }

        public String getData() {
            return Data;
        }

        public void setData(String data) {
            Data = data;
        }
    }

    public int getProductLineID() {
        return ProductLineID;
    }

    public void setProductLineID(int productLineID) {
        ProductLineID = productLineID;
    }

    public ProductLineDataEntity.LineDataBean getNewData() {
        return NewData;
    }

    public void setNewData(ProductLineDataEntity.LineDataBean newData) {
        NewData = newData;
    }
}
