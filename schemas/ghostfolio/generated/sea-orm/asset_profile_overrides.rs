use super::asset_class::AssetClass;
use super::asset_sub_class::AssetSubClass;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "SymbolProfileOverrides")]
pub struct Model {
    #[sea_orm(column_name = "assetClass")]
    pub asset_class: Option<AssetClass>,
    #[sea_orm(column_name = "assetSubClass")]
    pub asset_sub_class: Option<AssetSubClass>,
    #[sea_orm(default_value = "[]")]
    pub countries: Option<Json>,
    #[sea_orm(default_value = "[]")]
    pub holdings: Option<Json>,
    pub name: Option<String>,
    #[sea_orm(default_value = "[]")]
    pub sectors: Option<Json>,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "symbolProfileId")]
    pub symbol_profile_id: String,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    pub url: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::symbol_profile::Entity",
        from = "Column::SymbolProfileId",
        to = "super::symbol_profile::Column::Id",
        on_delete = "Cascade"
    )]
    SymbolProfile,
}

impl Related<super::symbol_profile::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SymbolProfile.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, _insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
