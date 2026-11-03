use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "instance_settings")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: i32,
    #[sea_orm(default_value = false)]
    pub disable_user_registration: bool,
    #[sea_orm(unique)]
    #[sea_orm(column_type = "Uuid")]
    pub instance_id: String,
    pub app_name: Option<String>,
    pub primary_color: Option<String>,
    pub primary_color_dark: Option<String>,
    pub logo: Option<String>,
    pub logo_dark: Option<String>,
    pub logo_icon: Option<String>,
    pub hide_attribution: Option<bool>,
    #[sea_orm(default_value = "[]")]
    pub footer_links: Json,
    pub created_at: DateTime,
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
