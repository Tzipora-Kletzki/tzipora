function Search(wordToSearch) {//שליחת מילה לשרות המחפש על פי אגורטמים ויינשטיין
    
    var wordToSearch = $("#word").val();
    $.ajax({
        type: "Get",
        url: "Api/SearchApi",
        data: { word: wordToSearch },
        dataType: "json",
        xhrFields: { withCredentials: true },
        crossDomain: true,
        async: false,
        success: function (aa) {
            nodes = aa.nodes;
            edges = aa.edgesL
            $('#nodes').html("");
            $.each(nodes, function (i, item) {
                $('#nodes').append($('<option>', {
                    value: item.id,
                    text: item.label+"  "+item.m
                }));
            });

        },
        error: function (xhr, status, error) {
            debugger;
            alert(error);
        }
    });
  
     
    
}





var nodes;
var edges;
var dic;

$(document).ready(function () {//שליפת נתונים בטעינת הדף
    $.ajax({
        type: "Get",
        url: "api/SearchApi/getAll",
        dataType: "json",
        xhrFields: { withCredentials: true },
        crossDomain: true,
        async: true,
        success: function (aa) {
            dic = aa;
            $.each(dic, function (index, value,a) {
                $("#tableToProp").append("<tr><td>" + index + "</td><td>" + value + "</td></tr>");
              
            });
            $("#preloader").remove();
        },
        error: function (xhr, status, error) {
            alert(error);
        }
    });
    });

