use super::classification_feedback_event_type::ClassificationFeedbackEventType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ClassificationFeedback")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    pub sender: String,
    #[sea_orm(column_name = "eventType")]
    pub event_type: ClassificationFeedbackEventType,
    #[sea_orm(column_name = "ruleId")]
    pub rule_id: String,
    #[sea_orm(column_name = "emailAccountId")]
    pub email_account_id: String,
    #[sea_orm(column_name = "threadId")]
    pub thread_id: String,
    #[sea_orm(column_name = "messageId")]
    pub message_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::rule::Entity",
        from = "Column::RuleId",
        to = "super::rule::Column::Id",
        on_delete = "Cascade"
    )]
    Rule,
    #[sea_orm(
        belongs_to = "super::email_account::Entity",
        from = "Column::EmailAccountId",
        to = "super::email_account::Column::Id",
        on_delete = "Cascade"
    )]
    EmailAccount,
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
