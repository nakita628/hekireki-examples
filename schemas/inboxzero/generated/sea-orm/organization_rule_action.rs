use super::action_type::ActionType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "OrganizationRuleAction")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "organizationRuleId")]
    pub organization_rule_id: String,
    pub r#type: ActionType,
    pub label: Option<String>,
    pub subject: Option<String>,
    pub content: Option<String>,
    pub to: Option<String>,
    pub cc: Option<String>,
    pub bcc: Option<String>,
    pub url: Option<String>,
    #[sea_orm(column_name = "folderName")]
    pub folder_name: Option<String>,
    #[sea_orm(column_name = "delayInMinutes")]
    pub delay_in_minutes: Option<i32>,
    #[sea_orm(column_name = "staticAttachments")]
    pub static_attachments: Option<Json>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organization_rule::Entity",
        from = "Column::OrganizationRuleId",
        to = "super::organization_rule::Column::Id",
        on_delete = "Cascade"
    )]
    OrganizationRule,
}

impl Related<super::organization_rule::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganizationRule.def()
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
