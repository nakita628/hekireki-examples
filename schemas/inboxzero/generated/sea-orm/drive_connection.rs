use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "DriveConnection")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    pub provider: String,
    pub email: String,
    #[sea_orm(column_name = "accessToken")]
    pub access_token: Option<String>,
    #[sea_orm(column_name = "refreshToken")]
    pub refresh_token: Option<String>,
    #[sea_orm(column_name = "expiresAt")]
    pub expires_at: Option<DateTime>,
    #[sea_orm(column_name = "isConnected", default_value = true)]
    pub is_connected: bool,
    #[sea_orm(column_name = "emailAccountId")]
    pub email_account_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::email_account::Entity",
        from = "Column::EmailAccountId",
        to = "super::email_account::Column::Id",
        on_delete = "Cascade"
    )]
    EmailAccount,
    #[sea_orm(has_many = "super::document_filing::Entity")]
    DocumentFilings,
    #[sea_orm(has_many = "super::filing_folder::Entity")]
    FilingFolders,
    #[sea_orm(has_many = "super::attachment_source::Entity")]
    AttachmentSources,
}

impl Related<super::email_account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmailAccount.def()
    }
}

impl Related<super::document_filing::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::DocumentFilings.def()
    }
}

impl Related<super::filing_folder::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::FilingFolders.def()
    }
}

impl Related<super::attachment_source::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AttachmentSources.def()
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
