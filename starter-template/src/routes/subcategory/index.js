import {
  Button,
  Card,
  Col,
  Form,
  Input,
  Modal,
  Popconfirm,
  Row,
  Select,
  Table,
  message,
} from "antd";
import React, { useEffect, useState } from "react";
import { DeleteOutlined, FormOutlined } from "@ant-design/icons";
import axios from "axios";
import { API_URL, authUser } from "../../constanst";
import moment from "moment";
import CategoryEdit from "./Edit";
import SubCategoryEdit from "./Edit";
const SubCategory = () => {
  const [category, setCategory] = useState([]);
  const [dataModal, setDataModal] = useState([]);

  const [data, setData] = useState([]);
  const [isModalOpen, setIsModalOpen] = useState(false);

  const onFinishFailed = (errorInfo) => {};

  const onFinish = (values) => {
    console.log(values);
    const subcategory = {
      name: values.name,
      categoryId: values.category,
      createBy: authUser.id,
    };
    axios.post(API_URL + "/api/SubCategory/Create", subcategory).then((res) => {
      console.log(res);
      window.location.reload();
    });
  };

  const columns = [
    {
      title: "หมวดหมู่อะไหล่",
      dataIndex: "name",
      key: "name",
      width: 360,
      render: (text) => <span className="gx-link">{text}</span>,
    },
    {
      title: "ประเภทอะไหล่",
      dataIndex: "categoryName",
      key: "age",
      width: 360,
    },
    {
      title: "วันที่เข้าร่วม",
      dataIndex: "createDate",
      key: "createDate",
      width: 360,
    },
    {
      title: "Action",
      key: "action",
      width: 360,
      render: (record) => (
        <div>
          <Button
            style={{ color: "#286efb" }}
            icon={<FormOutlined />}
            type="link"
            onClick={()=>{showModal(record)}}
          ></Button>

          <Popconfirm
            title={`คุณต้องการลบหมวดหมู่อะไหล่นี้ใช่หรือไม่?`}
            okText="Yes"
            cancelText="No"
            onConfirm={() => {
              axios
                .post(API_URL + "/api/SubCategory/Remove/" + record.id)
                .then((res) => {
                  console.log("delete category", res);
                  window.location.reload();
                  message.success(`Delete ${record.date}!`);
                });
            }}
          >
            <Button
              style={{ color: "#FF4141", textAlign: "right" }}
              icon={<DeleteOutlined/>}
              type="link"
            ></Button>
          </Popconfirm>
        </div>
      ),
    },
  ];


  function getCategory() {
    axios.get(API_URL + "/api/Category/GetCategory").then((res) => {
      console.log("outputget category", res);
      res.data.data.map((el) => {
        let date = moment(new Date(el.createDate));
        el.createDate = date.format("DD/MM/YYYY");
      });
      setCategory(res.data.data);
    });
  }
  function setDataTable() {
    axios.get(API_URL + "/api/SubCategory/GetSubCategory").then((res) => {
      console.log("outputget category", res);
      res.data.data.map((el) => {
        let date = moment(new Date(el.createDate));
        el.createDate = date.format("DD/MM/YYYY");
      });
      setData(res.data.data);
    });
  }
  const showModal = (record) => {
    setDataModal({
      id:record.id,
      name:record.name,
      category:record.categoryId})
    setIsModalOpen(true);
  };

  const handleOk = () => {
    setIsModalOpen(false);
  };

  const handleCancel = () => {
    setIsModalOpen(false);
  };
  useEffect(()=>{
    setDataTable();
    getCategory();
  },[])
  return (
    <>
      <Row>
        <Col xl={6} lg={6} md={24} sm={24} xs={24}>
          <Card>
            <Form name="validateOnly" layout="vertical" autoComplete="off" onFinish={onFinish} onFinishFailed={onFinishFailed}>
              <Form.Item
                name="category"
                label="หมวดหมู่สินค้า"
                rules={[
                  {
                    required: true,
                  },
                ]}
              >
                <Select placeholder="โปรดเลือกหมวดหมู่อะไหล่">
                  {category.sort().map((item)=>(
                    <Select.Option value={item.id}>{item.name}</Select.Option>
                  ))}
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
                <Input placeholder="ชื่อประเภทสินค้า"/>
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
          <Modal
            title="แก้ไขข้อมูลหมวดหมู่อะไหล่"
            open={isModalOpen}
            onCancel={handleCancel}
            footer={null}
          >
            <SubCategoryEdit data={dataModal}/>
          </Modal>
        </Col>
      </Row>
    </>
  );
};

export default SubCategory;
