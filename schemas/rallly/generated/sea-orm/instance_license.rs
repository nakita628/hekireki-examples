use super::license_status::LicenseStatus;
use super::license_type::LicenseType;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "instance_licenses")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    pub license_key: String,
    pub version: Option<i32>,
    pub r#type: LicenseType,
    pub seats: Option<i32>,
    pub issued_at: DateTime,
    pub expires_at: Option<DateTime>,
    pub licensee_email: Option<String>,
    pub licensee_name: Option<String>,
    #[sea_orm(default_value = false)]
    pub white_label_addon: bool,
    #[sea_orm(default_value = "ACTIVE")]
    pub status: LicenseStatus,
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
        if insert && self.issued_at.is_not_set() {
            self.issued_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
