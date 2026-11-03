use super::creative_show_on::CreativeShowOn;
use super::creative_status::CreativeStatus;
use super::creative_type::CreativeType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Creative")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub created_at: DateTime,
    pub updated_at: DateTime,
    pub name: String,
    pub r#type: CreativeType,
    #[sea_orm(default_value = "draft")]
    pub status: CreativeStatus,
    #[sea_orm(column_name = "publishedAt")]
    pub published_at: Option<DateTime>,
    #[sea_orm(column_name = "archivedAt")]
    pub archived_at: Option<DateTime>,
    #[sea_orm(column_name = "workspaceId")]
    pub workspace_id: String,
    #[sea_orm(column_name = "createdBy")]
    pub created_by: Option<String>,
    pub content: Json,
    pub styling: Json,
    #[sea_orm(column_name = "surveyId")]
    pub survey_id: Option<String>,
    #[sea_orm(column_name = "linkUrl")]
    pub link_url: Option<String>,
    #[sea_orm(column_name = "showOn", default_value = "pageView")]
    pub show_on: CreativeShowOn,
    #[sea_orm(column_name = "showConditions")]
    pub show_conditions: Option<Json>,
    #[sea_orm(column_name = "displayPercentage")]
    pub display_percentage: Option<f64>,
    #[sea_orm(column_name = "startAt")]
    pub start_at: Option<DateTime>,
    #[sea_orm(column_name = "endAt")]
    pub end_at: Option<DateTime>,
    #[sea_orm(column_name = "autoSelectLanguage")]
    pub auto_select_language: Option<bool>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::workspace::Entity",
        from = "Column::WorkspaceId",
        to = "super::workspace::Column::Id",
        on_delete = "Cascade"
    )]
    Workspace,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::CreatedBy",
        to = "super::user::Column::Id",
        on_delete = "SetNull"
    )]
    Creator,
    #[sea_orm(
        belongs_to = "super::survey::Entity",
        from = "(Column::SurveyId, Column::WorkspaceId)",
        to = "(super::survey::Column::Id, super::survey::Column::WorkspaceId)",
        on_update = "NoAction",
        on_delete = "NoAction"
    )]
    Survey,
    #[sea_orm(has_many = "super::creative_trigger::Entity")]
    Triggers,
    #[sea_orm(has_many = "super::creative_language::Entity")]
    Languages,
    #[sea_orm(has_many = "super::creative_daily_stat::Entity")]
    DailyStats,
    #[sea_orm(has_many = "super::creative_daily_page_stat::Entity")]
    DailyPageStats,
}

impl Related<super::workspace::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Workspace.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Creator.def()
    }
}

impl Related<super::survey::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Survey.def()
    }
}

impl Related<super::creative_trigger::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Triggers.def()
    }
}

impl Related<super::creative_language::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Languages.def()
    }
}

impl Related<super::creative_daily_stat::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DailyStats.def()
    }
}

impl Related<super::creative_daily_page_stat::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DailyPageStats.def()
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
