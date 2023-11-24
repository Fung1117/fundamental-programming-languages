<?php
define("DB_HOST", "mydb");
define("USERNAME", "dummy");
define("PASSWORD", "c3322b");
define("DB_NAME", "db3322");

session_start();

$current_time = time();

if (!isset($_SESSION['uid']) || $current_time-$_SESSION['login_time'] > 300) {
    header('Location: ../login.php');
    exit;
} else {
    display_course_information();
}

function get_enrolled_courses() {
    $id = $_SESSION['uid'];
    $conn = mysqli_connect(DB_HOST, USERNAME, PASSWORD, DB_NAME) or die('Error! '. mysqli_connect_error($conn));
    $query = "SELECT DISTINCT course FROM courseinfo WHERE uid = $id";
    $result = mysqli_query($conn, $query) or die ('Failed to query '.mysqli_error($conn));
    if (mysqli_num_rows($result) > 0) {
        while($row = mysqli_fetch_array($result)) {
            ?> <div> <a href ='getscore.php?course=<?php echo $row['course']?>'> <?php echo $row['course']?> </a> </div>
        <?php
        }
    } else {
        ?> <p> You have not enrolled in any courses in our system. <p>
        <?php
    }
    mysqli_free_result($result);
    mysqli_close($conn);
}

function display_course_information() {
    ?>
    <!DOCTYPE html>
    <html>
    <head>
        <style>
            div {
                margin: 20px;
            }
            
            a {
                text-decoration:none;
                color: blue;
            }
        </style>
        <meta name="viewport" charset="UTF-8" content="width=device-width, initial-scale=1.0">
        <title>COMP3322 Assignment 4</title>
    </head>
    <body>
        <h1> Course Information </h1>
        <h3> Retrieve continuous assessment scores for:</h3>
        <?php get_enrolled_courses();?>
    </body>
    </html>
    <?php
}
?>