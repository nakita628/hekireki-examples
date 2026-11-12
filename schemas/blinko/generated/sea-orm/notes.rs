use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "notes")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(default_value = 0)]
    pub r#type: i32,
    #[sea_orm(default_value = "")]
    pub content: String,
    #[sea_orm(column_name = "isArchived", default_value = false)]
    pub is_archived: bool,
    #[sea_orm(column_name = "isRecycle", default_value = false)]
    pub is_recycle: bool,
    #[sea_orm(column_name = "isShare", default_value = false)]
    pub is_share: bool,
    #[sea_orm(column_name = "isTop", default_value = false)]
    pub is_top: bool,
    #[sea_orm(column_name = "isReviewed", default_value = false)]
    pub is_reviewed: bool,
    #[sea_orm(column_name = "sharePassword", default_value = "")]
    pub share_password: String,
    #[sea_orm(column_name = "shareEncryptedUrl")]
    pub share_encrypted_url: Option<String>,
    #[sea_orm(column_name = "shareExpiryDate", column_type = "TimestampWithTimeZone")]
    pub share_expiry_date: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "shareMaxView", default_value = 0)]
    pub share_max_view: Option<i32>,
    #[sea_orm(column_name = "shareViewCount", default_value = 0)]
    pub share_view_count: Option<i32>,
    pub metadata: Option<Json>,
    #[sea_orm(column_name = "accountId")]
    pub account_id: Option<i32>,
    #[sea_orm(column_name = "sortOrder", default_value = 0)]
    pub sort_order: i32,
    #[sea_orm(column_name = "createdAt", column_type = "TimestampWithTimeZone")]
    pub created_at: DateTimeWithTimeZone,
    #[sea_orm(column_name = "updatedAt", column_type = "TimestampWithTimeZone")]
    pub updated_at: DateTimeWithTimeZone,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::accounts::Entity",
        from = "Column::AccountId",
        to = "super::accounts::Column::Id"
    )]
    Account,
    #[sea_orm(has_many = "super::attachments::Entity")]
    Attachments,
    #[sea_orm(has_many = "super::tags_to_note::Entity")]
    Tags,
    #[sea_orm(
        has_many = "super::note_reference::Entity",
        from = "Column::Id",
        to = "super::note_reference::Column::ToNoteId"
    )]
    ReferencedBy,
    #[sea_orm(
        has_many = "super::note_reference::Entity",
        from = "Column::Id",
        to = "super::note_reference::Column::FromNoteId"
    )]
    References,
    #[sea_orm(has_many = "super::comments::Entity")]
    Comments,
    #[sea_orm(has_many = "super::note_history::Entity")]
    Histories,
    #[sea_orm(has_many = "super::note_internal_share::Entity")]
    InternalShares,
}

impl Related<super::accounts::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Account.def()
    }
}

impl Related<super::attachments::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Attachments.def()
    }
}

impl Related<super::tags_to_note::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tags.def()
    }
}

impl Related<super::note_reference::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ReferencedBy.def()
    }
}

impl Related<super::comments::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Comments.def()
    }
}

impl Related<super::note_history::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Histories.def()
    }
}

impl Related<super::note_internal_share::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::InternalShares.def()
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
            self.created_at = Set(now.fixed_offset());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
