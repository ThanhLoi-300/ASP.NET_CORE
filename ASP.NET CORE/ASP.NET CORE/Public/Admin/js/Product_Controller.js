var list_Img = []//content
var list_File = []

            function delete_btn(index) {
                Swal.fire({
                    title: 'Do you want to delete?',
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonText: 'Yes',
                }).then((result) => {
                    if (result.isConfirmed) {
                        var list_Row = $('.row_Id')
                        list_Img.splice(index, 1)
                        list_File.splice(index, 1)

                        for (var i = 0; i < list_Row.length; i++)
                            list_Row[i].remove()

                        var str = ''
                        for (var i = 0; i < list_Img.length; i++) {
                            if(i == 0)
                                str = `<tr class="row_Id">
                                           <td class="text-center" ><div class="stt">${i + 1}</div></td>
                                           <td class="text-center">
                                               <img width="80" src="${list_Img[i]}"/><input type="hidden" value="${list_Img[i]}" name="file"/>
                                           </td>
                                           <td class="text-center"> <input type="radio" checked ="checked" value="${i + 1}" name="radio"/></td>
                                           <td class="text-center"> <div onclick="delete_btn(${i})" class="btn btn-sm btn-danger btn_Xoa"><i class="fas fa-trash"></i></div></td>
                                       </tr>`
                            else
                                str = `<tr class="row_Id">
                                           <td class="text-center" ><div class="stt">${i + 1}</div></td>
                                           <td class="text-center">
                                               <img width="80" src="${list_Img[i]}"/><input type="hidden" value="${list_Img[i]}" name="file"/>
                                           </td>
                                           <td class="text-center"> <input type="radio" value="${i + 1}" name="radio"/></td>
                                           <td class="text-center"> <div onclick="delete_btn(${i})" class="btn btn-sm btn-danger btn_Xoa"><i class="fas fa-trash"></i></div></td>
                                       </tr>`
                            $('#tbHtml').append(str)
                        }
                        var temp = $('#CurrentId').val()
                        $('#CurrentId').val(temp - 1)

                        let dataTransfer = new DataTransfer();
                        for (let file of list_File) {
                            dataTransfer.items.add(file);
                        }

                        save_Upload.files = dataTransfer.files
                    }
                })
            }

            function add_Img(url) {
                var temmp = $('#CurrentId').val()
                var currentId = parseInt(temmp)
                var str = ''
                if (currentId == 0)
                    str += `<tr class="row_Id">
                               <td class="text-center" ><div class="stt">${currentId + 1}</div></td>
                               <td class="text-center">
                                   <img width="80" src="${url}"/>
                                   <input type="hidden" value="${url}" name="file" />
                               </td>
                               <td class="text-center"> <input type="radio" checked ="checked" value="${currentId + 1}" name="radio"/></td>
                               <td class="text-center"> <div onclick="delete_btn(${currentId})" class="btn btn-sm btn-danger btn_Xoa"><i class="fas fa-trash"></i></div></td>
                            </tr>`
                else 
                    str += `<tr class="row_Id">
                               <td class="text-center" ><div class="stt">${currentId + 1}</div></td>
                               <td class="text-center">
                                   <img width="80" src="${url}"/>
                                   <input type="hidden" value="${url}" name="file" />
                               </td>
                               <td class="text-center"> <input type="radio" value="${currentId + 1}" name="radio"/></td>
                               <td class="text-center"> <div onclick="delete_btn(${currentId})" class="btn btn-sm btn-danger btn_Xoa"><i class="fas fa-trash"></i></div></td>
                            </tr>`
                list_Img.push(url)
                $('#tbHtml').append(str)
                $('#CurrentId').val(currentId + 1)
            }


            function display_Img(file) {
                if (file.length > 0) {
                    $('#img').attr('value', file[0].name)
                    var fileReader = new FileReader()
                    fileReader.onload = function (e) {
                        $('#avata').attr('src', e.target.result)
                    }
                    fileReader.readAsDataURL(file[0])
                }
            }

const upload = document.getElementById("upload");
const save_Upload = document.getElementById('save_Upload');

upload.addEventListener("change", () => {
    const files = upload.files;

    const filesArray = Array.from(files);

    filesArray.sort(function (a, b) {
        return a.name.localeCompare(b.name);
    });

    let dataTransfer = new DataTransfer();
    for (let file of filesArray) {
        dataTransfer.items.add(file);
    }

    for (let i = 0; i < dataTransfer.files.length; i++) {
        const file = dataTransfer.files[i];
        const reader = new FileReader();

        reader.readAsDataURL(file);

        reader.onload = () => {
            console.log(reader.result.length)
            add_Img(reader.result)
        };
    }
    //save_Upload.files = dataTransfer.files
    Data_From_Input1_To_Input2()
    console.log(list_File.length)
});

function Data_From_Input1_To_Input2() {
    alert("ok")
    // Lặp qua từng thẻ input và lấy danh sách file của nó
    if (upload.files.length > 0) {
        // Lặp qua từng file của thẻ input và thêm vào danh sách file
        for (let i = 0; i < upload.files.length; i++) {
            list_File.push(upload.files[i]);
        }
    }

    let dataTransfer = new DataTransfer();
    for (let file of list_File) {
        dataTransfer.items.add(file);
    }

    save_Upload.files = dataTransfer.files
    console.log(list_File.length)
}

