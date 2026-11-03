use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "CreativeTrigger")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    #[sea_orm(column_name = "creativeId")]
    pub creative_id: String,
    #[sea_orm(column_name = "actionClassId")]
    pub action_class_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::creative::Entity",
        from = "Column::CreativeId",
        to = "super::creative::Column::Id",
        on_delete = "Cascade"
    )]
    Creative,
    #[sea_orm(
        belongs_to = "super::action_class::Entity",
        from = "Column::ActionClassId",
        to = "super::action_class::Column::Id",
        on_delete = "Cascade"
    )]
    ActionClass,
}

impl Related<super::creative::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Creative.def()
    }
}

impl Related<super::action_class::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ActionClass.def()
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
