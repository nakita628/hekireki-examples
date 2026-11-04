use super::prisma_date_time::PrismaDateTime;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "model_router_rules")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    pub router_id: i32,
    #[sea_orm(default_value = true)]
    pub enabled: bool,
    pub priority: i32,
    #[sea_orm(default_value = "calculated")]
    pub r#type: String,
    pub title: String,
    pub description: Option<String>,
    pub condition_logic: Option<String>,
    pub conditions: Option<String>,
    pub route_provider: String,
    pub route_model: String,
    pub created_by: Option<i32>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: PrismaDateTime,
    #[sea_orm(column_name = "lastUpdatedAt")]
    pub last_updated_at: PrismaDateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::model_routers::Entity",
        from = "Column::RouterId",
        to = "super::model_routers::Column::Id",
        on_delete = "Cascade"
    )]
    Router,
}

impl Related<super::model_routers::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Router.def()
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
            self.created_at = Set(now.into());
        }
        if insert && self.last_updated_at.is_not_set() {
            self.last_updated_at = Set(now.into());
        }
        Ok(self)
    }
}
