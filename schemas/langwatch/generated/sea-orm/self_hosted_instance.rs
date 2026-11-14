use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "SelfHostedInstance")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "instanceId")]
    pub instance_id: String,
    #[sea_orm(column_name = "firstSeenAt")]
    pub first_seen_at: DateTime,
    #[sea_orm(column_name = "lastSeenAt")]
    pub last_seen_at: DateTime,
    pub version: Option<String>,
    #[sea_orm(column_name = "installMethod")]
    pub install_method: Option<String>,
    #[sea_orm(column_name = "chartVersion")]
    pub chart_version: Option<String>,
    pub hostname: Option<String>,
    pub environment: Option<String>,
    #[sea_orm(column_name = "installedAt")]
    pub installed_at: Option<DateTime>,
    #[sea_orm(column_name = "reportSchemaVersion")]
    pub report_schema_version: Option<i32>,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: Option<String>,
    #[sea_orm(column_name = "issuedLicenseId")]
    pub issued_license_id: Option<String>,
    #[sea_orm(column_name = "userEmailDomains")]
    pub user_email_domains: Option<Json>,
    #[sea_orm(column_name = "userDomains")]
    pub user_domains: Vec<String>,
    #[sea_orm(column_name = "latestReport")]
    pub latest_report: Option<Json>,
    #[sea_orm(column_name = "optionalMetricsReported", default_value = true)]
    pub optional_metrics_reported: bool,
    #[sea_orm(column_name = "hostnameReported", default_value = true)]
    pub hostname_reported: bool,
    #[sea_orm(column_name = "reportCount", default_value = 0)]
    pub report_count: i32,
    #[sea_orm(column_name = "lastUnknownFields", default_value = 0)]
    pub last_unknown_fields: i32,
    #[sea_orm(column_name = "raisedSignals")]
    pub raised_signals: Vec<String>,
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
        if insert && self.first_seen_at.is_not_set() {
            self.first_seen_at = Set(now.naive_utc());
        }
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
