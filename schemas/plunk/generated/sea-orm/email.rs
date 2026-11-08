use super::email_source_type::EmailSourceType;
use super::email_status::EmailStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "emails")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "contactId")]
    pub contact_id: String,
    #[sea_orm(column_name = "toName")]
    pub to_name: Option<String>,
    pub subject: String,
    pub body: String,
    pub from: String,
    #[sea_orm(column_name = "fromName")]
    pub from_name: Option<String>,
    #[sea_orm(column_name = "replyTo")]
    pub reply_to: Option<String>,
    pub headers: Option<Json>,
    pub attachments: Option<Json>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "messageId")]
    pub message_id: Option<String>,
    #[sea_orm(column_name = "sourceType")]
    pub source_type: EmailSourceType,
    #[sea_orm(column_name = "templateId")]
    pub template_id: Option<String>,
    #[sea_orm(column_name = "campaignId")]
    pub campaign_id: Option<String>,
    #[sea_orm(column_name = "workflowExecutionId")]
    pub workflow_execution_id: Option<String>,
    #[sea_orm(column_name = "workflowStepExecutionId")]
    pub workflow_step_execution_id: Option<String>,
    #[sea_orm(default_value = "PENDING")]
    pub status: EmailStatus,
    #[sea_orm(column_name = "scheduledFor")]
    pub scheduled_for: Option<DateTime>,
    #[sea_orm(column_name = "sentAt")]
    pub sent_at: Option<DateTime>,
    #[sea_orm(column_name = "deliveredAt")]
    pub delivered_at: Option<DateTime>,
    #[sea_orm(column_name = "openedAt")]
    pub opened_at: Option<DateTime>,
    #[sea_orm(column_name = "clickedAt")]
    pub clicked_at: Option<DateTime>,
    #[sea_orm(column_name = "bouncedAt")]
    pub bounced_at: Option<DateTime>,
    #[sea_orm(column_name = "complainedAt")]
    pub complained_at: Option<DateTime>,
    #[sea_orm(column_name = "unsubscribedAt")]
    pub unsubscribed_at: Option<DateTime>,
    #[sea_orm(default_value = 0)]
    pub opens: i32,
    #[sea_orm(default_value = 0)]
    pub clicks: i32,
    #[sea_orm(default_value = false)]
    pub simulated: bool,
    pub error: Option<String>,
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
        belongs_to = "super::contact::Entity",
        from = "Column::ContactId",
        to = "super::contact::Column::Id",
        on_delete = "Cascade"
    )]
    Contact,
    #[sea_orm(
        belongs_to = "super::template::Entity",
        from = "Column::TemplateId",
        to = "super::template::Column::Id"
    )]
    Template,
    #[sea_orm(
        belongs_to = "super::campaign::Entity",
        from = "Column::CampaignId",
        to = "super::campaign::Column::Id",
        on_delete = "Cascade"
    )]
    Campaign,
    #[sea_orm(
        belongs_to = "super::workflow_execution::Entity",
        from = "Column::WorkflowExecutionId",
        to = "super::workflow_execution::Column::Id",
        on_delete = "Cascade"
    )]
    WorkflowExecution,
    #[sea_orm(
        belongs_to = "super::workflow_step_execution::Entity",
        from = "Column::WorkflowStepExecutionId",
        to = "super::workflow_step_execution::Column::Id",
        on_delete = "Cascade"
    )]
    WorkflowStepExecution,
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(has_many = "super::event::Entity")]
    Events,
}

impl Related<super::contact::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Contact.def()
    }
}

impl Related<super::template::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Template.def()
    }
}

impl Related<super::campaign::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Campaign.def()
    }
}

impl Related<super::workflow_execution::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkflowExecution.def()
    }
}

impl Related<super::workflow_step_execution::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkflowStepExecution.def()
    }
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::event::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Events.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    fn new() -> Self {
        Self {
            id: Set(uuid::Uuid::new_v4().to_string()),
            ..ActiveModelTrait::default()
        }
    }

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
