use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "mastra_scorers")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "scorerId")]
    pub scorer_id: String,
    #[sea_orm(column_name = "traceId")]
    pub trace_id: Option<String>,
    #[sea_orm(column_name = "runId")]
    pub run_id: String,
    pub scorer: Json,
    #[sea_orm(column_name = "preprocessStepResult")]
    pub preprocess_step_result: Option<Json>,
    #[sea_orm(column_name = "extractStepResult")]
    pub extract_step_result: Option<Json>,
    #[sea_orm(column_name = "analyzeStepResult")]
    pub analyze_step_result: Option<Json>,
    pub score: f64,
    pub reason: Option<String>,
    pub metadata: Option<Json>,
    #[sea_orm(column_name = "preprocessPrompt")]
    pub preprocess_prompt: Option<String>,
    #[sea_orm(column_name = "extractPrompt")]
    pub extract_prompt: Option<String>,
    #[sea_orm(column_name = "generateScorePrompt")]
    pub generate_score_prompt: Option<String>,
    #[sea_orm(column_name = "generateReasonPrompt")]
    pub generate_reason_prompt: Option<String>,
    #[sea_orm(column_name = "analyzePrompt")]
    pub analyze_prompt: Option<String>,
    #[sea_orm(column_name = "reasonPrompt")]
    pub reason_prompt: Option<String>,
    pub input: Json,
    pub output: Json,
    #[sea_orm(column_name = "additionalContext")]
    pub additional_context: Option<Json>,
    #[sea_orm(column_name = "runtimeContext")]
    pub runtime_context: Option<Json>,
    #[sea_orm(column_name = "entityType")]
    pub entity_type: Option<String>,
    pub entity: Option<Json>,
    #[sea_orm(column_name = "entityId")]
    pub entity_id: Option<String>,
    pub source: String,
    #[sea_orm(column_name = "resourceId")]
    pub resource_id: Option<String>,
    #[sea_orm(column_name = "threadId")]
    pub thread_id: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "createdAtZ", column_type = "TimestampWithTimeZone")]
    pub created_at_z: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "updatedAtZ", column_type = "TimestampWithTimeZone")]
    pub updated_at_z: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "spanId")]
    pub span_id: Option<String>,
    #[sea_orm(column_name = "requestContext")]
    pub request_context: Option<Json>,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: Option<String>,
    #[sea_orm(column_name = "projectId")]
    pub project_id: Option<String>,
    #[sea_orm(column_name = "batchId")]
    pub batch_id: Option<String>,
    #[sea_orm(column_name = "datasetId")]
    pub dataset_id: Option<String>,
    #[sea_orm(column_name = "datasetItemId")]
    pub dataset_item_id: Option<String>,
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
        if insert && self.created_at_z.is_not_set() {
            self.created_at_z = Set(Some(now.fixed_offset()));
        }
        if insert && self.updated_at_z.is_not_set() {
            self.updated_at_z = Set(Some(now.fixed_offset()));
        }
        Ok(self)
    }
}
