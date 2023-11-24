const months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
let current_date = ""
let current_time = ""
let yesterday = ""
update_time()

var text = document.getElementById("text")
var arrival = document.getElementById("arrival")
var departure = document.getElementById("departure")
let isArrival = false
let prev_output = ""
let output = ""
let next_output = ""

document.querySelector('#checkbox').checked = false

document.querySelector('#checkbox').addEventListener('click', (event) =>{
    if(event.target.checked){
        text.innerHTML = "Arrival "
        arrival.style.textDecoration = "underline"
        departure.style.textDecoration = "none"
        isArrival = true
    } else {
        text.innerHTML = "Departure "
        arrival.style.textDecoration = "none"
        departure.style.textDecoration = "underline"
        isArrival = false
    }
    update_time()
    prev_output = ""
    output = ""
    next_output = ""
    document.querySelector('.loadEarlyButton').style.display = 'inline'
    document.querySelector('.loadMoreButton').style.display = 'inline'
    Request()
})

var iata_code_list = {}
fetch("iata.json")
.then(response => {
    response.json().then(iata => {
        iata.forEach( data => {
            let iata_code = Object.values(data)[0]
            let name = Object.values(data)[1]
            let municipality = Object.values(data)[5]
            iata_code_list[iata_code] = `<span style="padding-left: 5px;">${municipality} (${name})</span><br>`
        })
    })
})

Request()

function update_time() {
    let current = new Date()
    let month = (current.getMonth() + 1) < 10 ? "0" + (current.getMonth() + 1) : (current.getMonth() + 1)
    let day = current.getDate() < 10 ? "0" + current.getDate() : current.getDate()
    let hour = current.getHours() < 10 ? "0" + current.getHours() : current.getHours()
    let min = current.getMinutes() < 10 ? "0" + current.getMinutes() : current.getMinutes()

    current_date = `${current.getFullYear()}-${month}-${day}`
    current_time = `${hour}:${min}`
    document.getElementById("today").innerHTML = ` ${current.getDate()} ${months[current.getMonth()]} ${current.getFullYear()}`
    
    let ytd = new Date()
    ytd.setDate(ytd.getDate()-1)
    let ytd_month = (ytd.getMonth() + 1) < 10 ? "0" + (ytd.getMonth() + 1) :  (ytd.getMonth() + 1)
    let ytd_day = ytd.getDate() < 10 ? "0" + ytd.getDate() : ytd.getDate()
    yesterday = `${ytd.getFullYear()}-${ytd_month}-${ytd_day}`
}

function Request() {
    fetch(`flight.php?date=${current_date}&lang=en&cargo=false&arrival=${isArrival}`)
    .then(response => {
        response.json().then( flights => {
            let today_flight = flights[flights.length-1]
            let count = 0

            if (flights.length > 1){
                for (let x of flights[0].list){
                    let flight_no = ""
                    let destination = ""
                    let time = yesterday + " " + x.time
                    if (isArrival) {
                        for (let airplane of x.origin) {
                            destination += iata_code_list[airplane]
                        }
                    } else {
                        for (let airplane of x.destination) {
                            destination += iata_code_list[airplane]
                        }
                    }
                    for (let y of x.flight) {
                        flight_no += `<span class="flight_no"> ${y.no.slice(0,2)}&nbsp;${y.no.slice(3)} </span>`
                    }
                    prev_output += isArrival ? html_arrival(flight_no, destination, time, x) : html_departure(flight_no, destination, time, x)
                }
            }

            for (let x of today_flight.list){
                let flight_no = ""
                let destination = ""
                let time = x.time
                if (isArrival) {
                    for (let airplane of x.origin) {
                        destination += iata_code_list[airplane]
                    }
                } else {
                    for (let airplane of x.destination) {
                        destination += iata_code_list[airplane]
                    }
                }
                for (let y of x.flight) {
                    flight_no += `<span class="flight_no"> ${y.no.slice(0,2)}&nbsp;${y.no.slice(3)} </span>`
                }
                if (x.time < current_time) {
                    prev_output += isArrival ? html_arrival(flight_no, destination, time, x) : html_departure(flight_no, destination, time, x)
                } else if (x.time >= current_time && count < 10) {
                    count ++
                    output += isArrival ? html_arrival(flight_no, destination, time, x) : html_departure(flight_no, destination, time, x)
                } else {
                    next_output += isArrival ? html_arrival(flight_no, destination, time, x) : html_departure(flight_no, destination, time, x)
                }
            }
            document.getElementById('output').innerHTML = output;
            if (prev_output == ""){
                document.querySelector('.loadEarlyButton').style.display = 'none'
            }
            if (next_output == ""){
                document.querySelector('.loadMoreButton').style.display = 'none'
            }
        })
    })
}

function html_departure(flight_no, destination, time, x) {
    return `<table>
                <tr> <td> <b>Flight No.:</b> ${flight_no} </td>
                     <td> <b>Scheduled Time:</b> ${time} </td></tr>
                <tr> <td colspan="2"> <b>Destination (Airport):</b> <br> ${destination} </td> </tr>
                <tr> <td> <b>Terminal:</b> ${x.terminal} <span style="padding-left: 10px;"><b>Aisle:</b> ${x.aisle}</span> <span style="padding-left: 10px;"><b>Gate:</b> ${x.gate}</span> </td>
                     <td> <b>Status:</b> ${x.status} </td> </tr>
            </table>`
}

function html_arrival(flight_no, destination, time, x) {
    return  `<table>
                <tr> <td> <b>Flight No.:</b> ${flight_no} </td>
                     <td> <b>Scheduled Time:</b> ${time} </td></tr>
                <tr> <td colspan="2"> <b>Origin (Airport):</b> <br> ${destination} </td> </tr>
                <tr> <td> <b>Parking Stand:</b> ${x.stand} <span style="padding-left: 10px;"><b>Hall:</b> ${x.hall}</span> <span style="padding-left: 10px;"><b>Belt:</b> ${x.baggage}</span> </td>
                     <td> <b>Status:</b> ${x.status} </td> </tr>
            </table>`
}

function loadEarlyFlights() {
    document.getElementById('output').innerHTML =  prev_output + document.getElementById('output').innerHTML
    document.querySelector('.loadEarlyButton').style.display = 'none'
}

function loadNextFlights() {
    document.getElementById('output').innerHTML += next_output
    document.querySelector('.loadMoreButton').style.display = 'none'
}
