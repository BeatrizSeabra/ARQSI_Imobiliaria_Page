var root_path="widget_procura/";
var jqueryURL="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js";
var jsBootstrap="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js";
var estBootstrap="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css";

var constURLrestr="http://phpdev2.dei.isep.ipp.pt/~nsilva/imorest/imoveis.php?";
var urlFacetas="http://phpdev2.dei.isep.ipp.pt/~nsilva/imorest/facetas.php";
var urlValFaceta="http://phpdev2.dei.isep.ipp.pt/~nsilva/imorest/valoresFaceta.php?faceta=";
var urlMinFaceta="http://phpdev2.dei.isep.ipp.pt/~nsilva/imorest/minFaceta.php?facetaCont=";
var urlMaxFaceta="http://phpdev2.dei.isep.ipp.pt/~nsilva/imorest/maxFaceta.php?facetaCont=";
var urlTipoFaceta="http://phpdev2.dei.isep.ipp.pt/~nsilva/imorest/tipoFaceta.php?faceta=";
var iconeSearch="glyphicon glyphicon-search";
var iconeLeft="glyphicon glyphicon-chevron-left";
var iconeRight="glyphicon glyphicon-chevron-right";
var loadingImg=root_path+"imagens/loading.gif";
var notFoundImg=root_path+"imagens/notfound.gif";
var estiloW=root_path+"widget.css";
var loading=[];
var conteudo=[];
var nextStep=[];
var detalhes=[];

function limpa(elem){
	while(elem.firstChild){
		elem.removeChild(elem.firstChild);
	}
}
function randomId(partial){
	return "rid_"+partial+Math.floor((Math.random() * 100000) + 1);
}
function createElement(tag, classe, id, role, href){
	var element=document.createElement(tag);
	if(typeof classe !== "undefined" && classe!==null){
		element.setAttribute("class",classe);}
	if(typeof id !== "undefined" && id!==null){
		element.setAttribute("id",id);}
	if(typeof role !== "undefined" && role!==null){
		element.setAttribute("role",role);}
	if(typeof href !== "undefined" && href!==null){
		element.setAttribute("href",href);}
	return element;
}
function createDiv(classe, id, role){
	return createElement("div", classe, id, role, null);
}
function placeLoader(contentor){
	while(contentor.firstChild){
		contentor.removeChild(contentor.firstChild);
	}
	var loading_image=document.createElement("img");
	loading_image.setAttribute("src",loadingImg);
	var brk=document.createElement("br");
	var loading_text=document.createTextNode("A carregar...");
	var loading_span=document.createElement("span");
	loading_span.appendChild(loading_image);
	loading_span.appendChild(brk);
	loading_span.appendChild(loading_text);
	contentor.appendChild(loading_span);
}
//método para simplificar chamadas de pedidos ao servidor (API REST)
function makeXmlHttpGetCall(url, params, async, xml, callback, args, contentorID) {
	var xmlHttpObj = new XMLHttpRequest();
	if (xmlHttpObj) {
		if(typeof loading[contentorID] === "undefined"){
			loading[contentorID]=0;	
		}
		loading[contentorID]++;
		xmlHttpObj.open("Get",url, async);
		xmlHttpObj.onreadystatechange = function() {
			 if (xmlHttpObj.readyState === 4 && xmlHttpObj.status === 200) {
				 var response;
				 if (xml === true){
					response = xmlHttpObj.responseXML;
				 }
				 else{
					 response = xmlHttpObj.responseText;
				 }
				 callback(response,args);
				 loading[contentorID]--;
				 if(loading[contentorID]==0){
					 
					 var contentor=document.getElementById(contentorID);
					 limpa(contentor);
					 contentor.appendChild(conteudo[contentorID]);
					 //nextStep[contentorID][0](nextStep[contentorID][1]);
				 }
			 }
		 };
		 xmlHttpObj.send(params);
	}
}
//método para adicionar as checkbox's ao painel de dada faceta
function preencheCheckVal(response, contentor){
	var valoresFaceta = JSON.parse(response);
	for(var j = 0; j < valoresFaceta.length; j++)
	{
		var facetaId = contentor[1];
		var brk=document.createElement("br");
		var lbl=document.createElement("label");
		var checkbox=createElement("input", null, facetaId, null, null);
		checkbox.setAttribute("type","checkbox");
		checkbox.setAttribute("name", facetaId);
		checkbox.setAttribute("value",valoresFaceta[j]);
		var check_text=document.createTextNode(" "+valoresFaceta[j]);
		
		lbl.appendChild(checkbox);
		lbl.appendChild(check_text);
		
		contentor[0].appendChild(lbl);
		contentor[0].appendChild(brk);
	}
}
//método para definir como limite mínimo da text box (dados contínuos) o valor mínimo da faceta
function preencheMinimo(response,minimos){
	var minimo = JSON.parse(response);
	minimos[0].setAttribute("min",minimo.min);
	minimos[1].setAttribute("min",minimo.min);
}
//método para definir como limite máximo da text box (dados contínuos) o valor máximo da faceta
function preencheMaximo(response,maximos){
	var maximo = JSON.parse(response);
	maximos[0].setAttribute("max",maximo.max);
	maximos[1].setAttribute("max",maximo.max);
}
//método para ir buscar os valores da faceta, para depois meter as checkbox's
function preencheValoresDiscreto(content){
	var fac=content[1];
	
	makeXmlHttpGetCall(urlValFaceta + fac, null, true, false, preencheCheckVal, content, content[2]);
}
function validate(minimoID,maximoID){
	var minimo=document.getElementById(minimoID);
	var maximo=document.getElementById(maximoID);
	if(maximo.value<minimo.value){
		var valueTmp=maximo.value;
		maximo.value=minimo.value;
		minimo.value=valueTmp;
	}
}
//método para criar a caixa de texto, para as facetas cujo tipo de dados é contínuo
function preencheValoresContinuo(content){
	var fac=content[1];
	
	var textMinimo=createElement("input", null, randomId(fac+"Minimo"), null, null);
	textMinimo.setAttribute("type","number");
	textMinimo.setAttribute("name","quantity");
	var lblMinimo=createElement("label", null, null, null, null);
	lblMinimo.setAttribute("for", "'"+textMinimo.id+"'");
	lblMinimo.appendChild(document.createTextNode("Minimo: "));
	
	var textMaximo=createElement("input", null, randomId(fac+"Maximo"), null, null);
	textMaximo.setAttribute("type","number");
	textMaximo.setAttribute("name","quantity");
	var lblMaximo=createElement("label", null, null, null, null);
	lblMaximo.setAttribute("for", "'"+textMaximo.id+"'");
	lblMaximo.appendChild(document.createTextNode("Maximo: "));
	
	textMinimo.setAttribute("onfocusout","validate('"+textMinimo.id+"','"+textMaximo.id+"');");
	textMaximo.setAttribute("onfocusout","validate('"+textMinimo.id+"','"+textMaximo.id+"');");
	
	content[0].appendChild(lblMinimo);
	content[0].appendChild(textMinimo);
	content[0].appendChild(lblMaximo);
	content[0].appendChild(textMaximo);
	
	var caixasTxt=[textMinimo,textMaximo];
	var minURL=urlMinFaceta+fac;
	var maxURL=urlMaxFaceta+fac;
	makeXmlHttpGetCall(minURL, null, true, false, preencheMinimo, caixasTxt, content[2]);
	makeXmlHttpGetCall(maxURL, null, true, false, preencheMaximo, caixasTxt, content[2]);
}
//método para, consoante o tipo de dados, chamar o método para preencher os valores das facetas
function preencheValores(response, content){
	var tipoDados = JSON.parse(response);
	if(tipoDados.semântica!=="figura"){
		if(tipoDados.discreto === "discreto"){
			preencheValoresDiscreto(content);	
		}
		else if(tipoDados.discreto === "contínuo"){
			preencheValoresContinuo(content);
		}
	}
}
//método para ir buscar o tipo de dados de cada faceta
function preencheFaceta(faceta,contentor,parentID){
	var content = [contentor,faceta,parentID];
	var tipoURL=urlTipoFaceta + faceta;
	makeXmlHttpGetCall(tipoURL, null, true, false, preencheValores, content, parentID);
}
//método para fazer os painéis expandíveis com os nomes das facetas
function preencheFacetas(response, parentID){
	var contentor=conteudo[parentID];	
	var facetasXML = response.getElementsByTagName("faceta");
	for (var i=0;i<facetasXML.length;i++)
	{
		var faceta_c = facetasXML[i].childNodes[0];
		var faceta = faceta_c.nodeValue;
		var c_faceta_id = randomId("c_faceta");
		var p_faceta_id = randomId("p_faceta");
		var t_faceta_id = randomId("t_faceta");
		
		var faceta_nice=faceta.replace(/_/g," ");
		var lnk=createElement("a", null, null, "button", "#"+c_faceta_id);
		lnk.setAttribute("data-toggle","collapse");
		lnk.setAttribute("data-parent","#"+contentor.id);
		lnk.appendChild(document.createTextNode(faceta_nice));
		
		var header=createElement("h6", "panel-title", null, null, null);
		header.setAttribute("style","text-transform:capitalize;");
		header.appendChild(lnk);
		
		var painel_heading=createDiv("panel-heading", t_faceta_id, "tab");
		painel_heading.appendChild(header);
		
		var painel_corpo=createDiv("panel-body",p_faceta_id, null);
		
		var painel_conteudo=createDiv("panel-collapse collapse", c_faceta_id, "tabpanel");
		painel_conteudo.appendChild(painel_corpo);
		
		var painel_default=createDiv("panel panel-default", null, null);
		painel_default.appendChild(painel_heading);
		painel_default.appendChild(painel_conteudo);
		
		contentor.appendChild(painel_default);
		
		//chama método de preenchimento dos valores da faceta e envia painel.corpo como contentor
		//nesse método faz verificação do tipo de dados e invoca um método diferente, ou não (imagens)
		preencheFaceta(faceta,painel_corpo,parentID);
	}
}
function fechaDetalhe(){
	var shaderDiv=document.getElementById("shader");
	document.getElementsByTagName("body").item(0).removeChild(shaderDiv);
}
function mostraDetalhe(contentorID, i){
	
	var mostrarDetalhe=createDiv(null,"shader",null);
	var painelDetalhe=createDiv("jumbotron",null, null);
	
	var detalheR=detalhes[contentorID][i];
	
	var img=createElement("img", "img-detalhe pull-left", null, null, null);
	if(typeof detalheR.fotos[0]!=="undefined"){
		img.setAttribute("src", detalheR.fotos[0]);
	}
	else{
		img.setAttribute("src",notFoundImg);
	}
	img.setAttribute("alt", "Imagem Indisponível");
			
	var tipoImovelDetalhe=createElement("p",null, null,null,null);
	tipoImovelDetalhe.appendChild(document.createTextNode("Tipo De Imóvel: "+detalheR.tipo_de_imóvel));
	var tipoAnuncioDetalhe=createElement("p",null, null,null,null);
	tipoAnuncioDetalhe.appendChild(document.createTextNode("Tipo De Anúncio: "+detalheR.tipo_de_anúncio));
	var localizacaoDetalhe=createElement("p",null, null,null,null);
	localizacaoDetalhe.appendChild(document.createTextNode("Localização: "+detalheR.localização));
	var areaDetalhe=createElement("p",null, null,null,null);
	areaDetalhe.appendChild(document.createTextNode("Área: "+detalheR.área));
	var precoDetalhe=createElement("p",null, null,null,null);
	precoDetalhe.appendChild(document.createTextNode("Preço: "+detalheR.preço));
	var mediadorDetalhe=createElement("p",null, null,null,null);
	mediadorDetalhe.appendChild(document.createTextNode("Mediador: "+detalheR.mediador));
	
	var closeBtn=createElement("a", "btn btn-default pull-right", null, "button", null);
	closeBtn.setAttribute("onclick", "fechaDetalhe();");
	closeBtn.appendChild(document.createTextNode("Fechar Anúncio"));
	
	painelDetalhe.appendChild(img);
	painelDetalhe.appendChild(tipoImovelDetalhe);
	painelDetalhe.appendChild(tipoAnuncioDetalhe);
	painelDetalhe.appendChild(localizacaoDetalhe);
	painelDetalhe.appendChild(areaDetalhe);
	painelDetalhe.appendChild(precoDetalhe);
	painelDetalhe.appendChild(mediadorDetalhe);
	painelDetalhe.appendChild(closeBtn);
	
	mostrarDetalhe.appendChild(painelDetalhe);
	document.getElementsByTagName("body").item(0).appendChild(mostrarDetalhe);
}

function preencheResultados(response, contentor){
	var resultados = JSON.parse(response);
	var contentorID=contentor.id;
	if(resultados!==null){
		var painelResults=createDiv(null,contentor.id+"-painel", null);
		detalhes[contentorID]=resultados;
		var carousel=createDiv("carousel slide",contentor.id+"-carousel", null);
		carousel.setAttribute("data-ride","carousel");
		var indics=createElement("ol","carousel-indicators",null,null,null);
		var innerC=createDiv("carousel-inner",null,"listbox");
		
		var controlL=createElement("a", "left carousel-control", null, "button", "#"+contentorID+"-carousel");
		controlL.setAttribute("data-slide","prev");
	
		var span1=createElement("span", iconeLeft, null, null, null);
		span1.setAttribute("aria-hidden", "true");
		var span2=createElement("span", "sr-only", null, null, null);
		span2.appendChild(document.createTextNode("Previous"));
		controlL.appendChild(span1);
		controlL.appendChild(span2);
	
		var controlR=createElement("a", "right carousel-control", null, "button", "#"+contentorID+"-carousel");
		controlR.setAttribute("data-slide","next");
	
		var span3=createElement("span", iconeRight, null, null, null);
		span3.setAttribute("aria-hidden", "true");
		var span4=createElement("span", "sr-only", null, null, null);
		span4.appendChild(document.createTextNode("Next"));
	
		controlR.appendChild(span3);
		controlR.appendChild(span4);
		
		carousel.appendChild(indics);
		carousel.appendChild(innerC);
		carousel.appendChild(controlL);
		carousel.appendChild(controlR);
		
		painelResults.appendChild(carousel);
		
		conteudo[contentorID]=painelResults;
	
		for(var i=0; i<resultados.length; i++){
			
			var target=createElement("li", "active", null,null,null);
			target.setAttribute("data-target","#"+contentor.id+"-carousel");
			target.setAttribute("data-slide-to",i);
			indics.appendChild(target);
			var itemC;
			if(i===0){
				itemC=createDiv("item active", null,null);
			}
			else{
				itemC=createDiv("item", null,null);
			}
			
			var img=createElement("img", null, null, null, null);
			
			if(typeof resultados[i].fotos[0]!=="undefined"){
				img.setAttribute("src", resultados[i].fotos[0]);
			}
			else{
				img.setAttribute("src",notFoundImg);
			}
			img.setAttribute("alt", "Imagem Indisponível");
			img.setAttribute("onclick", "mostraDetalhe('"+contentorID+"',"+i+");");
			
			var caption=createDiv("carousel-caption", null, null);
			var tituloImg=createElement("p", null, null, null, null);
			tituloImg.appendChild(document.createTextNode(resultados[i].tipo_de_imóvel+" - "+resultados[i].tipo_de_anúncio));
			caption.appendChild(tituloImg);
			itemC.appendChild(img);
			itemC.appendChild(caption);
			innerC.appendChild(itemC);
		}
	}
}

function procurar(widgetId){
	var widget=document.getElementById(widgetId);
	if(widget!==null){
		var inputs=widget.getElementsByTagName("input");
		if(inputs.length>0){
			var rFaceta=inputs[0].name;
			var urlRestricoes=constURLrestr;
			var checkedFaceta=false;
			var primeiraFaceta=true;
			for(var i=0; i<inputs.length; i++){
				if(inputs[i].type==="checkbox"){
					if(rFaceta!==(inputs[i].name)){
						rFaceta=inputs[i].name;
						if(checkedFaceta){
							urlRestricoes+="]";
							checkedFaceta=false;
						}
					}
					if((inputs[i]).checked){
						var valueC=inputs[i].value;
						if(!checkedFaceta){
							if(!primeiraFaceta){
								urlRestricoes+="&";
							}
							else{
								primeiraFaceta=false;
							}
							urlRestricoes+=rFaceta;
							urlRestricoes+="=[";
							checkedFaceta=true;
						}
						else{
							urlRestricoes+=",";
						}
						urlRestricoes+=valueC;
					}
					
					if(i===inputs.length-1 && checkedFaceta){
						urlRestricoes+="]";
					}
				}
			}
			var parentId=widgetId+"-Resultados";
			var resultados=document.getElementById(parentId);
			while(resultados.firstChild){
				resultados.removeChild(resultados.firstChild);
			}		
			makeXmlHttpGetCall(urlRestricoes, null, true, false, preencheResultados, resultados, parentId);
		}
	}
}
function constroiFacetas(cFacetas,contentorId){
	placeLoader(cFacetas);
	var acordion=createDiv("panel-group", randomId("acordion"), "tablist");
	conteudo[cFacetas.id]=acordion;
	loading[cFacetas.id]=0;
	nextStep[cFacetas.id]=[procurar(),contentorId];
	
	makeXmlHttpGetCall(urlFacetas, null, true, true, preencheFacetas, cFacetas.id, cFacetas.id);
	
	var submitBtn=createElement("button", "btn btn-default", "btn_procurar", null, null);
	submitBtn.setAttribute("type", "button");
	submitBtn.setAttribute("aria-label", "Center Align");
	submitBtn.setAttribute("onclick","procurar('"+contentorId+"');");
	var icone=createElement("span",iconeSearch, null, null, null);
	submitBtn.appendChild(icone);
	submitBtn.appendChild(document.createTextNode(" Procurar Resultados"));
	acordion.appendChild(submitBtn);
}
function constroiWidget(contentor){
	contentor.className+=" widget_imoveis";
	var linha=createDiv("row", null, null);
	var cFacetas=createDiv("col-xs-4 col-md-2", contentor.id+"-Facetas", null);
	var cResultados=createDiv("col-xs-14 col-md-10 well", contentor.id+"-Resultados", null);
	
	linha.appendChild(cFacetas);
	linha.appendChild(cResultados);
	contentor.appendChild(linha);
	
	constroiFacetas(cFacetas,contentor.id);
}

document.getElementsByTagName("body").item(0).onload=function(){
	(function (){
		function loadScript(url, callback){
			var script = document.createElement("script");
			script.type = "text/javascript";
			if (script.readyState){
				//Bug do IE
				script.onreadystatechange = function (){
					if (script.readyState === "loaded" || script.readyState === "complete") {
						script.onreadystatechange = null;
						callback();
					}
				};
			}
			else {
				script.onload = function (){
					callback();
				};
			}
			script.src = url;
			document.getElementsByTagName("head")[0].appendChild(script);
		}
		//carregar jquery
		loadScript(jqueryURL, function (){
			 //carregar js do bootstrap
			 loadScript(jsBootstrap, function (){
				 //carregar estilos bootstrap
				 var bootstrap=document.createElement("link");
				 bootstrap.setAttribute("rel","stylesheet");
				 bootstrap.setAttribute("href",estBootstrap);
				 document.getElementsByTagName("head")[0].appendChild(bootstrap);
				 
				 //carregar estilos personalizados
				 var estilos=document.createElement("link");
				 estilos.setAttribute("rel","stylesheet");
				 estilos.setAttribute("href",estiloW);
				 document.getElementsByTagName("head")[0].appendChild(estilos);
			});
		});
	})();
	
	//código a executar quando o documento foi carregado
	if(typeof places==='undefined'){
		places=["body"];
	}
	
	for(var place of places) {
		var elem=document.getElementById(place);
		limpa(elem);
		constroiWidget(elem);
	}
};