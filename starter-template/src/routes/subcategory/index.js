import {
  Button,
  Card,
  Col,
  Divider,
  Form,
  Input,
  Row,
  Select,
  Space,
  Table,
} from "antd";
import React from "react";
import Dynamic from "../../components/table/Data/Dynamic";
import Icon from "@ant-design/icons/lib/components/Icon";
const SubCategory = () => {
  const onFinishFailed = (errorInfo) => {};

  const onFinish = (values) => {
    console.log(values);
  };

  const columns = [
    {
      title: "Name",
      dataIndex: "name",
      key: "name",
      width: 150,
      render: (text) => <span className="gx-link">{text}</span>,
    },
    {
      title: "Age",
      dataIndex: "age",
      key: "age",
      width: 70,
    },
    {
      title: "Address",
      dataIndex: "address",
      key: "address",
    },
    {
      title: "Action",
      key: "action",
      width: 360,
      render: (text, record) => (
        <span>
          <span className="gx-link">Action 一 {record.name}</span>
          <Divider type="vertical" />
          <span className="gx-link">Delete</span>
          <Divider type="vertical" />
          <span className="gx-link ant-dropdown-link">
            More actions <Icon type="down" />
          </span>
        </span>
      ),
    },
  ];

  const data = [];
  for (let i = 1; i <= 10; i++) {
    data.push({
      key: i,
      name: "John Brown",
      age: `${i}2`,
      address: `New York No. ${i} Lake Park`,
      description: `My name is John Brown, I am ${i}2 years old, living in New York No. ${i} Lake Park.`,
    });
  }

  return (
    <>
      <Row>
        <Col xl={6} lg={6} md={24} sm={24} xs={24}>
          <Card>
            <Form name="validateOnly" layout="vertical" autoComplete="off">
              <Form.Item
                name="category"
                label="หมวดหมู่สินค้า"
                rules={[
                  {
                    required: true,
                  },
                ]}
              >
                <Select>
                  <Select.Option value="demo">Demo</Select.Option>
                </Select>
              </Form.Item>
              <Form.Item
                name="name"
                label="ชื่อประเภทสินค้า"
                rules={[
                  {
                    required: true,
                  },
                ]}
              >
                <Input />
              </Form.Item>
              <Form.Item className="align-center">
                <Row
                  type="flex"
                  justify="center"
                  align="middle"
                  className="container"
                >
                  <Button type="primary" className="gx-mb-0" htmlType="submit">
                    บันทึก
                  </Button>
                </Row>
              </Form.Item>
            </Form>
          </Card>
        </Col>
        <Col xl={18} lg={18} md={24} sm={24} xs={24}>
          <Card title="ประเภทสินค้า">
            <Table
              className="gx-table-responsive"
              columns={columns}
              dataSource={data}
            />
          </Card>
        </Col>
      </Row>
    </>
  );
};

export default SubCategory;
