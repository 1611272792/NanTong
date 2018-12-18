package interfaces;

import java.util.List;

import entity.CheckVersionEntity;
import entity.CompanyInfoEntity;
import entity.MessageEntity;
import entity.ProductLineDataEntity;
import entity.ProductLineInfoEntity;

/**
 * Created by guhh on 2018/3/21.
 */

public interface MyDataChangeListener {
    void onNewData(ProductLineDataEntity Data);
    void onNewMessage(MessageEntity messageEntity);
    void onNewProductionInfo(ProductLineInfoEntity productLineInfoEntity);
    void onNewCompanyInfo(CompanyInfoEntity companyInfoEntity);
    void onNewVersion(CheckVersionEntity checkVersionEntity);
    void onSuccess();
    void onError(Throwable t);
}
