import React from "react";
import { BranchIcon } from "../../utils/Icon.jsx";

class Footer extends React.Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    return (
      <div className="footer d-flex justify-content-evenly align-items-center p-3 text-center">
        <div className="project_intro">
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Vero minima
          laborum ipsum repudiandae assumenda laboriosam sint, voluptatem ea
          provident consectetur tempore recusandae laudantium. Explicabo cumque,
          enim omnis accusantium veniam eligendi?
        </div>
        <div>
          <img src={BranchIcon} alt="" />
        </div>
        <div className="project_info">
          Lorem ipsum dolor sit amet consectetur adipisicing elit. Omnis libero
          cum praesentium sit pariatur quisquam consequatur veniam sed dolorem
          asperiores? Eaque ducimus tempora ut sequi voluptas. Magni
          voluptatibus incidunt quo.
        </div>
      </div>
    );
  }
}

export default Footer;
