use super::link_button;
use crate::{
    constants::{COLORS, EMOJIS, FAB},
    models::ProductInfos,
};
use twilight_model::channel::message::{Component, component::SeparatorSpacingSize};
use twilight_util::builder::message::{
    ActionRowBuilder, ContainerBuilder, SeparatorBuilder, TextDisplayBuilder,
};

fn product_row(label: &str, product: &ProductInfos) -> Component {
    let icons = &EMOJIS.emojis_static;
    Component::ActionRow(
        ActionRowBuilder::new()
            .component(link_button(label, icons.icons_f, &product.product_url))
            .component(link_button("Documentation", icons.icons_d, &product.doc_url))
            .build(),
    )
}

pub fn fab_component() -> Component {
    let separator = SeparatorBuilder::new()
        .spacing(SeparatorSpacingSize::Small)
        .build();

    let mut container = ContainerBuilder::new()
        .accent_color(Some(COLORS.green))
        .component(Component::TextDisplay(
            TextDisplayBuilder::new("### CHECK OUT MY FAB PRODUCTS").build(),
        ));

    let products = [
        ("Engine User Settings", &FAB.engine_user_setings),
        ("Internet Protocol", &FAB.internet_protocol),
    ];

    for (label, product) in products {
        container = container
            .component(Component::Separator(separator.clone()))
            .component(product_row(label, product))
            .component(Component::TextDisplay(
                TextDisplayBuilder::new(&product.product_desc).build(),
            ));
    }

    Component::Container(container.build())
}
