use super::campaign_audience_type::CampaignAudienceType;
use super::campaign_status::CampaignStatus;
use super::template_type::TemplateType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "campaigns")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub name: String,
    pub description: Option<String>,
    #[sea_orm(default_value = "DRAFT")]
    pub status: CampaignStatus,
    pub subject: String,
    #[sea_orm(column_type = "Text")]
    pub body: String,
    pub from: String,
    #[sea_orm(column_name = "fromName")]
    pub from_name: Option<String>,
    #[sea_orm(column_name = "replyTo")]
    pub reply_to: Option<String>,
    #[sea_orm(default_value = "MARKETING")]
    pub r#type: TemplateType,
    #[sea_orm(column_name = "audienceType", default_value = "ALL")]
    pub audience_type: CampaignAudienceType,
    #[sea_orm(column_name = "audienceCondition")]
    pub audience_condition: Option<Json>,
    #[sea_orm(column_name = "segmentId")]
    pub segment_id: Option<String>,
    #[sea_orm(column_name = "scheduledFor")]
    pub scheduled_for: Option<DateTime>,
    #[sea_orm(column_name = "totalRecipients", default_value = 0)]
    pub total_recipients: i32,
    #[sea_orm(column_name = "sentCount", default_value = 0)]
    pub sent_count: i32,
    #[sea_orm(column_name = "deliveredCount", default_value = 0)]
    pub delivered_count: i32,
    #[sea_orm(column_name = "openedCount", default_value = 0)]
    pub opened_count: i32,
    #[sea_orm(column_name = "clickedCount", default_value = 0)]
    pub clicked_count: i32,
    #[sea_orm(column_name = "bouncedCount", default_value = 0)]
    pub bounced_count: i32,
    #[sea_orm(column_name = "complainedCount", default_value = 0)]
    pub complained_count: i32,
    #[sea_orm(column_name = "unsubscribedCount", default_value = 0)]
    pub unsubscribed_count: i32,
    #[sea_orm(default_value = false)]
    pub archived: bool,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "sentAt")]
    pub sent_at: Option<DateTime>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::segment::Entity",
        from = "Column::SegmentId",
        to = "super::segment::Column::Id"
    )]
    Segment,
    #[sea_orm(
        belongs_to = "super::project::Entity",
        from = "Column::ProjectId",
        to = "super::project::Column::Id",
        on_delete = "Cascade"
    )]
    Project,
    #[sea_orm(has_many = "super::email::Entity")]
    Emails,
}

impl Related<super::segment::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Segment.def()
    }
}

impl Related<super::project::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Project.def()
    }
}

impl Related<super::email::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Emails.def()
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
