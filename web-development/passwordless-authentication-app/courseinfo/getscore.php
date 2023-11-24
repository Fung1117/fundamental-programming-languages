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
    display_course_gradebook();
}

function get_assessment_records() {
    $id = $_SESSION['uid'];
    $course = $_GET['course'];
    $conn = mysqli_connect(DB_HOST, USERNAME, PASSWORD, DB_NAME) or die('Error! '. mysqli_connect_error($conn));
    $query = "SELECT * FROM courseinfo WHERE uid = $id AND course = '$course'";
    $result = mysqli_query($conn, $query) or die ('Failed to query '.mysqli_error($conn));
    if (mysqli_num_rows($result) > 0) {
        $total = 0;
        ?>
        <div> Assessment scores: </div>
        <table>
            <tr> 
                <th id='item'> Item </th>
                <th> Score </th> 
            </tr>
        <?php
        while($row = mysqli_fetch_array($result)) {
            $total += $row['score'];
            ?>
            <tr> 
                <td class='items'> <?php echo $row['assign'] ?> </td>
                <td class='score'> <?php echo $row['score'] ?> </td> 
            </tr>
            <?php
        } ?>
        <tr> 
            <td> </td>
            <td id="toatl"> <b>Total</b>&nbsp;<?php echo $total ?></td>
        </tr>
        </table>
        <?php
    } else {
        ?> <p> You do not have the gradebook for the course: <?php echo $course ?> in the system. </p>
        <?php
    }
    mysqli_free_result($result);
    mysqli_close($conn);
}

function display_course_gradebook() { 
    ?>
    <!DOCTYPE html>
    <html>
    <head>
        <style>
            div {
                text-align: center;
            }

            table, td, th {
                border: 1px solid;
            }

            table {
                margin-left: auto;
                margin-right: auto;
                width: 30%;
                border-collapse: collapse;
            }

            th {
                background-color: #f0f0f0;
            }

            .items, #toatl {
                padding: 2.5px 0 2.5px 5px;
            }

            .score {
                text-align: center;
            }

            #item {
                width: 60%;
            }

            @media screen and (max-width: 600px) {
                table {
                    width: 80%;
                }
            }
        </style>
        <meta name="viewport" charset="UTF-8" content="width=device-width, initial-scale=1.0">
        <title>COMP3322 Assignment 4</title>
    </head>
    <body>
        <h1> <?php echo $_GET['course']; ?> - Gradebook</h1>
        <?php get_assessment_records(); ?>
    </body>
    </html>
    <?php
}
?>