$(function () {
    toggleAdvancedFilter();
    clickSearch();
    RetornaCidades();
});

function toggleAdvancedFilter() {
    var advancedContent = $('#main-section .advanced-filter-content')
    var advancedButton = advancedContent.find('.advanced-filter-button');
    var advancedFilters = advancedContent.find('.advanced-filters');
    //advancedFilters.toggle();

    advancedButton.on("click", () => {
        advancedFilters.fadeToggle(500, "linear");
    });
}


function registerSubmit(register) {
    event.preventDefault();
    console.log(register);
    var form = $(register);

    let crimeCadastrado = {
        Descricao: form.find('#register-description').val(),
        Data: form.find('#register-crime-date').val().replace('-', '/').replace('-', '/'),
        nomeVitima: form.find('#register-name').val(),
        dataNascimentoVitima: form.find('#register-birthday').val().replace('-', '/').replace('-', '/'),
        emailVitima: form.find('#register-email').val(),
        sexoVitima: form.find('#register-genre').val(),
        
        endereco: {
            cep: form.find('#register-zipcode').val(),
            estado: form.find('#register-state').val(),
            cidadeBO: form.find('#register-city').val(),
            bairro: form.find('#register-district').val(),
            logradouro: form.find('#register-street').val(),
            numero: form.find('#register-streetnumber').val()
        },         

        tipoCrime: {
            Id: form.find('#register-crime-type').val(),
            Nome: form.find('#register-crime-type option:selected').text()
        }
    }

    console.log(crimeCadastrado);
    //Realiza o post
    if (crimeCadastrado != "" || null || undefined) {
        console.log("Chamou o método para dar o post.");
        RegistrarCrime(crimeCadastrado);
    }




}

function clickSearch() {
    var searchButton = $('#main-section .main-search-button');

    searchButton.click(() => {
        //var inputValue = $('#main-section .main-search-content .address-filter').val();      
        this.showLoader();
        hideResultBox();

        var campoLogradouro = $('#main-section .main-search-content .address-filter').val();
        var campoCidade = $('.advanced-filter-town').val();
        var tipoBaseDados = 0;

        //if ($('.advanced-filter-police').is(':checked') && $('.advanced-filter-user').is(':checked')) {
        //    tipoBaseDados = 3;

        if ($('.advanced-filter-user').is(':checked')) {
            tipoBaseDados = 2;                        

        } else if ($('.advanced-filter-police').is(':checked')) {
            tipoBaseDados = 1;
        }

        var endereco = {
            CidadeBO: campoCidade,
            Logradouro: campoLogradouro,
            Bairro: null,
            TipoBaseDados: tipoBaseDados
        };

        if (endereco != "" || null || undefined) {
            this.showLoader();
            RetornaListaDeCidades(endereco);
            campoLogradouro = $('#main-section .main-search-content .address-filter').val('');
            $('.advanced-filter-user, .advanced-filter-police').prop('checked', false)
        }
    });
}

function RetornaListaDeCidades(data) {
    $.get('/Home/ListEnderecoConfirmacao', data)
        .done((response) => {
            montaModalListaDeRuas(response, data.TipoBaseDados);
        })
        .fail(() => alert('Nenhuma rua foi encontrada.'));
}

function limpaListaDeRuas() {
    $('#listaRuas').html('');
    $('#listaRuas').html('<ul id="listaRuas"><li class="header"><p>Rua</p><p>Bairro</p><p>Cidade</p></li></ul>');
}

function montaModalListaDeRuas(listaRuas, tipoBaseDados) {
    if (listaRuas.length == 0) {
        this.hideLoader();
        return alert('Não constam registros criminais para o endereço digitado');
    }

    var listaRuasHTML = $('#listaRuas');
    this.limpaListaDeRuas();
    for (var i = 0; i < listaRuas.length; i++) {
        listaRuasHTML.append('<li onclick="selecionaEndereçoListaDeRuas(this)" data-baseID="' +tipoBaseDados+'" data-street="' + listaRuas[i].Logradouro + '" data-district="' + listaRuas[i].Bairro + '" data-city="' + listaRuas[i].Cidade +'"><p>' + listaRuas[i].Logradouro + '</p><p>' + listaRuas[i].Bairro + '</p><p>' + listaRuas[i].Cidade + '</p></li>');
    }
    $('#streetListModal').modal('show');
    this.hideLoader();
}

function selecionaEndereçoListaDeRuas(event) {
    this.showLoader();
    var endereco = {
        CidadeBO: event.dataset.city,
        Logradouro: event.dataset.street,
        Bairro: event.dataset.district,
        TipoBaseDados: event.dataset.baseid
    };

    //var enderecoGraficos = {
    //    CidadeBO: null,
    //    Logradouro: event.dataset.street,
    //    Bairro: null
    //};
    this.RetornaMensagemAlerta(endereco);

    endereco.Bairro = null;
    endereco.CidadeBO = null;

    this.RetornaTipoCrimePorMesAnual(endereco);
    this.RetornaTipoCrimePorMes(endereco);
    this.RetornaQtdCrimePorDia(endereco);
    $('#streetListModal').modal('hide');
    this.showResultBox();
}

function RetornaMensagemAlerta(data) {
    $.get('/Home/GeraMsgSeguranca', data)
        .done((response) => $('#warning-message p.message-description').text(response))
        .fail(() => alert('Não foi possível gerar o gráfico de "RetornaTipoCrimePorMes_Anual"'));
}

function RetornaCidades() {
    $.get('https://servicodados.ibge.gov.br/api/v1/localidades/estados/35/municipios')
        .done(function (data) {
            var comboCidade = $("#combo-cidade");
            //console.log(comboCidade);

            $.each(data, function (indice, obj) {
                comboCidade.append('<option>' + obj.nome + '</option>');
            });
        })
        .fail(function () {
            console.log('fail');
        });
}

function RegistrarCrime(data) {
    crimeCadastrado = data;

    $.post('/CrimeCadastrado/CadastrarCrime', crimeCadastrado)
        .done(() => {
            console.log('Usuário criado com sucesso.');
            this.limpaFormularioCadastro();
            $('#registerModal').modal('hide');
            alert('Crime cadastrado com sucesso');
        })
        .fail((erro) => alert("Não foi possível realizar o cadastro."));
}

function RetornaTipoCrimePorMesAnual(data) {
    endereco = data;
    $.get('/Home/RetornaTipoCrimePorMes_Anual', endereco)
        .done((data) => {
            criaGraficoTipoCrimePorMesAnual(data);
        })
        .fail(() => alert('Não foi possível gerar o gráfico de "RetornaTipoCrimePorMes_Anual"'));
}

function RetornaTipoCrimePorMes(data) {
    endereco = data;
    $.get('/Home/RetornaTipoCrimePorMes', endereco)
        .done((data) => {
            criaGraficoTipoCrimePorMes(data);
        })
        .fail(() => alert('Não foi possível gerar o gráfico de "RetornaTipoCrimePorMes"'));
}

function RetornaQtdCrimePorDia(data) {
    endereco = data;
    $.get('/Home/RetornaQtdCrimePorDia', endereco)
        .done((data) => {
            criaGraficoQtdCrimePorDia(data);
        })
        .fail(() => alert('Não foi possível gerar o gráfico de "RetornaQtdCrimePorDia"'));
}

function criaGraficoTipoCrimePorMesAnual(data) {
    var ctx = document.getElementById("RetornaTipoCrimePorMesAnual").getContext('2d');

    var listaQtdCrime = [];
    var listaNomeMes = [];

    if (this.graficoTipoCrimePorMesAnual)
        this.graficoTipoCrimePorMesAnual.destroy();

    for (var i = 0; i < data.length; i++) {
        listaQtdCrime.push(data[i].QtdCrimeMes);
        listaNomeMes.push(data[i].Mes);
    }

    this.graficoTipoCrimePorMesAnual = new Chart(ctx, {
        type: 'line',
        data: {
            labels: listaNomeMes,
            datasets:
                [{
                    label: "Crimes ocorridos no mês",
                    data: listaQtdCrime,
                    backgroundColor: 'transparent',
                    borderColor: '#3B3B3B',
                    borderWidth: 5
                }]
        },
        options: {
            label: { display: false },
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            },
            title: {
                display: true,
                text: 'Índice de quantidade de crimes ocorridos durante um ano'
            },
            layout: {
                padding: {
                    left: 10,
                    right: 10,
                    top: 20,
                    bottom: 10
                }
            }
        }
    });

    this.hideLoader();
}

function criaGraficoTipoCrimePorMes(data) {
    var ctx = document.getElementById("RetornaTipoCrimePorMes").getContext('2d');

    var listaQtdCrime = [];
    var listaNome = [];


    if (this.graficoTipoCrimePorMes)
        this.graficoTipoCrimePorMes.destroy();


    for (var i = 0; i < data.length; i++) {
        listaQtdCrime.push(data[i].QtdCrime);
        listaNome.push(data[i].Nome);
    }

    this.graficoTipoCrimePorMes = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: listaNome,
            datasets: [{
                label: listaNome,
                data: listaQtdCrime,
                backgroundColor: [
                    "rgba(255, 0, 0, 0.8)",
                    "rgba(80, 208, 255, 0.8)",
                    "rgba(0, 192, 0, 0.8)",
                    "rgba(255, 224, 32, 0.8)",
                    "rgba(8, 208, 55, 0.8)",
                    "rgba(160, 32, 255, 0.8)",
                    "rgba(255, 160, 16, 0.8)"
                ],
                borderColor: 'rgba(0, 0, 0, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            },
            title: {
                display: true,
                text: 'Índice de quantidade de crimes por mês separados por tipo'
            },
            layout: {
                padding: {
                    left: 10,
                    right: 10,
                    top: 20,
                    bottom: 10
                }
            }
        }
    });

    this.hideLoader();
}

function criaGraficoQtdCrimePorDia(data) {
    var ctx = document.getElementById("RetornaQtdCrimePorDia").getContext('2d');

    var listaQtdCrime = [];
    var listaData = [];

    if (this.graficoQtdCrimePorDia)
        this.graficoQtdCrimePorDia.destroy();


    for (var i = 0; i < data.length; i++) {
        listaQtdCrime.push(data[i].QtdCrime);
        listaData.push(data[i].Data);
    }

    this.graficoQtdCrimePorDia = new Chart(ctx, {
        type: 'line',
        data: {
            labels: listaData,
            datasets: [{
                label: "Crimes ocorridos por dia",
                data: listaQtdCrime,
                backgroundColor: "rgba(255, 0, 0, 0.8)",
                borderColor: 'rgba(0, 0, 0, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            },
            title: {
                display: true,
                text: 'Índice da quantidade de crimes ocorridos por dia em ' + getNomeMes(data[0].Data.split("/")[1])
            },
            layout: {
                padding: {
                    left: 10,
                    right: 10,
                    top: 20,
                    bottom: 10
                }
            }
        }
    });

    this.hideLoader();
}

function showResultBox() {
    return $('.result-content').show();
}

function hideResultBox() {
    return $('.result-content').hide();
}

function showLoader() {
    return $('body').addClass('loader');
}

function hideLoader() {
    return $('body').removeClass('loader');
}

function limpaFormularioCadastro(){
    $("#register-form input, #register-form select").val("");
}

function limpaFormularioCep() {
    $("#register-state").val("");
    $("#register-street").val("");
    $("#register-district").val("");
    $("#register-city").val("");
}

function getNomeMes(numMes) {
    switch (numMes) {
        case "01":
            nomeMes = "Janeiro";
            break;
        case "02":
            nomeMes = "Fevereiro";
            break;
        case "03":
            nomeMes = "Março";
            break;
        case "04":
            nomeMes = "Abril";
            break;
        case "05":
            nomeMes = "Maio";
            break;
        case "06":
            nomeMes = "Junho";
            break;
        case "07":
            nomeMes = "Julho";
            break;
        case "08":
            nomeMes = "Agosto";
            break;
        case "09":
            nomeMes = "Setembro";
            break;
        case "10":
            nomeMes = "Outubro";
            break;
        case "11":
            nomeMes = "Novembro";
            break;
        case "12":
            nomeMes = "Dezembro";
            break;

    }

    return nomeMes;
}



$('.advanced-filter-police').click(() => {
    $('.advanced-filter-user').prop('checked', false);
});

$('.advanced-filter-user').click(() => {
    $('.advanced-filter-police').prop('checked', false);
});

$("#register-zipcode").blur(function () {
    this.showLoader();
    var cep = $(this).val().replace(/\D/g, '');
    if (cep != "") {
        var validacep = /^[0-9]{8}$/;

        if (validacep.test(cep)) {
            $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                if (!("erro" in dados)) {
                    $("#register-state").val(dados.uf);
                    $("#register-city").val(dados.localidade);
                    $("#register-street").val(dados.logradouro);
                    $("#register-district").val(dados.bairro);
                }
                else {
                    limpaFormularioCep();
                    alert("CEP não encontrado.");
                }
            });
        } else {
            limpaFormularioCep();
            alert("Formato de CEP inválido.");
        }
    }
    else {
        limpaFormularioCep();
    }
    this.hideLoader();
});