<?php

// Fung make this website

//Define the database connection constants
define("DB_HOST", "mydb");
define("USERNAME", "dummy");
define("PASSWORD", "c3322b");
define("DB_NAME", "db3322");


//These must be at the top of your script, not inside a function
use PHPMailer\PHPMailer\PHPMailer;
use PHPMailer\PHPMailer\SMTP;
use PHPMailer\PHPMailer\Exception;

//Load PHPMailer
require 'PHPMailer/src/Exception.php';
require 'PHPMailer/src/PHPMailer.php';
require 'PHPMailer/src/SMTP.php';

session_start();

start();

function start() {
    $current_time = time();
    if (isset($_SESSION['uid'])) {
        if ($current_time - $_SESSION['login_time'] <= 300) {
            header('Location: courseinfo/index.php');
            exit;
        } else {
            session_unset();
            session_destroy();
            display_login_form("Session expired. Please login again", "error");
        }
    } else if (isset($_GET['token'])) {
        // decode
        $token_json = hex2bin($_GET['token']);
        $token_array = json_decode($token_json, true);
        $uid = $token_array['uid'];
        $secret = $token_array['secret'];

        if (check_uid_record($uid)) {
            if (check_secret_record($secret)) {
                $timestamp = get_timestamp_by_uid($uid);
                if ($current_time - $timestamp <= 60) {
                    $_SESSION['uid'] = $uid;
                    $_SESSION['login_time'] = $current_time;
                    delete_timestamp_secret_by_uid($uid);
                    header('Location: courseinfo/index.php');
                    exit;
                } else {
                    delete_timestamp_secret_by_uid($uid);
                    display_login_form("Fail to authenticate - OTP expired!", "error");
                }
            } else {
                display_login_form("Fail to authenticate - incorrect secret!", "error");
            }
        } else {
            display_login_form("Unknown user - cannot identify the student.", "error");
        } 
    } else if (isset($_POST['login'])) {
        $email = $_POST['email'];
        if (check_email_record($email)) {
            $uid = get_uid_by_email($email);
            send_authentication_email($email, $uid);
            display_login_form("Please check your email for the authentication URL.", "authentication");
        } else {
            display_login_form("Unknown user - we don't have the records for ".$_POST['email']." in the system.", "error");
        }
    } else {
        display_login_form();
    }
}

function check_email_record($email) {
    $conn=mysqli_connect(DB_HOST, USERNAME, PASSWORD, DB_NAME) or die('Error! '. mysqli_connect_error($conn));
    $query = "SELECT * FROM user WHERE email = '$email'";
    $result = mysqli_query($conn, $query) or die ('Failed to query '.mysqli_error($conn));
    if (mysqli_num_rows($result) == 1) {
        $row = mysqli_fetch_assoc($result);
        mysqli_free_result($result);
        mysqli_close($conn);
        return true;
    }
    mysqli_free_result($result);
    mysqli_close($conn);
    return false;
}

function check_uid_record($uid) {
    $conn=mysqli_connect(DB_HOST, USERNAME, PASSWORD, DB_NAME) or die('Error! '. mysqli_connect_error($conn));
    $query = "SELECT * FROM user WHERE uid = $uid";
    $result = mysqli_query($conn, $query) or die ('Failed to query '.mysqli_error($conn));
    if (mysqli_num_rows($result) > 0) {
        $row = mysqli_fetch_assoc($result);
        mysqli_free_result($result);
        mysqli_close($conn);
        return true;
    }
    mysqli_free_result($result);
    mysqli_close($conn);
    return false;
}

function check_secret_record($secret) {
    $conn=mysqli_connect(DB_HOST, USERNAME, PASSWORD, DB_NAME) or die('Error! '. mysqli_connect_error($conn));
    $query = "SELECT * FROM user WHERE secret = '$secret'";
    $result = mysqli_query($conn, $query) or die ('Failed to query '.mysqli_error($conn));
    if (mysqli_num_rows($result) > 0) {
        $row = mysqli_fetch_assoc($result);
        mysqli_free_result($result);
        mysqli_close($conn);
        return true;
    }
    mysqli_free_result($result);
    mysqli_close($conn);
    return false;
}

function send_authentication_email($email, $uid) {
    $timestamp = time();
    $secret = bin2hex(random_bytes(8));
    upload_timestamp_secret_by_uid($uid, $secret, $timestamp);
    $token_array = [
        'uid' => $uid,
        'secret' => $secret
    ];
    // encode
    $token_json = json_encode($token_array);
    $token = bin2hex($token_json);

    // current url
    $current_url = (isset($_SERVER['HTTPS']) && $_SERVER['HTTPS'] === 'on' ? "https" : "http"). "://$_SERVER[HTTP_HOST]$_SERVER[REQUEST_URI]";
    $url = "$current_url?token=$token";

    $mail = new PHPMailer(true);
    try {
        $mail->isSMTP();                                            //Send using SMTP
        $mail->Host       = 'testmail.cs.hku.hk';                     //Set the SMTP server to send through
        $mail->SMTPAuth   = false;                                   //Enable SMTP authentication
    
        $mail->Port       = 25;                                    //TCP port to connect to; use 587 if you have set `SMTPSecure = PHPMailer::ENCRYPTION_STARTTLS`
    
        //Sender
        $mail->setFrom('c3322@cs.hku.hk', 'COMP3322');
        //******** Add a recipient to receive your email *************
        $mail->addAddress($email);   
    
        //Content
        $mail->isHTML(true);                                  //Set email format to HTML
        $mail->Subject = 'Access Course info page';
        $mail->Body    = "Dear Student, <br><br> You can log on to the system via the following link:<br><a href='$url'>$url<a>";
        //$mail->AltBody = 'This is the body in plain text for non-HTML mail clients';
        $mail->send();
    } catch (Exception $e) {
        echo "Message could not be sent. Mailer Error: {$mail->ErrorInfo}";
    }
}

function upload_timestamp_secret_by_uid($uid, $secret, $timestamp) {
    $conn = mysqli_connect(DB_HOST, USERNAME, PASSWORD, DB_NAME) or die('Error! '. mysqli_connect_error($conn));
    $query = "UPDATE user SET secret='$secret', timestamp=$timestamp WHERE uid = $uid";
    mysqli_query($conn, $query);
    mysqli_close($conn);
}


function delete_timestamp_secret_by_uid($uid) {
    $conn = mysqli_connect(DB_HOST, USERNAME, PASSWORD, DB_NAME) or die('Error! '. mysqli_connect_error($conn));
    $query = "UPDATE user SET secret=NULL, timestamp=NULL WHERE uid = $uid";
    mysqli_query($conn, $query);
    mysqli_close($conn);
}

function get_timestamp_by_uid($uid) {
    $conn = mysqli_connect(DB_HOST, USERNAME, PASSWORD, DB_NAME) or die('Error! '. mysqli_connect_error($conn));
    $query = "SELECT * FROM user WHERE uid = $uid";
    $result = mysqli_query($conn, $query) or die ('Failed to query '.mysqli_error($conn));
    if ($result && mysqli_num_rows($result) > 0) {
        $row = mysqli_fetch_assoc($result);
        $timestamp = $row['timestamp'];
        mysqli_free_result($result);
        mysqli_close($conn);
        return $timestamp;
    }
    mysqli_free_result($result);
    mysqli_close($conn);
    return "";
}

function get_uid_by_email($email) {
    $conn = mysqli_connect(DB_HOST, USERNAME, PASSWORD, DB_NAME) or die('Error! '. mysqli_connect_error($conn));
    $query = "SELECT * FROM user WHERE email = '$email'";
    $result = mysqli_query($conn, $query) or die ('Failed to query '.mysqli_error($conn));
    if ($result && mysqli_num_rows($result) > 0) {
        $row = mysqli_fetch_assoc($result);
        $uid = $row['uid'];
        mysqli_free_result($result);
        mysqli_close($conn);
        return $uid;
    }
    mysqli_free_result($result);
    mysqli_close($conn);
    return "";
}

function display_login_form($message='', $id="hidden") {
    ?>
    <!DOCTYPE html>
    <html>
    <head>
        <style>
            h1 {
                margin-bottom: 50px;
            }

            fieldset {
                width: 40%;
                height: 80px;
                margin: auto;
                margin-bottom: 50px;
                border-radius: 10px;
                background-color: #e0ffff;
            }

            legend, label {
                margin: 0 25px 0 10px;
            }

            input[name="email"] {
                width: 60%;
            }

            input[name="login"] {
                display: block;
                margin: 15px auto 0;
            }

            #center {
                display: flex;
                justify-content: center;
            }

            #hidden {
                display: none;
            }

            #error {
                background-color: #ffcccc;
                display: inline-block; 
                vertical-align: middle;
                width: 40%;
                padding: 10px;
                border-radius: 10px;
            }

            #authentication {
                background-color: #ccccff;
                display: inline-block; 
                vertical-align: middle;
                width: 40%;
                padding: 10px;
                border-radius: 10px;
            }

            p {
                line-height: 1.5;
            }

            @media screen and (max-width: 600px) {
                fieldset, #msg, #error, #authentication {
                    width: 80%;
                }
                legend, label {
                    margin: 0 0 0 10px;
                }
            }
        </style>
        <meta name="viewport" charset="UTF-8" content="width=device-width, initial-scale=1.0">
        <title>COMP3322 Assignment 4</title>
    </head>
    <body>
    <h1> Gradebook Accessing Page </h1>
    <form action="login.php" method="post">
        <fieldset>
            <legend> <b>My Gradebooks</b> </legend>
            <label for="email"> <b>Email:</b> </label>
            <input type="text" name="email" id="email" value="" pattern="^[_\.A-Za-z0-9]+@(connect|cs)\.hku\.hk$" required/>
            <input type="submit" name="login" id="submit" value="Login">
        </fieldset>
        <div id="center">
            <div id="<?php echo $id?>">
                <p><?php echo $message; ?></p>
            </div>
        <div>
    </form>
    <?php
} ?>

<script defer>
        const emailInput = document.getElementById("email");
        emailInput.setCustomValidity("Must be an email address with @cs.hku.hk or @connect.hku.hk")
        emailInput.addEventListener("input", function (event) {
            console.debug(emailInput.validity.patternMismatch);
            if (emailInput.validity.patternMismatch) {
                emailInput.setCustomValidity("Must be an email address with @cs.hku.hk or @connect.hku.hk");
            } else if (emailInput.validity.valueMissing) {
                emailInput.setCustomValidity("Must be an email address with @cs.hku.hk or @connect.hku.hk");
            }else {
                emailInput.setCustomValidity("");
            }
        });
</script>
</body>
</html>