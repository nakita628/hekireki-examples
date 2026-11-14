use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "ActivationCode")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "codeHash")]
    pub code_hash: String,
    #[sea_orm(column_name = "codeHint")]
    pub code_hint: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "organizationName")]
    pub organization_name: String,
    pub email: String,
    #[sea_orm(column_name = "planType")]
    pub plan_type: String,
    #[sea_orm(column_name = "maxMembers")]
    pub max_members: i32,
    #[sea_orm(column_name = "maxMembersLite", default_value = 0)]
    pub max_members_lite: Option<i32>,
    #[sea_orm(column_name = "liteSeatsUnlimited", default_value = false)]
    pub lite_seats_unlimited: bool,
    #[sea_orm(column_name = "maxMembersDeveloper")]
    pub max_members_developer: Option<i32>,
    #[sea_orm(column_name = "licenseTermDays")]
    pub license_term_days: i32,
    pub services: Vec<String>,
    #[sea_orm(column_name = "expiresAt")]
    pub expires_at: DateTime,
    #[sea_orm(default_value = false)]
    pub reusable: bool,
    #[sea_orm(column_name = "redeemedAt")]
    pub redeemed_at: Option<DateTime>,
    #[sea_orm(column_name = "redeemedByInstanceId")]
    pub redeemed_by_instance_id: Option<String>,
    #[sea_orm(column_name = "issuedLicenseId")]
    pub issued_license_id: Option<String>,
    #[sea_orm(column_name = "redemptionCount", default_value = 0)]
    pub redemption_count: i32,
    #[sea_orm(column_name = "revokedAt")]
    pub revoked_at: Option<DateTime>,
    #[sea_orm(column_name = "revokedById")]
    pub revoked_by_id: Option<String>,
    #[sea_orm(column_name = "createdById")]
    pub created_by_id: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {}

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
