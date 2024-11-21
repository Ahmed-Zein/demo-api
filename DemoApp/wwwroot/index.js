const port = 8080;
const mapLvl = (lvl) => {
    switch (lvl) {
        case 0:
            return "info";
        default:
            return "error";
    }
};
const table = document.getElementById("tableBody");
const connection = new signalR.HubConnectionBuilder()
    .withUrl(`http://localhost:${port}/logs`)
    .withAutomaticReconnect()
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.on("OnLog", (data) => {
    console.log(data);
    let lvlCls = mapLvl(data.level);
    table.innerHTML += `
    <tr>
      <td class=${lvlCls}>${lvlCls}</td>
      <td>${data.message}</td>
      <td>${new Date(data.timStamp).toLocaleString()}</td>
    </tr>
  `;
});
connection
    .start()
    .then((_) => {
        console.log("CONNECTED");
    })
    .catch((e) => console.error(e));
