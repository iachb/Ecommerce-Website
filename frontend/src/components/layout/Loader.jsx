import React from "react";

const Loader = () => {
  return (
    <div className="flex justify-center mt-[20%]">
      <div className="h-16 w-16 animate-spin rounded-full border-4 border-brand border-t-transparent" />
    </div>
  );
};

export default Loader;
