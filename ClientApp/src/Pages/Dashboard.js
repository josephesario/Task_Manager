import React, { useState, useEffect } from "react";
import Completed_task from "./Completed_task"
import To_do from "./To_do"

const Dashboard = () => {
 

    return (
        <>
            <div>
                <Completed_task />
                <To_do />
            </div>
        </>
    );
};

export default Dashboard;
