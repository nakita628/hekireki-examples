use super::runtime_environment_type::RuntimeEnvironmentType;
use super::task_event_kind::TaskEventKind;
use super::task_event_level::TaskEventLevel;
use super::task_event_status::TaskEventStatus;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "TaskEvent")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    pub message: String,
    #[sea_orm(column_name = "traceId")]
    pub trace_id: String,
    #[sea_orm(column_name = "spanId")]
    pub span_id: String,
    #[sea_orm(column_name = "parentId")]
    pub parent_id: Option<String>,
    pub tracestate: Option<String>,
    #[sea_orm(column_name = "isError", default_value = false)]
    pub is_error: bool,
    #[sea_orm(column_name = "isPartial", default_value = false)]
    pub is_partial: bool,
    #[sea_orm(column_name = "isCancelled", default_value = false)]
    pub is_cancelled: bool,
    #[sea_orm(column_name = "isDebug", default_value = false)]
    pub is_debug: bool,
    #[sea_orm(column_name = "serviceName")]
    pub service_name: String,
    #[sea_orm(column_name = "serviceNamespace")]
    pub service_namespace: String,
    #[sea_orm(default_value = "TRACE")]
    pub level: TaskEventLevel,
    #[sea_orm(default_value = "INTERNAL")]
    pub kind: TaskEventKind,
    #[sea_orm(default_value = "UNSET")]
    pub status: TaskEventStatus,
    pub links: Option<Json>,
    pub events: Option<Json>,
    #[sea_orm(column_name = "startTime")]
    pub start_time: i64,
    #[sea_orm(default_value = "0")]
    pub duration: i64,
    #[sea_orm(column_name = "attemptId")]
    pub attempt_id: Option<String>,
    #[sea_orm(column_name = "attemptNumber")]
    pub attempt_number: Option<i32>,
    #[sea_orm(column_name = "environmentId")]
    pub environment_id: String,
    #[sea_orm(column_name = "environmentType")]
    pub environment_type: RuntimeEnvironmentType,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "projectRef")]
    pub project_ref: String,
    #[sea_orm(column_name = "runId")]
    pub run_id: String,
    #[sea_orm(column_name = "runIsTest", default_value = false)]
    pub run_is_test: bool,
    #[sea_orm(column_name = "idempotencyKey")]
    pub idempotency_key: Option<String>,
    #[sea_orm(column_name = "taskSlug")]
    pub task_slug: String,
    #[sea_orm(column_name = "taskPath")]
    pub task_path: Option<String>,
    #[sea_orm(column_name = "taskExportName")]
    pub task_export_name: Option<String>,
    #[sea_orm(column_name = "workerId")]
    pub worker_id: Option<String>,
    #[sea_orm(column_name = "workerVersion")]
    pub worker_version: Option<String>,
    #[sea_orm(column_name = "queueId")]
    pub queue_id: Option<String>,
    #[sea_orm(column_name = "queueName")]
    pub queue_name: Option<String>,
    #[sea_orm(column_name = "batchId")]
    pub batch_id: Option<String>,
    pub properties: Json,
    pub metadata: Option<Json>,
    pub style: Option<Json>,
    pub output: Option<Json>,
    #[sea_orm(column_name = "outputType")]
    pub output_type: Option<String>,
    pub payload: Option<Json>,
    #[sea_orm(column_name = "payloadType")]
    pub payload_type: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "usageDurationMs", default_value = 0)]
    pub usage_duration_ms: i32,
    #[sea_orm(column_name = "usageCostInCents", default_value = 0)]
    pub usage_cost_in_cents: f64,
    #[sea_orm(column_name = "machinePreset")]
    pub machine_preset: Option<String>,
    #[sea_orm(column_name = "machinePresetCpu")]
    pub machine_preset_cpu: Option<f64>,
    #[sea_orm(column_name = "machinePresetMemory")]
    pub machine_preset_memory: Option<f64>,
    #[sea_orm(column_name = "machinePresetCentsPerMs")]
    pub machine_preset_cents_per_ms: Option<f64>,
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
