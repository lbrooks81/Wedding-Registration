document.addEventListener('DOMContentLoaded', () =>
{
    let tableWidth = document.getElementById('table').style.width;
    let buttons = document.getElementsByClassName("btns");

    for (let btn in buttons)
    {
        btn.style.width = tableWidth / 4;
    }
});

