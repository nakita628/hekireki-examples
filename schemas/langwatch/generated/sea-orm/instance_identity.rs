use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "InstanceIdentity")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "instanceId")]
    pub instance_id: String,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "lastReportAt")]
    pub last_report_at: Option<DateTime>,
    #[sea_orm(column_name = "lastReportError")]
    pub last_report_error: Option<String>,
    #[sea_orm(column_name = "optionalMetricsOptOut", default_value = false)]
    pub optional_metrics_opt_out: bool,
    #[sea_orm(column_name = "hostnameOptOut", default_value = false)]
    pub hostname_opt_out: bool,
    #[sea_orm(column_name = "upgradeDiagnosticsOptOut", default_value = false)]
    pub upgrade_diagnostics_opt_out: bool,
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
        Ok(self)
    }
}
