use super::action_type::ActionType;
use super::scheduled_action_status::ScheduledActionStatus;
use super::scheduling_status::SchedulingStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ScheduledAction")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "executedRuleId")]
    pub executed_rule_id: String,
    #[sea_orm(column_name = "actionType")]
    pub action_type: ActionType,
    #[sea_orm(column_name = "messageId")]
    pub message_id: String,
    #[sea_orm(column_name = "threadId")]
    pub thread_id: String,
    #[sea_orm(column_name = "scheduledFor")]
    pub scheduled_for: DateTime,
    #[sea_orm(column_name = "emailAccountId")]
    pub email_account_id: String,
    #[sea_orm(default_value = "PENDING")]
    pub status: ScheduledActionStatus,
    #[sea_orm(column_name = "schedulingStatus", default_value = "PENDING")]
    pub scheduling_status: SchedulingStatus,
    pub label: Option<String>,
    #[sea_orm(column_name = "labelId")]
    pub label_id: Option<String>,
    pub subject: Option<String>,
    pub content: Option<String>,
    pub to: Option<String>,
    pub cc: Option<String>,
    pub bcc: Option<String>,
    pub url: Option<String>,
    #[sea_orm(column_name = "folderName")]
    pub folder_name: Option<String>,
    #[sea_orm(column_name = "folderId")]
    pub folder_id: Option<String>,
    #[sea_orm(column_name = "scheduledId")]
    pub scheduled_id: Option<String>,
    #[sea_orm(column_name = "staticAttachments")]
    pub static_attachments: Option<Json>,
    #[sea_orm(column_name = "selectedAttachments")]
    pub selected_attachments: Option<Json>,
    #[sea_orm(column_name = "executedAt")]
    pub executed_at: Option<DateTime>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "executedActionId")]
    pub executed_action_id: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::executed_rule::Entity",
        from = "Column::ExecutedRuleId",
        to = "super::executed_rule::Column::Id",
        on_delete = "Cascade"
    )]
    ExecutedRule,
    #[sea_orm(
        belongs_to = "super::executed_action::Entity",
        from = "Column::ExecutedActionId",
        to = "super::executed_action::Column::Id",
        on_delete = "Cascade"
    )]
    ExecutedAction,
    #[sea_orm(
        belongs_to = "super::email_account::Entity",
        from = "Column::EmailAccountId",
        to = "super::email_account::Column::Id",
        on_delete = "Cascade"
    )]
    EmailAccount,
}

impl Related<super::executed_rule::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ExecutedRule.def()
    }
}

impl Related<super::executed_action::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ExecutedAction.def()
    }
}

impl Related<super::email_account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmailAccount.def()
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
