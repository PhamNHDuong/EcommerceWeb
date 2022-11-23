export const userColumns = [
  { field: "aUserId", headerName: "ID", width: 70, hide: true },
  {
    field: "userName",
    headerName: "User",
    width: 230,
    flex: 1,
  },
  {
    field: "role",
    headerName: "Email",
    width: 230,
    flex: 1,
  },
  {
    field: "isDeleted",
    headerName: "Status",
    width: 160,
    flex: 1,
    renderCell: (params) => {
      let status;
      if (params.row.isDeleted) {
        status = "Disable";
      } else {
        status = "Avaiable";
      }
      return (
        <div className={`cellWithStatus ${params.row.isDeleted}`}>{status}</div>
      );
    },
  },
];

export const productColumns = [
  { field: "productId", headerName: "ID", width: 70, hide: true },
  {
    field: "name",
    headerName: "Product Name",
    width: 230,
    flex: 1,
  },
  {
    field: "price",
    headerName: "Price",
    width: 230,
    flex: 1,
  },

  {
    field: "stock",
    headerName: "Amount",
    width: 100,
    flex: 1,
  },
  {
    field: "isDeleted",
    headerName: "Status",
    width: 160,
    flex: 1,
    renderCell: (params) => {
      let status;
      if (params.row.isDeleted) {
        status = "Disable";
      } else {
        status = "Avaiable";
      }
      return (
        <div className={`cellWithStatus ${params.row.isDeleted}`}>{status}</div>
      );
    },
  },
  {
    field: "categoryName",
    headerName: "Category Name",
    width: 230,
    flex: 1,
  },
  {
    field: "dateCreated",
    headerName: "Date Created",
    width: 230,
    flex: 1,
  },
  {
    field: "dateUpdated",
    headerName: "Date Updated",
    width: 230,
    flex: 1,
  },
];

export const categoryColumns = [
  { field: "categoryId", headerName: "ID", width: 70, hide: true },
  {
    field: "name",
    headerName: "Category Name",
    width: 230,
    flex: 1,
  },
  {
    field: "isDeleted",
    headerName: "Status",
    width: 160,
    flex: 1,
    renderCell: (params) => {
      let status;
      if (params.row.isDeleted) {
        status = "Disable";
      } else {
        status = "Avaiable";
      }
      return (
        <div className={`cellWithStatus ${params.row.isDeleted}`}>{status}</div>
      );
    },
  },
  {
    field: "dateCreated",
    headerName: "Date Created",
    width: 230,
    flex: 1,
  },
  {
    field: "dateUpdated",
    headerName: "Date Updated",
    width: 230,
    flex: 1,
  },
];

//temporary data
export const userRows = [
  {
    id: 1,
    username: "Snow",
    img: "https://images.pexels.com/photos/1820770/pexels-photo-1820770.jpeg?auto=compress&cs=tinysrgb&dpr=2&w=500",
    status: "active",
    email: "1snow@gmail.com",
    age: 35,
  },
  {
    id: 2,
    username: "Jamie Lannister",
    img: "https://images.pexels.com/photos/1820770/pexels-photo-1820770.jpeg?auto=compress&cs=tinysrgb&dpr=2&w=500",
    email: "2snow@gmail.com",
    status: "passive",
    age: 42,
  },
  {
    id: 3,
    username: "Lannister",
    img: "https://images.pexels.com/photos/1820770/pexels-photo-1820770.jpeg?auto=compress&cs=tinysrgb&dpr=2&w=500",
    email: "3snow@gmail.com",
    status: "pending",
    age: 45,
  },
  {
    id: 4,
    username: "Stark",
    img: "https://images.pexels.com/photos/1820770/pexels-photo-1820770.jpeg?auto=compress&cs=tinysrgb&dpr=2&w=500",
    email: "4snow@gmail.com",
    status: "active",
    age: 16,
  },
  {
    id: 5,
    username: "Targaryen",
    img: "https://images.pexels.com/photos/1820770/pexels-photo-1820770.jpeg?auto=compress&cs=tinysrgb&dpr=2&w=500",
    email: "5snow@gmail.com",
    status: "passive",
    age: 22,
  },
  {
    id: 6,
    username: "Melisandre",
    img: "https://images.pexels.com/photos/1820770/pexels-photo-1820770.jpeg?auto=compress&cs=tinysrgb&dpr=2&w=500",
    email: "6snow@gmail.com",
    status: "active",
    age: 15,
  },
  {
    id: 7,
    username: "Clifford",
    img: "https://images.pexels.com/photos/1820770/pexels-photo-1820770.jpeg?auto=compress&cs=tinysrgb&dpr=2&w=500",
    email: "7snow@gmail.com",
    status: "passive",
    age: 44,
  },
  {
    id: 8,
    username: "Frances",
    img: "https://images.pexels.com/photos/1820770/pexels-photo-1820770.jpeg?auto=compress&cs=tinysrgb&dpr=2&w=500",
    email: "8snow@gmail.com",
    status: "active",
    age: 36,
  },
  {
    id: 9,
    username: "Roxie",
    img: "https://images.pexels.com/photos/1820770/pexels-photo-1820770.jpeg?auto=compress&cs=tinysrgb&dpr=2&w=500",
    email: "snow@gmail.com",
    status: "pending",
    age: 65,
  },
  {
    id: 10,
    username: "Roxie",
    img: "https://images.pexels.com/photos/1820770/pexels-photo-1820770.jpeg?auto=compress&cs=tinysrgb&dpr=2&w=500",
    email: "snow@gmail.com",
    status: "active",
    age: 65,
  },
];
