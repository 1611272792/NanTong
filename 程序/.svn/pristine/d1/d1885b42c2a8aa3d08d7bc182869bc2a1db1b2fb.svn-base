package interfaces;

import entity.BaseResultBean;
import entity.CheckProcessEntity;
import entity.CheckVersionEntity;
import entity.CompanyInfoEntity;
import entity.GetPwdResultEntity;
import entity.MessageEntity;
import entity.ProccessEntity;
import entity.ProductLineDataEntity;
import entity.ProductLineInfoEntity;
import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Query;

/**
 * Created by guhh on 2018/2/27.
 */

public interface IApi {
    @GET("GetProductLineInfo")
    Call<ProductLineInfoEntity> getProductLineInfo(@Query("ProductLineId") int productLineId,@Query("AndroidID") String androidID);

    @GET("CollectInfo")
    Call<ProductLineDataEntity> getProductLineData();

//    @GET("CheckProcess")
//    Call<CheckProcessEntity> checkProcess(@Query("ProcessId") int ProcessId, @Query("StateId") String StateId);
//
//    @GET("GetMessage")
//    Call<MessageEntity> getMessage(@Query("ProductLineId")int productLineId ,@Query("AndroidID") String androidID);
//
//    @GET("GetCompanyInfo")
//    Call<CompanyInfoEntity> getCompanyInfo(@Query("ProductLineId")int productLineId,@Query("AndroidID") String androidID );
//
//    @GET("GetProcessTerminal")
//    Call<ProccessEntity> getAllProcess();
//
//    @GET("CheckVersion")
//    Call<CheckVersionEntity> checkVersion(@Query("TerminalType")int terminalType,@Query("VersionCode")int versionCode);
//
//    @GET("AlterProductionData")
//    Call<BaseResultBean> alterProductionData(@Query("ProductLineID") int productLineID ,@Query("AndroidID") String androidID,@Query("NewData") String newDataEntity);
//
//    @GET("GetProductionPwd")
//    Call<GetPwdResultEntity> getPwd(@Query("ProductLineID") int productLineID ,@Query("AndroidID") String androidID);


}
