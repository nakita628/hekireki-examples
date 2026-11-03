use super::link_scope::LinkScope;
use super::webhook_receiver::WebhookReceiver;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Webhook")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "installationId")]
    pub installation_id: Option<String>,
    #[sea_orm(default_value = "user")]
    pub receiver: WebhookReceiver,
    pub name: String,
    #[sea_orm(column_type = "Text")]
    pub url: String,
    pub secret: String,
    pub triggers: Json,
    #[sea_orm(column_name = "linkScope")]
    pub link_scope: Option<LinkScope>,
    #[sea_orm(column_name = "consecutiveFailures", default_value = 0)]
    pub consecutive_failures: i32,
    #[sea_orm(column_name = "lastFailedAt")]
    pub last_failed_at: Option<DateTime>,
    #[sea_orm(column_name = "disabledAt")]
    pub disabled_at: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(
        belongs_to = "super::installed_integration::Entity",
        from = "Column::InstallationId",
        to = "super::installed_integration::Column::Id",
        on_delete = "Cascade"
    )]
    InstalledIntegration,
    #[sea_orm(has_many = "super::link_webhook::Entity")]
    Links,
    #[sea_orm(has_many = "super::folder_webhook::Entity")]
    Folders,
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::installed_integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::InstalledIntegration.def()
    }
}

impl Related<super::link_webhook::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Links.def()
    }
}

impl Related<super::folder_webhook::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Folders.def()
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
