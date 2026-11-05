use super::executed_rule_status::ExecutedRuleStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ExecutedRule")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "threadId")]
    pub thread_id: String,
    #[sea_orm(column_name = "messageId")]
    pub message_id: String,
    pub status: ExecutedRuleStatus,
    pub automated: bool,
    pub reason: Option<String>,
    #[sea_orm(column_name = "matchMetadata")]
    pub match_metadata: Option<Json>,
    #[sea_orm(column_name = "ruleId")]
    pub rule_id: Option<String>,
    #[sea_orm(column_name = "emailAccountId")]
    pub email_account_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::rule::Entity",
        from = "Column::RuleId",
        to = "super::rule::Column::Id"
    )]
    Rule,
    #[sea_orm(
        belongs_to = "super::email_account::Entity",
        from = "Column::EmailAccountId",
        to = "super::email_account::Column::Id",
        on_delete = "Cascade"
    )]
    EmailAccount,
    #[sea_orm(has_many = "super::executed_action::Entity")]
    ActionItems,
    #[sea_orm(has_many = "super::scheduled_action::Entity")]
    ScheduledActions,
}

impl Related<super::rule::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Rule.def()
    }
}

impl Related<super::email_account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmailAccount.def()
    }
}

impl Related<super::executed_action::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ActionItems.def()
    }
}

impl Related<super::scheduled_action::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ScheduledActions.def()
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
