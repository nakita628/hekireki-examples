use super::approved_submit_for_order::APPROVED_SUBMIT_FOR_ORDER;
use super::creation_method::CreationMethod;
use super::state::State;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Post")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(default_value = "QUEUE")]
    pub state: State,
    #[sea_orm(column_name = "publishDate")]
    pub publish_date: DateTime,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "integrationId")]
    pub integration_id: String,
    pub content: String,
    #[sea_orm(default_value = 0)]
    pub delay: i32,
    pub group: String,
    #[sea_orm(column_name = "batchId")]
    pub batch_id: Option<String>,
    pub title: Option<String>,
    pub description: Option<String>,
    #[sea_orm(column_name = "parentPostId")]
    pub parent_post_id: Option<String>,
    #[sea_orm(column_name = "releaseId")]
    pub release_id: Option<String>,
    #[sea_orm(column_name = "releaseURL")]
    pub release_url: Option<String>,
    pub settings: Option<String>,
    pub image: Option<String>,
    #[sea_orm(column_name = "submittedForOrderId")]
    pub submitted_for_order_id: Option<String>,
    #[sea_orm(column_name = "submittedForOrganizationId")]
    pub submitted_for_organization_id: Option<String>,
    #[sea_orm(column_name = "approvedSubmitForOrder", default_value = "NO")]
    pub approved_submit_for_order: APPROVED_SUBMIT_FOR_ORDER,
    #[sea_orm(column_name = "creationMethod", default_value = "UNKNOWN")]
    pub creation_method: CreationMethod,
    #[sea_orm(column_name = "lastMessageId")]
    pub last_message_id: Option<String>,
    #[sea_orm(column_name = "intervalInDays")]
    pub interval_in_days: Option<i32>,
    pub error: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "deletedAt")]
    pub deleted_at: Option<DateTime>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::integration::Entity",
        from = "Column::IntegrationId",
        to = "super::integration::Column::Id"
    )]
    Integration,
    #[sea_orm(
        belongs_to = "super::messages::Entity",
        from = "Column::LastMessageId",
        to = "super::messages::Column::Id"
    )]
    LastMessage,
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::OrganizationId",
        to = "super::organization::Column::Id"
    )]
    Organization,
    #[sea_orm(
        belongs_to = "super::post::Entity",
        from = "Column::ParentPostId",
        to = "super::post::Column::Id"
    )]
    ParentPost,
    #[sea_orm(
        belongs_to = "super::orders::Entity",
        from = "Column::SubmittedForOrderId",
        to = "super::orders::Column::Id"
    )]
    SubmittedForOrder,
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::SubmittedForOrganizationId",
        to = "super::organization::Column::Id"
    )]
    SubmittedForOrganization,
    #[sea_orm(has_many = "super::comments::Entity")]
    Comments,
    #[sea_orm(has_many = "super::errors::Entity")]
    Errors,
    #[sea_orm(has_many = "super::payout_problems::Entity")]
    PayoutProblems,
    #[sea_orm(
        has_many = "super::post::Entity",
        from = "Column::Id",
        to = "super::post::Column::ParentPostId"
    )]
    ChildrenPost,
    #[sea_orm(has_many = "super::tags_posts::Entity")]
    Tags,
}

impl Related<super::integration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Integration.def()
    }
}

impl Related<super::messages::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::LastMessage.def()
    }
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
    }
}

impl Related<super::post::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ParentPost.def()
    }
}

impl Related<super::orders::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SubmittedForOrder.def()
    }
}

impl Related<super::comments::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Comments.def()
    }
}

impl Related<super::errors::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Errors.def()
    }
}

impl Related<super::payout_problems::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PayoutProblems.def()
    }
}

impl Related<super::tags_posts::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tags.def()
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
