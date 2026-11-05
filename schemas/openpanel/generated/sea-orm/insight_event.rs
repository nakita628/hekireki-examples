use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "insight_events")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_type = "Uuid")]
    pub id: String,
    #[sea_orm(column_name = "insightId", column_type = "Uuid")]
    pub insight_id: String,
    #[sea_orm(column_name = "eventKind")]
    pub event_kind: String,
    #[sea_orm(column_name = "changeFrom")]
    pub change_from: Option<Json>,
    #[sea_orm(column_name = "changeTo")]
    pub change_to: Option<Json>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::project_insight::Entity",
        from = "Column::InsightId",
        to = "super::project_insight::Column::Id",
        on_delete = "Cascade"
    )]
    Insight,
}

impl Related<super::project_insight::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Insight.def()
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
        Ok(self)
    }
}
