$(".option").click(function() {
	$(".option").removeClass("selected");
	$(this).addClass("selected");
	$(".message").text("Logujesz się jako: " + $(this).text());
	$("option").removeAttr("selected", "selected");
	$("option#" + $(this).attr("id") + "").attr("selected", "selected");
});