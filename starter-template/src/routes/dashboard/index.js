import { Card, Col, DatePicker, Row } from "antd";
import React, { useEffect } from "react";
import SignIn from "../../containers/SignIn";
// import ChartCard from "../../../../app/components/dashboard/Crypto/ChartCard";
import ChartCard from "../../components/dashboard/Crypto/ChartCard";
import { authUser } from "../../constanst";
import Widget from "../../components/Widget/index";
import dayjs from "dayjs";

const { RangePicker } = DatePicker;
const Dashboard = () => {
  const onChange = (date) => {
    if (date) {
      console.log("Date: ", date);
    } else {
      console.log("Clear");
    }
  };
  const onRangeChange = (dates, dateStrings) => {
    if (dates) {
      console.log("From: ", dates[0], ", to: ", dates[1]);
      console.log("From: ", dateStrings[0], ", to: ", dateStrings[1]);
    } else {
      console.log("Clear");
    }
  };

  const rangePresets = [
    {
      label: "Last 7 Days",
      value: [dayjs().add(-7, "d"), dayjs()],
    },
    {
      label: "Last 14 Days",
      value: [dayjs().add(-14, "d"), dayjs()],
    },
    {
      label: "Last 30 Days",
      value: [dayjs().add(-30, "d"), dayjs()],
    },
    {
      label: "Last 90 Days",
      value: [dayjs().add(-90, "d"), dayjs()],
    },
  ];
  useEffect(() => {
    if (!authUser) {
      return <SignIn />;
    }
  }, []);
  return (
    <>
      <Widget
        extra={<RangePicker presets={rangePresets} onChange={onRangeChange} />}
        title="กรองข้อมูลตามระยะเวลา"
      ></Widget>
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
