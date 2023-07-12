import { Button, Form, Input, Row, Select } from "antd";
import { API_URL, authUser } from "../../constanst";
import axios from "axios";
import { useEffect, useState } from "react";

const InvoiceAdd = () => {
  const onFinishFailed = (errorInfo) => {};
  const onFinish = (values) => {
    console.log(values)
    const context = {
      "refCode": values.refCode,
      "code": values.code,
      "compCode": authUser.comp,
      "count": values.count,
      "use": values.use,
      "createBy":authUser.user.name
    }
    console.log(context)
    axios.post(API_URL+"/api/Invoice/Create",context).then((res)=>{
        console.log(res);
        window.location.reload();
      })
  };
  const [store,setStore] = useState([]);

  function getStore(){
    
    axios.get(API_URL + "/api/Structure/GetPlant").then((res) => {
        console.log(res.data.data)
        setStore(res.data.data)
    });
  }
  useEffect(()=>{
    getStore()
  },[])
  return (
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
      name="refCode"
      label="เลขใบเบิก"
      rules={[
        {
          required: true,
        },
      ]}
    >
      <Input />
    </Form.Item>
      <Form.Item
        name="code"
        label="รหัสอะไหล่"
        rules={[
          {
            required: true,
          },
        ]}
      >
        <Input />
      </Form.Item>
      <Form.Item
        name="name"
        label="ชื่ออะไหล่"
      >
        <Input disabled />
      </Form.Item>
      <Form.Item
        name="detail"
        label="รายละเอียด"
      >
        <Input disabled/>
      </Form.Item>
      <Form.Item
        name="count"
        label="จำนวน"
      >
        <Input type="number" />
      </Form.Item>
      <Form.Item
        name="use"
        label="ใช้ที่โรงงาน"
        rules={[
          {
            required: true,
          },
        ]}
      >
        <Select placeholder="กรุณาเลือกโรงงาน">
            {store.sort().map((item)=>(
                <Select.Option value={item.code}>{item.code+" "+item.name}</Select.Option>
            ))}
        </Select>
      </Form.Item>
      <Form.Item className="align-center">
        <Row type="flex" justify="center" align="middle" className="container">
          <Button type="primary" className="gx-mb-0" htmlType="submit">
            บันทึก
          </Button>
        </Row>
      </Form.Item>
    </Form>
  );
};

export default InvoiceAdd;
