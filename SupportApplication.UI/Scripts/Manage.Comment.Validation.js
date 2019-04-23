jQuery(document).ready(function() {

	$("textarea#comment").keyup(validation);
	document.getElementById("SaveStatus").disabled = true;
});

function validation() {
	var value = $("textarea#comment").val();
	if (value.length !== 0) {
		document.getElementById("SaveStatus").disabled = false;
	} else {
		document.getElementById("SaveStatus").disabled = true;
	}
}
