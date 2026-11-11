use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Alert")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "environmentId")]
    pub environment_id: String,
    #[sea_orm(column_name = "capabilityKey")]
    pub capability_key: String,
    pub params: Json,
    pub condition: Json,
    pub query: Option<String>,
    #[sea_orm(column_name = "expiresAt", column_type = "TimestampWithTimeZone")]
    pub expires_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(default_value = "active")]
    pub lifecycle: String,
    #[sea_orm(default_value = "clear")]
    pub attention: String,
    #[sea_orm(column_name = "observedCondition", default_value = "unknown")]
    pub observed_condition: String,
    #[sea_orm(
        column_name = "observedConditionChangedAt",
        column_type = "TimestampWithTimeZone"
    )]
    pub observed_condition_changed_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "pendingMetSince", column_type = "TimestampWithTimeZone")]
    pub pending_met_since: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "evaluatorState")]
    pub evaluator_state: Option<Json>,
    #[sea_orm(column_name = "evaluationLeaseId")]
    pub evaluation_lease_id: Option<String>,
    #[sea_orm(
        column_name = "evaluationLeaseUntil",
        column_type = "TimestampWithTimeZone"
    )]
    pub evaluation_lease_until: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "lastEvaluatedAt", column_type = "TimestampWithTimeZone")]
    pub last_evaluated_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(
        column_name = "nextEvaluationAt",
        column_type = "TimestampWithTimeZone"
    )]
    pub next_evaluation_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(default_value = "[]")]
    pub notifiers: Json,
    pub name: Option<String>,
    #[sea_orm(column_name = "createdBy")]
    pub created_by: String,
    #[sea_orm(column_name = "createdAt", column_type = "TimestampWithTimeZone")]
    pub created_at: DateTimeWithTimeZone,
    #[sea_orm(column_name = "updatedAt", column_type = "TimestampWithTimeZone")]
    pub updated_at: DateTimeWithTimeZone,
    #[sea_orm(column_name = "resolvedAt", column_type = "TimestampWithTimeZone")]
    pub resolved_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "resolvedBy")]
    pub resolved_by: Option<String>,
    #[sea_orm(column_name = "stoppedAt", column_type = "TimestampWithTimeZone")]
    pub stopped_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "stoppedBy")]
    pub stopped_by: Option<String>,
    #[sea_orm(
        column_name = "lastOccurrenceAt",
        column_type = "TimestampWithTimeZone"
    )]
    pub last_occurrence_at: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "occurrencesSinceRaise", default_value = 0)]
    pub occurrences_since_raise: i32,
    #[sea_orm(column_name = "occurrencesTotal", default_value = "0")]
    pub occurrences_total: i64,
    #[sea_orm(column_name = "groupId")]
    pub group_id: Option<String>,
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
            self.created_at = Set(now.fixed_offset());
        }
        if insert && self.updated_at.is_not_set() {
            self.updated_at = Set(now.fixed_offset());
        }
        Ok(self)
    }
}
