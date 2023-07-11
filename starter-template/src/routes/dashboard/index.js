import { Col, Row } from "antd";
import React from "react";
// import ChartCard from "../../../../app/components/dashboard/Crypto/ChartCard";
import ChartCard from "../../components/dashboard/Crypto/ChartCard";

const Dashboard = () => {
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
      {/* <Row>
        <Col xl={6} lg={12} md={12} sm={12} xs={24}>
          <Widget styleName="gx-card-full">
            <div className="gx-actchart gx-px-3 gx-pt-3">
              <div className="ant-row-flex">
                <h2 className="gx-mb-0 gx-fs-xxl gx-font-weight-medium">
                  <span
                    className={`gx-mb-0 gx-ml-2 gx-pt-xl-2 gx-fs-lg gx-chart-up`}
                  >
                    รายการเข้า
                    <i className="icon icon-menu-up gx-fs-sm" />
                  </span>
                </h2>
                <i
                  className={`icon icon-bitcoin gx-fs-xl gx-ml-auto gx-text-primary gx-fs-xxxl`}
                />
              </div>
              <p className="gx-mb-0 gx-fs-sm gx-text-grey"></p>
            </div>
          </Widget>
        </Col>
        <Col xl={6} lg={12} md={12} sm={12} xs={24}>
          b
        </Col>
        <Col xl={6} lg={12} md={12} sm={12} xs={24}>
          c
        </Col>
        <Col xl={6} lg={12} md={12} sm={12} xs={24}>
          d
        </Col>
      </Row> */}
    </>
  );
};

export default Dashboard;
