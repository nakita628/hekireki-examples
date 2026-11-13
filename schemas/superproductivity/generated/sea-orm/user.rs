use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "users")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(unique)]
    pub email: String,
    pub password_hash: Option<String>,
    #[sea_orm(default_value = 0)]
    pub is_verified: i32,
    pub verification_token: Option<String>,
    pub verification_token_expires_at: Option<i64>,
    #[sea_orm(default_value = 0)]
    pub verification_resend_count: i32,
    pub reset_password_token: Option<String>,
    pub reset_password_token_expires_at: Option<i64>,
    pub passkey_recovery_token: Option<String>,
    pub passkey_recovery_token_expires_at: Option<i64>,
    pub login_token: Option<String>,
    pub login_token_expires_at: Option<i64>,
    #[sea_orm(default_value = 0)]
    pub failed_login_attempts: i32,
    pub locked_until: Option<i64>,
    #[sea_orm(default_value = 0)]
    pub token_version: i32,
    pub terms_accepted_at: Option<i64>,
    pub created_at: DateTime,
    #[sea_orm(default_value = "104857600")]
    pub storage_quota_bytes: i64,
    #[sea_orm(default_value = "0")]
    pub storage_used_bytes: i64,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::operation::Entity")]
    Operations,
    #[sea_orm(has_many = "super::sync_device::Entity")]
    Devices,
    #[sea_orm(has_many = "super::passkey::Entity")]
    Passkeys,
    #[sea_orm(has_many = "super::pending_passkey_registration::Entity")]
    PendingPasskeyRegistrations,
    #[sea_orm(has_many = "super::capture_grant::Entity")]
    CaptureGrants,
    #[sea_orm(has_many = "super::capture_item::Entity")]
    CaptureItems,
    #[sea_orm(has_many = "super::capture_auth::Entity")]
    CaptureAuthorizations,
    #[sea_orm(has_many = "super::connect_request::Entity")]
    ConnectRequests,
    #[sea_orm(has_one = "super::user_sync_state::Entity")]
    SyncState,
    #[sea_orm(has_one = "super::capture_key::Entity")]
    CaptureKey,
    #[sea_orm(has_one = "super::capture_connect_code::Entity")]
    CaptureConnectCode,
}

impl Related<super::operation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Operations.def()
    }
}

impl Related<super::sync_device::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Devices.def()
    }
}

impl Related<super::passkey::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Passkeys.def()
    }
}

impl Related<super::pending_passkey_registration::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PendingPasskeyRegistrations.def()
    }
}

impl Related<super::capture_grant::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CaptureGrants.def()
    }
}

impl Related<super::capture_item::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CaptureItems.def()
    }
}

impl Related<super::capture_auth::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CaptureAuthorizations.def()
    }
}

impl Related<super::connect_request::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ConnectRequests.def()
    }
}

impl Related<super::user_sync_state::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::SyncState.def()
    }
}

impl Related<super::capture_key::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CaptureKey.def()
    }
}

impl Related<super::capture_connect_code::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::CaptureConnectCode.def()
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
