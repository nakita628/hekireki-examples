use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "sheets")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub space_id: String,
    pub host_id: String,
    pub title: String,
    pub description: Option<String>,
    #[sea_orm(unique)]
    pub url_id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    #[sea_orm(default_value = false)]
    pub deleted: bool,
    pub deleted_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::space::Entity",
        from = "Column::SpaceId",
        to = "super::space::Column::Id",
        on_delete = "Cascade"
    )]
    Space,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::HostId",
        to = "super::user::Column::Id",
        on_delete = "Cascade"
    )]
    Host,
    #[sea_orm(has_many = "super::sheet_slot::Entity")]
    Slots,
}

impl Related<super::space::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Space.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Host.def()
    }
}

impl Related<super::sheet_slot::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Slots.def()
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
