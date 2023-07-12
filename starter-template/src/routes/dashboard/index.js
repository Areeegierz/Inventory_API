import { Col, Row } from "antd";
import React, { useEffect } from "react";
import SignIn from "../../containers/SignIn"
// import ChartCard from "../../../../app/components/dashboard/Crypto/ChartCard";
import ChartCard from "../../components/dashboard/Crypto/ChartCard";
import { authUser } from "../../constanst";

const Dashboard = () => {
  useEffect(()=>{
    if(!authUser){
      return <SignIn/>
    }
  },[])
  return (
    <>
      <Row>
        <Col xl={6} lg={12} md={12} sm={12} xs={24}>
          <ChartCard
            prize="1"
            title="รายการนำเข้า"
            icon="bitcoin"
            
            styleName="up"
            desc="Bitcoin Price"
          />
        </Col>
        <Col xl={6} lg={12} md={12} sm={12} xs={24}>
          <ChartCard
            prize="1"
            title="รายการเบิกออก"
            icon="etherium"
            desc="Etherium Price"
          />
        </Col>
        <Col xl={6} lg={12} md={12} sm={12} xs={24}>
          <ChartCard
            prize="1"
            title="รวมรายการวันนี้"
            icon="ripple"
            
            styleName="down"
            desc="Ripple Price"
          />
        </Col>
        <Col xl={6} lg={12} md={12} sm={12} xs={24}>
          <ChartCard
            prize="1"
            title="รายการวัสดุทั้งหมด"
            icon="litcoin"
            
            styleName="down"
            desc="Litecoin Price"
          />
        </Col>
      </Row>
    </>
  );
};

export default Dashboard;
