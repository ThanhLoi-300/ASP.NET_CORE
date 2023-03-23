var list_Img = []

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
                                   <input type="text" value="${url}" name="file" />
                               </td>
                               <td class="text-center"> <input type="radio" checked ="checked" value="${currentId + 1}" name="radio"/></td>
                               <td class="text-center"> <div onclick="delete_btn(${currentId})" class="btn btn-sm btn-danger btn_Xoa"><i class="fas fa-trash"></i></div></td>
                            </tr>`
                else 
                    str += `<tr class="row_Id">
                               <td class="text-center" ><div class="stt">${currentId + 1}</div></td>
                               <td class="text-center">
                                   <img width="80" src="${url}"/>
                                   <input type="text" value="${url}" name="file" />
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

            function upload(field) {
                var finder = new CKFinder()
                finder.selectActionFunction = function (fileUrl) {
                    add_Img(fileUrl) 
                }
                finder.popup()
            }

function previewImage() {
    var fileInput = document.getElementById('myFileInput');
    var image = document.getElementById('myImage');
    var file = fileInput.files[0];
    var reader = new FileReader();
    reader.onload = function (e) {
        image.src = e.target.result;
    };
    reader.readAsDataURL(file);
}
