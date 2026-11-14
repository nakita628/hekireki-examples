use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ScenarioVersion")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "scenarioId")]
    pub scenario_id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    pub version: i32,
    #[sea_orm(column_name = "authorId")]
    pub author_id: Option<String>,
    #[sea_orm(column_name = "authorLabel")]
    pub author_label: String,
    #[sea_orm(column_name = "changeDescription")]
    pub change_description: Option<String>,
    pub snapshot: Json,
    #[sea_orm(column_name = "schemaVersion", default_value = 1)]
    pub schema_version: i32,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::scenario::Entity",
        from = "Column::ScenarioId",
        to = "super::scenario::Column::Id",
        on_delete = "Cascade"
    )]
    Scenario,
}

impl Related<super::scenario::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Scenario.def()
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
