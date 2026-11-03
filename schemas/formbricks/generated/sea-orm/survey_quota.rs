use super::survey_quota_action::SurveyQuotaAction;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "SurveyQuota")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    #[sea_orm(column_name = "surveyId")]
    pub survey_id: String,
    pub name: String,
    pub limit: i32,
    #[sea_orm(default_value = "{}")]
    pub logic: Json,
    pub action: SurveyQuotaAction,
    #[sea_orm(column_name = "endingCardId")]
    pub ending_card_id: Option<String>,
    #[sea_orm(column_name = "countPartialSubmissions", default_value = false)]
    pub count_partial_submissions: bool,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::survey::Entity",
        from = "Column::SurveyId",
        to = "super::survey::Column::Id",
        on_delete = "Cascade"
    )]
    Survey,
    #[sea_orm(has_many = "super::response_quota_link::Entity")]
    QuotaLinks,
}

impl Related<super::survey::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Survey.def()
    }
}

impl Related<super::response_quota_link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::QuotaLinks.def()
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
