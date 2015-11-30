function getAlertElement(bsStateClass, message) {
    return '<div class="alert alert-' + bsStateClass + '">'
        + '<button class="close" data-dismiss="alert" type="button">×</button>'
        + '<div>' + message + '</div>'
        + '</div>';
}

function addOrReplaceAlert($alert, $container) {
    var $existingAlert = $container.children('.alert');
    if ($existingAlert.length)
        $existingAlert.remove();
    $container.prepend($alert);
}