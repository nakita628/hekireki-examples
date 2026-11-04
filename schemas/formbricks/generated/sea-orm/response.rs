use super::response_outcome::ResponseOutcome;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Response")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    #[sea_orm(default_value = false)]
    pub finished: bool,
    #[sea_orm(column_name = "surveyId")]
    pub survey_id: String,
    #[sea_orm(column_name = "contactId")]
    pub contact_id: Option<String>,
    #[sea_orm(column_name = "endingId")]
    pub ending_id: Option<String>,
    #[sea_orm(default_value = "{}")]
    pub data: Json,
    #[sea_orm(default_value = "{}")]
    pub variables: Json,
    #[sea_orm(default_value = "{}")]
    pub ttc: Json,
    #[sea_orm(default_value = "{}")]
    pub meta: Json,
    pub ingest_flags: Option<Json>,
    pub outcome: Option<ResponseOutcome>,
    #[sea_orm(column_name = "contactAttributes")]
    pub contact_attributes: Option<Json>,
    #[sea_orm(column_name = "singleUseId")]
    pub single_use_id: Option<String>,
    pub language: Option<String>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "displayId")]
    pub display_id: Option<String>,
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
    #[sea_orm(
        belongs_to = "super::contact::Entity",
        from = "Column::ContactId",
        to = "super::contact::Column::Id",
        on_delete = "Cascade"
    )]
    Contact,
    #[sea_orm(
        belongs_to = "super::display::Entity",
        from = "Column::DisplayId",
        to = "super::display::Column::Id"
    )]
    Display,
    #[sea_orm(has_many = "super::tags_on_responses::Entity")]
    Tags,
    #[sea_orm(has_many = "super::response_quota_link::Entity")]
    QuotaLinks,
    #[sea_orm(has_many = "super::workflow_run::Entity")]
    WorkflowRuns,
}

impl Related<super::survey::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Survey.def()
    }
}

impl Related<super::contact::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Contact.def()
    }
}

impl Related<super::display::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Display.def()
    }
}

impl Related<super::tags_on_responses::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tags.def()
    }
}

impl Related<super::response_quota_link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::QuotaLinks.def()
    }
}

impl Related<super::workflow_run::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::WorkflowRuns.def()
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
