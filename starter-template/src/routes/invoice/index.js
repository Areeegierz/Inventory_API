import { Button, Card, Col, Modal, Popconfirm, Row, Table, message } from "antd";
import axios from "axios";
import { useEffect, useState } from "react";
import { API_URL, authUser } from "../../constanst";
import { DeleteOutlined, FormOutlined } from "@ant-design/icons";
import InvoiceAdd from "./add";
import InvoiceEdit from "./Edit";

const Invoice = () => {
  
const columns = [
  {
    title: "รหัสอ้างอิง",
    dataIndex: "refCode",
    width: 175,
  },
  {
    title: "รหัสอะไหล่",
    dataIndex: "code",
    width: 175,
  },
    {
      title: "ชื่ออะไหล่",
      dataIndex: "name",
      width: 150,
    },
    {
      title: "รายละเอียด",
      dataIndex: "detail",
      width: 200,
    },
    {
      title: "จำนวน",
      dataIndex: "count",
      width: 100,
    },
    {
      title: "หนวยนับ",
      dataIndex: "unit",
      width: 100,
    },
    {
      title: "Store",
      dataIndex: "storeName",
      width: 150,
    },
    {
      title: "ใช้งานที่",
      dataIndex: "plantName",
      width: 150,
    },
    {
      title: "ผู้อัปเดต",
      dataIndex: "createBy",
      width: 150,
    },
    {
      title: "Action",
      key: "action",
      width: 150,
      render: (text, record) => (
        <div>
          {/* <Button
            style={{ color: "#286efb" }}
            icon={<FormOutlined />}
            type="link"
            onClick={()=>showModal1(record)}
          ></Button> */}
  
          <Popconfirm
            title={`คุณต้องการลบหมวดหมู่อะไหล่นี้ใช่หรือไม่?`}
            okText="Yes"
            cancelText="No"
            onConfirm={() => {
              axios
                .get(API_URL + "/api/Invoice/Remove/" + record.id)
                .then((res) => {
                  console.log("delete category", res);
                  getDataTable();
                  message.success(`Delete ${record.date} success!`);
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
  function getDataTable() {
    axios.get(API_URL + "/api/Invoice/Get/"+authUser.comp).then((res) => {
      console.log(res);
      setData(res.data.data);
    });
  }

  
  const [dataModal, setDataModal] = useState([]);
  const [data, setData] = useState([]);

  const [loading, setLoading] = useState(false);
  const [open, setOpen] = useState(false);

  
  const showModal = () => {
    setOpen(true);
  };
  const handleOk = () => {
    setLoading(true);
    setTimeout(() => {
      setLoading(false);
      setOpen(false);
    }, 3000);
  };
  const handleCancel = () => {
    setOpen(false);
  };

  const [data1, setData1] = useState([]);

  const [loading1, setLoading1] = useState(false);
  const [open1, setOpen1] = useState(false);

  const showModal1 = (record) => {
    setDataModal(record)
    setOpen1(true);
  };
  const handleOk1 = () => {
    setLoading1(true);
    setTimeout(() => {
      setLoading1(false);
      setOpen1(false);
    }, 3000);
  };
  const handleCancel1 = () => {
    setOpen1(false);
  };
  useEffect(() => {
    getDataTable();
  }, []);
  return (
    <>
      <Row>
        <Col xl={24} lg={24} md={24} sm={24} xs={24}>
          <Card
            style={{ verticalAlign: "middle" }}
            title="คลังอะไหล่"
            extra={
                <button type="button" class="ant-btn ant-btn-primary" style={{marginTop:"10px"}}  onClick={showModal}><span>เพิ่มข้อมูล</span></button>
            }
          >
            <Table
              className="gx-table-responsive"
              columns={columns}
              dataSource={data}
              pagination={{ pageSize: 50 }}
              scroll={{ y: 500 }}
            />
          </Card>
        </Col>
      </Row>
      <Modal
        open={open}
        title="แบบฟอร์มเพิ่มข้อมูล"
        onOk={handleOk}
        onCancel={handleCancel}
        // width={1000}
        footer={null}
      >
        <InvoiceAdd/>
      </Modal>

      <Modal
        open={open1}
        title="แบบฟอร์มแก้ไขข้อมูล"
        onOk={handleOk1}
        onCancel={handleCancel1}
        // width={1000}
        footer={null}
      >
        <InvoiceEdit data={dataModal} />
      </Modal>
    </>
  );
};
export default Invoice;
