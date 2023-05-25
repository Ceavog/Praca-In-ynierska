import ModalBox from "components/modalBox";
import { useRoutes } from "react-router-dom";
import routes from "router";

const App = () => {
  const content = useRoutes(routes);
  return (
    <>
      <ModalBox />
      {content}
    </>
  );
};

export default App;
