
function register_version_variable_for_yoast() {
    if ( class_exists( 'WPSEO_Frontend' ) ) {
        wpseo_register_var_replacement( '%%Version%%', 'get_custom_version_meta', 'advanced', 'Version from custom field' );
    }
}
add_action( 'wpseo_register_extra_replacements', 'register_version_variable_for_yoast' );

// Hàm để lấy giá trị version từ custom field
function get_custom_version_meta( $replacement, $args ) {
    global $post;

    // Lấy giá trị từ custom field 'datos_informacion'
    $datos_informacion = get_post_meta( $post->ID, 'datos_informacion', true );

    // Kiểm tra xem 'version' có tồn tại trong custom field không
    if ( is_array( $datos_informacion ) && isset( $datos_informacion['version'] ) ) {
        return "".esc_html( $datos_informacion['version'] );
    }

    return '';
}
function force_update_yoast_meta_preview() {
    if ( get_the_ID() ) { // Kiểm tra nếu có giá trị post ID
        $datos_informacion = get_post_meta( get_the_ID(), 'datos_informacion', true );

        // Kiểm tra nếu datos_informacion và version tồn tại
        if ( is_array( $datos_informacion ) && isset( $datos_informacion['version'] ) ) {
            ?>
            <script type="text/javascript">
                document.addEventListener('DOMContentLoaded', function() {
                    // Lấy giá trị thực của version từ PHP
                    var version = '<?php echo "" . esc_js( $datos_informacion['version'] ); ?>';
                    if (!version) {
                        version = '1.0'; // Giá trị mặc định nếu không có version
                    }

                    // Hàm thay thế %%Version%% trong thẻ #wpseo_meta
                    function updateYoastMetaPreview() {
                        var wpseoMeta = document.querySelector('.hFjSGk'); // Tìm thẻ với id là wpseo_meta
                        
                        if (wpseoMeta) {
                            var metaText = wpseoMeta.innerHTML; // Lấy nội dung của thẻ

                            // Kiểm tra và thay thế %%Version%%
                            if (metaText.includes('%%Version%%')) {
                                var newMetaText = metaText.replace(/%%Version%%/g, version);
                                wpseoMeta.innerHTML = newMetaText; // Cập nhật lại nội dung thẻ
                            }
                        }

						var wpseoMeta2 = document.querySelector('.iEkGYV'); // Tìm thẻ với id là wpseo_meta
                        
                        if (wpseoMeta2) {
                            var metaText = wpseoMeta2.innerHTML; // Lấy nội dung của thẻ

                            // Kiểm tra và thay thế %%Version%%
                            if (metaText.includes('%%Version%%')) {
                                var newMetaText = metaText.replace(/%%Version%%/g, version);
                                wpseoMeta2.innerHTML = newMetaText; // Cập nhật lại nội dung thẻ
                            }
                        }
                    }

                    // Sử dụng MutationObserver để theo dõi sự thay đổi của DOM
                    var targetNode = document.querySelector('body'); // Theo dõi toàn bộ body
                    if (targetNode) {
                        var observer = new MutationObserver(function(mutationsList, observer) {
                            updateYoastMetaPreview(); // Gọi hàm update mỗi khi DOM thay đổi
                        });

                        // Theo dõi các thay đổi trong cây DOM
                        observer.observe(targetNode, {
                            childList: true,
                            subtree: true
                        });

                        // Gọi hàm lần đầu để cập nhật ngay lập tức
                        updateYoastMetaPreview();
                    }
                });
            </script>
            <?php
        }
    }
}
add_action('admin_footer', 'force_update_yoast_meta_preview');

