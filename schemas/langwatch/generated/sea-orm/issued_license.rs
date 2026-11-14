use super::currency::Currency;
use super::issued_license_source::IssuedLicenseSource;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "IssuedLicense")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "licenseId")]
    pub license_id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "tokenHash")]
    pub token_hash: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: Option<String>,
    #[sea_orm(column_name = "organizationName")]
    pub organization_name: String,
    pub email: String,
    #[sea_orm(column_name = "planType")]
    pub plan_type: String,
    #[sea_orm(column_name = "maxMembers")]
    pub max_members: i32,
    #[sea_orm(column_name = "maxMembersLite")]
    pub max_members_lite: Option<i32>,
    #[sea_orm(column_name = "liteSeatsUnlimited", default_value = false)]
    pub lite_seats_unlimited: bool,
    #[sea_orm(column_name = "maxMembersDeveloper")]
    pub max_members_developer: Option<i32>,
    #[sea_orm(column_name = "issuedAt")]
    pub issued_at: DateTime,
    #[sea_orm(column_name = "expiresAt")]
    pub expires_at: DateTime,
    pub source: IssuedLicenseSource,
    #[sea_orm(column_name = "issuedById")]
    pub issued_by_id: Option<String>,
    #[sea_orm(column_name = "revokedAt")]
    pub revoked_at: Option<DateTime>,
    #[sea_orm(column_name = "revokedById")]
    pub revoked_by_id: Option<String>,
    #[sea_orm(column_name = "revokedReason")]
    pub revoked_reason: Option<String>,
    #[sea_orm(column_name = "supersededAt")]
    pub superseded_at: Option<DateTime>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "replacesId")]
    pub replaces_id: Option<String>,
    #[sea_orm(column_name = "pendingDeliveryLicense", column_type = "Text")]
    pub pending_delivery_license: Option<String>,
    pub services: Vec<String>,
    #[sea_orm(column_name = "seatRateCents")]
    pub seat_rate_cents: Option<i32>,
    #[sea_orm(column_name = "seatCurrency")]
    pub seat_currency: Option<Currency>,
    #[sea_orm(column_name = "commitUsdCents", default_value = 0)]
    pub commit_usd_cents: i32,
    #[sea_orm(column_name = "overageEnabled", default_value = false)]
    pub overage_enabled: bool,
    #[sea_orm(column_name = "overageMaxUsdCents")]
    pub overage_max_usd_cents: Option<i32>,
    #[sea_orm(column_name = "instanceId")]
    pub instance_id: Option<String>,
    #[sea_orm(column_name = "instanceBoundAt")]
    pub instance_bound_at: Option<DateTime>,
    #[sea_orm(column_name = "lastSyncAt")]
    pub last_sync_at: Option<DateTime>,
    #[sea_orm(column_name = "lastSyncVersion")]
    pub last_sync_version: Option<String>,
    #[sea_orm(column_name = "reportedMembers")]
    pub reported_members: Option<i32>,
    #[sea_orm(column_name = "reportedMembersLite")]
    pub reported_members_lite: Option<i32>,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "virtualKeyId")]
    pub virtual_key_id: Option<String>,
    #[sea_orm(column_name = "seatsRaisedFrom")]
    pub seats_raised_from: Option<i32>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::organization::Entity",
        from = "Column::OrganizationId",
        to = "super::organization::Column::Id",
        on_delete = "Restrict"
    )]
    Organization,
}

impl Related<super::organization::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Organization.def()
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
