use skia_safe::{
    Canvas, CubicResampler, Data, FilterMode, FontMgr, Image, Paint, PathBuilder, Point, Rect,
    SamplingOptions, Size, scalar, surfaces,
    textlayout::{FontCollection, ParagraphBuilder, ParagraphStyle, TextStyle, TypefaceFontProvider},
};

/// Desenha `image` recortada em círculo. O `save`/`restore` agora vive aqui dentro:
/// antes o `restore()` desta função dependia de um `save()` feito por quem a chamava.
pub fn draw_circle(canvas: &Canvas, image: &Image, x: f32, y: f32, radius: f32) {
    let mut clip_path = PathBuilder::new();
    clip_path.add_circle(Point { x, y }, radius, None);
    let clip_path = clip_path.snapshot();

    canvas.save();
    canvas.clip_path(&clip_path, None, true);
    canvas.draw_image(
        image,
        Point {
            x: x - radius,
            y: y - radius,
        },
        None,
    );
    canvas.restore();
}

pub fn load_image_from_bytes(slice: &[u8]) -> Option<Image> {
    Image::from_encoded(Data::new_copy(slice))
}

pub fn resize_image(image: &Image, new_width: u32, new_height: u32) -> Option<Image> {
    let mut surface = surfaces::raster_n32_premul((new_width as i32, new_height as i32))?;
    let canvas = surface.canvas();

    let size = Rect::from_size(Size::new(new_width as f32, new_height as f32));

    let mut sampling = SamplingOptions::default();
    sampling.cubic = CubicResampler::mitchell();
    sampling.filter = FilterMode::Linear;

    let mut paint = Paint::default();
    paint.set_anti_alias(true);

    canvas.draw_image_rect_with_sampling_options(image, None, size, sampling, &paint);

    Some(surface.image_snapshot())
}

/// Monta uma `FontCollection` com a fonte embutida registrada como "CanvasFont".
/// Faça isso uma vez por fonte e reutilize em vários textos (antes era refeito a cada texto).
pub fn font_collection(font: &[u8]) -> Option<FontCollection> {
    let typeface = FontMgr::new().new_from_data(Data::new_copy(font), None)?;

    let mut provider = TypefaceFontProvider::new();
    provider.register_typeface(typeface, Some("CanvasFont"));

    let mut collection = FontCollection::new();
    collection.set_default_font_manager(Some(provider.into()), None);
    Some(collection)
}

pub fn draw_text(
    canvas: &Canvas,
    fonts: &FontCollection,
    text: &str,
    font_size: f32,
    x: f32,
    y: f32,
) {
    let mut text_style = TextStyle::new();
    text_style.set_font_size(font_size);
    text_style.set_color(0xFFFFFFFF);
    text_style.set_font_families(&["CanvasFont"]);

    let mut builder = ParagraphBuilder::new(&ParagraphStyle::new(), fonts.clone());
    builder.push_style(&text_style);
    builder.add_text(text);

    let mut paragraph = builder.build();
    paragraph.layout(2800.0);
    paragraph.paint(canvas, Point::new(x as scalar, y as scalar));
}
