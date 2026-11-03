use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Collection")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    pub name: String,
    #[sea_orm(default_value = "")]
    pub description: String,
    pub icon: Option<String>,
    #[sea_orm(column_name = "iconWeight")]
    pub icon_weight: Option<String>,
    #[sea_orm(default_value = "#0ea5e9")]
    pub color: String,
    #[sea_orm(column_name = "parentId")]
    pub parent_id: Option<i32>,
    #[sea_orm(column_name = "isPublic", default_value = false)]
    pub is_public: bool,
    #[sea_orm(column_name = "ownerId")]
    pub owner_id: i32,
    #[sea_orm(column_name = "createdById")]
    pub created_by_id: Option<i32>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::collection::Entity",
        from = "Column::ParentId",
        to = "super::collection::Column::Id",
        on_delete = "Cascade"
    )]
    Parent,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::OwnerId",
        to = "super::user::Column::Id",
        on_delete = "Cascade"
    )]
    Owner,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::CreatedById",
        to = "super::user::Column::Id"
    )]
    CreatedBy,
    #[sea_orm(
        has_many = "super::collection::Entity",
        from = "Column::Id",
        to = "super::collection::Column::ParentId"
    )]
    SubCollections,
    #[sea_orm(has_many = "super::users_and_collections::Entity")]
    Members,
    #[sea_orm(has_many = "super::link::Entity")]
    Links,
    #[sea_orm(has_many = "super::rss_subscription::Entity")]
    RssSubscriptions,
    #[sea_orm(has_many = "super::dashboard_section::Entity")]
    DashboardSection,
}

impl Related<super::collection::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Parent.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Owner.def()
    }
}

impl Related<super::users_and_collections::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Members.def()
    }
}

impl Related<super::link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Links.def()
    }
}

impl Related<super::rss_subscription::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::RssSubscriptions.def()
    }
}

impl Related<super::dashboard_section::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DashboardSection.def()
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
