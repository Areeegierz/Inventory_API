import {
  Button,
  Card,
  Col,
  Form,
  Input,
  Modal,
  Popconfirm,
  Row,
  Table,
  message,
} from "antd";
import axios from "axios";
import React, { useEffect, useState } from "react";
import { API_URL, authUser } from "../../constanst";
import { DeleteOutlined, FormOutlined } from "@ant-design/icons";
import moment from "moment";
import CategoryEdit from "./Edit";

const Category = () => {
  const [data, setData] = useState([]);
  const [dataModal, setDataModal] = useState([]);

  const [isModalOpen, setIsModalOpen] = useState(false);


  const columns = [
    {
      title: "ชื่อหมวดหมู่",
      dataIndex: "name",
      key: "name",
      width: 360,
      render: (text) => <span className="gx-link">{text}</span>,
    },
    {
      title: "เข้าร่วม",
      dataIndex: "createDate",
      key: "joindate",
      width: 360,
    },
    {
      title: "Action",
      key: "action",
      width: 360,
      render: (text, record) => (
        <div>
          <Button
            style={{ color: "#286efb" }}
            icon={<FormOutlined />}
            type="link"
            onClick={()=>showModal(record)}
          ></Button>
          
          <Popconfirm
            title={`คุณต้องการลบหมวดหมู่อะไหล่นี้ใช่หรือไม่?`}
            okText="Yes"
            cancelText="No"
            onConfirm={() => {
              axios
                .post(API_URL + "/api/Category/Remove/" + record.id)
                .then((res) => {
                  console.log("delete category", res);
                  window.location.reload();
                  message.success(`Delete ${record.date}!`);
                });
            }}
          >
            <Button
              style={{ color: "#FF4141", textAlign: "right" }}
              icon={<DeleteOutlined />}
              type="link"
            ></Button>
          </Popconfirm>
        </div>
      ),
    },
  ];



  const showModal = (record) => {
    setDataModal({id:record.id,name:record.name})
    setIsModalOpen(true);
  };

  const handleOk = () => {
    setIsModalOpen(false);
  };

  const handleCancel = () => {
    setIsModalOpen(false);
  };

  function setDataTable() {
    axios.get(API_URL + "/api/Category/GetCategory").then((res) => {
      console.log("outputget category", res);
      res.data.data.map((el) => {
        let date = moment(new Date(el.createDate));
        el.createDate = date.format("DD/MM/YYYY");
      });
      setData(res.data.data);
    });
  }

  useEffect(() => {
    setDataTable();
  }, []);

  const onFinishFailed = (errorInfo) => {};

  const onFinish = (values) => {
    const category = {
      name: values.name,
      createBy: authUser.id,
    };
    axios.post(API_URL + "/api/Category/Create", category).then((res) => {
      console.log(res);
      window.location.reload();
    });
  };

  return (
    <>
    
      <Row>
        {/* {JSON.stringify(dataModal)} */}
        <Col xl={6} lg={6} md={24} sm={24} xs={24}>
          <Card>
            <Form
              name="validateOnly"
              layout="vertical"
              autoComplete="off"
              initialValues={{ remember: true }}
              onFinish={onFinish}
              onFinishFailed={onFinishFailed}
              className="gx-signin-form gx-form-row0"
            >
              <Form.Item
                name="name"
                label="ชื่อหมวดหมู่อะไหล่"
                rules={[
                  {
                    required: true,
                  },
                ]}
              >
                <Input placeholder="ชื่อหมวดหมู่อะไหล่"/>
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
          <Card title="หมวดหมู่สินค้า">
            <Table
              className="gx-table-responsive"
              columns={columns}
              dataSource={data}
            />
            <Modal
            title="แก้ไขข้อมูลหมวดหมู่อะไหล่"
            open={isModalOpen}
            onCancel={handleCancel}
            footer={null}
          >
            <CategoryEdit data={dataModal}/>
          </Modal>
          </Card>
        </Col>
      </Row>
    </>
  );
};

export default Category;
