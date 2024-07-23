"use client";

import { Pagination } from "flowbite-react";
import React, { useState } from "react";

type Props = {
  pageNumber: number;
  setPageNumber: (page: number) => void;
  pageCount: number;
};

export default function AppPagination({
  pageNumber,
  setPageNumber,
  pageCount,
}: Props) {
  return (
    <Pagination
      currentPage={pageNumber}
      onPageChange={(e) => setPageNumber(e)}
      totalPages={pageCount}
      layout="pagination"
      showIcons={true}
      className="text-blue-500 mb-5"
    ></Pagination>
  );
}
