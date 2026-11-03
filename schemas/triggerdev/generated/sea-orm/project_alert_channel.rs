use super::project_alert_channel_type::ProjectAlertChannelType;
use super::project_alert_type::ProjectAlertType;
use super::runtime_environment_type::RuntimeEnvironmentType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ProjectAlertChannel")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "friendlyId")]
    pub friendly_id: String,
    #[sea_orm(column_name = "deduplicationKey")]
    pub deduplication_key: String,
    #[sea_orm(column_name = "userProvidedDeduplicationKey", default_value = false)]
    pub user_provided_deduplication_key: bool,
    #[sea_orm(column_name = "integrationId")]
    pub integration_id: Option<String>,
    #[sea_orm(default_value = true)]
    pub enabled: bool,
    pub r#type: ProjectAlertChannelType,
    pub name: String,
    pub properties: Json,
    #[sea_orm(column_name = "alertTypes")]
    pub alert_types: Vec<ProjectAlertType>,
    #[sea_orm(column_name = "environmentTypes")]
    pub environment_types: Vec<RuntimeEnvironmentType>,
    #[sea_orm(column_name = "errorAlertConfig")]
    pub error_alert_config: Option<Json>,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organization_integration::Entity",
        from = "Column::IntegrationId",
        to = "super::organization_integration::Column::Id",
        on_update = "Cascade",
        on_delete = "SetNull"
    )]
    Integration,
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_update = "Cascade",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(has_many = "super::project_alert::Entity")]
    Alerts,
    #[sea_orm(has_many = "super::project_alert_storage::Entity")]
    AlertStorages,
}

impl Related<super::organization_integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Integration.def()
    }
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::project_alert::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Alerts.def()
    }
}

impl Related<super::project_alert_storage::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AlertStorages.def()
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
