function setStyle(id, style, value) {
    id.style[style] = value;
}
function opacity(el, opacity) {
    setStyle(el, "filter:", "alpha(opacity=" + opacity + ")");
    setStyle(el, "-moz-opacity", opacity / 100);
    setStyle(el, "-khtml-opacity", opacity / 100);
    setStyle(el, "opacity", opacity / 100);
}
function calendar() {
    var date = new Date();
    var day = date.getDate();
    var month = date.getMonth(); // idzie od zera
    var year = date.getFullYear();
    months = new Array('Styczeń', 'Luty', 'Marzec', 'Kwiecien', 'Maj', 'Czerwiec', 'Lipiec', 'Sierpień', 'Wrzesień', 'październik', 'listopad', 'grudzień');
    days_in_month = new Array(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);
    if (year % 4 == 0 && year != 1900) {
        days_in_month[1] = 29;
    }
    total = days_in_month[month];
    var date_today = day + ' ' + months[month] + ' ' + year;
    beg_j = date;
    beg_j.setDate(1); //zapisuje dzien jakopierwszy dzien w miesiacu
    if (beg_j.getDate() == 2) { //sprawdza czy dzien jest 2 wtedy zmienia dzien na ostatni dzien tygodnia tamtego miesiąca
        beg_j = setDate(0);
    }
    beg_j = beg_j.getDay(); //zwraca dzien tygodnia niedziela to jest 0
    document.write('<table class="cal_calendar" onload="opacity(document.getElementById(\'cal_body\'),20);"><tbody id="cal_body"><tr><th colspan="7">' + date_today + '</th></tr>');
    document.write('<tr class="cal_d_weeks"><th>Pon</th><th>Wt</th><th>Sr</th><th>Czw</th><th>Pią</th><th>Sob</th><th>Niedz</th></tr><tr>');
    week = 0; //  31 - 6 + 1
    for (i = 0; i < beg_j-1; i++) {
        document.write('<td class="cal_days_bef_aft">' + (days_in_month[month - 1] - beg_j + i+2) + '</td>');
        week++;
    }
    for (i = 1; i <= total; i++) {
        if (week == 0) {
            document.write('<tr>');
        }
        if (day == i) {
            document.write('<td class="cal_today">' + i + '</td>');
        }
        else {
            document.write('<td>' + i + '</td>');
        }
        week++;
        if (week == 7) {
            document.write('</tr>');
            week = 0;
        }
    }
    for (i = 1; week != 0; i++) {
        document.write('<td class="cal_days_bef_aft">' + i + '</td>');
        week++;
        if (week == 7) {
            document.write('</tr>');
            week = 0;
        }
    }
    document.write('</tbody></table>');
    opacity(document.getElementById('cal_body'), 70);
    return true;
}