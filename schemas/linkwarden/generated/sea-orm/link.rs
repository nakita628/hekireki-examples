use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Link")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(default_value = "")]
    pub name: String,
    #[sea_orm(default_value = "url")]
    pub r#type: String,
    #[sea_orm(default_value = "")]
    pub description: String,
    #[sea_orm(column_name = "createdById")]
    pub created_by_id: Option<i32>,
    #[sea_orm(column_name = "collectionId")]
    pub collection_id: i32,
    #[sea_orm(column_name = "collectionOrder")]
    pub collection_order: f64,
    pub icon: Option<String>,
    #[sea_orm(column_name = "iconWeight")]
    pub icon_weight: Option<String>,
    pub color: Option<String>,
    pub url: Option<String>,
    #[sea_orm(column_name = "textContent")]
    pub text_content: Option<String>,
    pub preview: Option<String>,
    pub image: Option<String>,
    pub pdf: Option<String>,
    pub readable: Option<String>,
    pub monolith: Option<String>,
    #[sea_orm(column_name = "clientSide", default_value = false)]
    pub client_side: bool,
    #[sea_orm(column_name = "aiTagged", default_value = false)]
    pub ai_tagged: bool,
    #[sea_orm(column_name = "metaDescription")]
    pub meta_description: Option<String>,
    #[sea_orm(column_name = "indexVersion")]
    pub index_version: Option<i32>,
    #[sea_orm(column_name = "lastPreserved")]
    pub last_preserved: Option<DateTime>,
    #[sea_orm(column_name = "importDate")]
    pub import_date: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::CreatedById",
        to = "super::user::Column::Id",
        on_delete = "Cascade"
    )]
    CreatedBy,
    #[sea_orm(
        belongs_to = "super::collection::Entity",
        from = "Column::CollectionId",
        to = "super::collection::Column::Id",
        on_delete = "Cascade"
    )]
    Collection,
    #[sea_orm(has_many = "super::highlight::Entity")]
    Highlight,
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CreatedBy.def()
    }
}

impl Related<super::collection::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Collection.def()
    }
}

impl Related<super::highlight::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Highlight.def()
    }
}

impl Related<super::tag::Entity> for Entity {
    fn to() -> RelationDef {
        super::link_to_tag::Relation::Tag.def()
    }
    fn via() -> Option<RelationDef> {
        Some(super::link_to_tag::Relation::Link.def().rev())
    }
}

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
