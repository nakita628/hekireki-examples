use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "RuleHistory")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "ruleId")]
    pub rule_id: String,
    pub version: i32,
    #[sea_orm(column_name = "triggerType")]
    pub trigger_type: String,
    #[sea_orm(column_name = "promptText")]
    pub prompt_text: Option<String>,
    pub name: String,
    pub instructions: Option<String>,
    pub enabled: bool,
    pub automate: bool,
    #[sea_orm(column_name = "runOnThreads")]
    pub run_on_threads: bool,
    #[sea_orm(column_name = "conditionalOperator")]
    pub conditional_operator: String,
    pub from: Option<String>,
    pub to: Option<String>,
    pub subject: Option<String>,
    pub body: Option<String>,
    #[sea_orm(column_name = "categoryFilterType")]
    pub category_filter_type: Option<String>,
    #[sea_orm(column_name = "systemType")]
    pub system_type: Option<String>,
    pub actions: Json,
    #[sea_orm(column_name = "categoryFilters")]
    pub category_filters: Option<Json>,
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
}

impl Related<super::rule::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Rule.def()
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
