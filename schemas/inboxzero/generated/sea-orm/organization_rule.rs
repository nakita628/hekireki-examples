use super::logical_operator::LogicalOperator;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "OrganizationRule")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(default_value = true)]
    pub enabled: bool,
    pub name: String,
    pub instructions: Option<String>,
    #[sea_orm(column_name = "runOnThreads", default_value = false)]
    pub run_on_threads: bool,
    #[sea_orm(column_name = "conditionalOperator", default_value = "AND")]
    pub conditional_operator: LogicalOperator,
    pub from: Option<String>,
    pub to: Option<String>,
    pub subject: Option<String>,
    pub body: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::OrganizationId",
        to = "super::organization::Column::Id",
        on_delete = "Cascade"
    )]
    Organization,
    #[sea_orm(has_many = "super::organization_rule_action::Entity")]
    Actions,
    #[sea_orm(has_many = "super::rule::Entity")]
    MemberRules,
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::organization_rule_action::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Actions.def()
    }
}

impl Related<super::rule::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::MemberRules.def()
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
