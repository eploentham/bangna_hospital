<?php

$date = $_REQUEST['date'];
$hosp_code = $_REQUEST['hosp_code'];

$conn = mysql_connect("localhost", "root", "35.103232");
mysql_select_db("medicadb");
mysql_query("SET NAMES UTF8");
//echo "date ".$date."<br>";
//echo "hosp_code ".$hosp_code;
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
$resultArray = array();
//echo "aaaaa";
//if($conn === true){
    
    $sql = "select labno from mc_result where hospital_code = '" . $hosp_code . "' and  substr(updatedate,1,10) = '". $date . "'";
    //echo $sql;
    //if($result = mysql_query($conn, $sql)){
        //if(mysql_num_rows($result) > 0){
    $result = mysql_query($sql);
            //echo "bbbb";
            //echo "result ".mysql_num_rows($result);
            while($row = mysql_fetch_array($result)){
                //echo "dddd";
                $tmp = array();
                $tmp["labno"] = $row["labno"];
                array_push($resultArray,$tmp);
            }
        //$result->free();
        //}
    //}

//echo "gggggg";
    mysql_close($conn);
    header('Content-Type: application/json');
    echo json_encode($resultArray);

//}
//else{
//    echo "cccc";
//}
?>
