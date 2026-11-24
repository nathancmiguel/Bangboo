use serde::{Deserialize, Serialize};

#[derive(Serialize, Deserialize)]
pub struct Fab {
    pub engine_user_setings: ProductInfos,

    pub internet_protocol: ProductInfos,
}


#[derive(Serialize, Deserialize)]
pub struct ProductInfos {
    pub product_name: String,

    pub product_desc: String,

    pub thumb_link: String,

    pub product_url: String,

    pub doc_url: String,
}