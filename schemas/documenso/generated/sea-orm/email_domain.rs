use super::email_domain_status::EmailDomainStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "EmailDomain")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(default_value = "PENDING")]
    pub status: EmailDomainStatus,
    #[sea_orm(unique)]
    pub selector: String,
    #[sea_orm(unique)]
    pub domain: String,
    #[sea_orm(column_name = "publicKey")]
    pub public_key: String,
    #[sea_orm(column_name = "privateKey")]
    pub private_key: String,
    #[sea_orm(column_name = "lastVerifiedAt")]
    pub last_verified_at: Option<DateTime>,
    #[sea_orm(column_name = "organisationId")]
    pub organisation_id: String,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organisation::Entity",
        from = "Column::OrganisationId",
        to = "super::organisation::Column::Id",
        on_delete = "Cascade"
    )]
    Organisation,
    #[sea_orm(has_many = "super::organisation_email::Entity")]
    Emails,
}

impl Related<super::organisation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organisation.def()
    }
}

impl Related<super::organisation_email::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Emails.def()
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
