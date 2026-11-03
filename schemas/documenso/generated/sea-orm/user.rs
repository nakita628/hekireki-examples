use super::identity_provider::IdentityProvider;
use super::role::Role;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "User")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    pub name: Option<String>,
    #[sea_orm(unique)]
    pub email: String,
    #[sea_orm(column_name = "emailVerified")]
    pub email_verified: Option<DateTime>,
    pub password: Option<String>,
    pub source: Option<String>,
    pub signature: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "lastSignedIn")]
    pub last_signed_in: DateTime,
    pub roles: Vec<Role>,
    #[sea_orm(column_name = "identityProvider", default_value = "DOCUMENSO")]
    pub identity_provider: IdentityProvider,
    #[sea_orm(column_name = "avatarImageId")]
    pub avatar_image_id: Option<String>,
    #[sea_orm(default_value = false)]
    pub disabled: bool,
    #[sea_orm(column_name = "twoFactorSecret")]
    pub two_factor_secret: Option<String>,
    #[sea_orm(column_name = "twoFactorEnabled", default_value = false)]
    pub two_factor_enabled: bool,
    #[sea_orm(column_name = "twoFactorBackupCodes")]
    pub two_factor_backup_codes: Option<String>,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::avatar_image::Entity",
        from = "Column::AvatarImageId",
        to = "super::avatar_image::Column::Id",
        on_delete = "SetNull"
    )]
    AvatarImage,
    #[sea_orm(has_many = "super::account::Entity")]
    Accounts,
    #[sea_orm(has_many = "super::session::Entity")]
    Sessions,
    #[sea_orm(has_many = "super::password_reset_token::Entity")]
    PasswordResetTokens,
    #[sea_orm(has_many = "super::organisation::Entity")]
    OwnedOrganisations,
    #[sea_orm(has_many = "super::organisation_member::Entity")]
    OrganisationMember,
    #[sea_orm(has_many = "super::folder::Entity")]
    Folders,
    #[sea_orm(has_many = "super::envelope::Entity")]
    Envelopes,
    #[sea_orm(has_many = "super::verification_token::Entity")]
    VerificationTokens,
    #[sea_orm(has_many = "super::api_token::Entity")]
    ApiTokens,
    #[sea_orm(has_many = "super::user_security_audit_log::Entity")]
    SecurityAuditLogs,
    #[sea_orm(has_many = "super::webhook::Entity")]
    Webhooks,
    #[sea_orm(has_many = "super::site_settings::Entity")]
    SiteSettings,
    #[sea_orm(has_many = "super::passkey::Entity")]
    Passkeys,
}

impl Related<super::avatar_image::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::AvatarImage.def()
    }
}

impl Related<super::account::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Accounts.def()
    }
}

impl Related<super::session::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Sessions.def()
    }
}

impl Related<super::password_reset_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PasswordResetTokens.def()
    }
}

impl Related<super::organisation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OwnedOrganisations.def()
    }
}

impl Related<super::organisation_member::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::OrganisationMember.def()
    }
}

impl Related<super::folder::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Folders.def()
    }
}

impl Related<super::envelope::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Envelopes.def()
    }
}

impl Related<super::verification_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::VerificationTokens.def()
    }
}

impl Related<super::api_token::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ApiTokens.def()
    }
}

impl Related<super::user_security_audit_log::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SecurityAuditLogs.def()
    }
}

impl Related<super::webhook::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Webhooks.def()
    }
}

impl Related<super::site_settings::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SiteSettings.def()
    }
}

impl Related<super::passkey::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Passkeys.def()
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
        if insert && self.last_signed_in.is_not_set() {
            self.last_signed_in = Set(now.naive_utc());
        }
        Ok(self)
    }
}
