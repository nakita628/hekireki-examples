use super::schedule_generator_type::ScheduleGeneratorType;
use super::schedule_type::ScheduleType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "TaskSchedule")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(default_value = "IMPERATIVE")]
    pub r#type: ScheduleType,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "friendlyId")]
    pub friendly_id: String,
    #[sea_orm(column_name = "taskIdentifier")]
    pub task_identifier: String,
    #[sea_orm(column_name = "deduplicationKey")]
    pub deduplication_key: String,
    #[sea_orm(column_name = "userProvidedDeduplicationKey", default_value = false)]
    pub user_provided_deduplication_key: bool,
    #[sea_orm(column_name = "generatorExpression")]
    pub generator_expression: String,
    #[sea_orm(column_name = "generatorDescription", default_value = "")]
    pub generator_description: String,
    #[sea_orm(column_name = "generatorType", default_value = "CRON")]
    pub generator_type: ScheduleGeneratorType,
    #[sea_orm(default_value = "UTC")]
    pub timezone: String,
    #[sea_orm(column_name = "windowDurationSeconds")]
    pub window_duration_seconds: Option<i32>,
    #[sea_orm(column_name = "windowPercentage")]
    pub window_percentage: Option<i32>,
    #[sea_orm(column_name = "defaultWindowDurationSeconds")]
    pub default_window_duration_seconds: Option<i32>,
    #[sea_orm(column_name = "minimumWindowDurationSeconds")]
    pub minimum_window_duration_seconds: Option<i32>,
    #[sea_orm(column_name = "externalId")]
    pub external_id: Option<String>,
    #[sea_orm(column_name = "lastRunTriggeredAt")]
    pub last_run_triggered_at: Option<DateTime>,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(default_value = true)]
    pub active: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(has_many = "super::task_schedule_instance::Entity")]
    Instances,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::task_schedule_instance::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Instances.def()
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
