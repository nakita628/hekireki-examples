use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "mastra_observational_memory")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "lookupKey")]
    pub lookup_key: String,
    pub scope: String,
    #[sea_orm(column_name = "resourceId")]
    pub resource_id: Option<String>,
    #[sea_orm(column_name = "threadId")]
    pub thread_id: Option<String>,
    #[sea_orm(column_name = "activeObservations")]
    pub active_observations: String,
    #[sea_orm(column_name = "activeObservationsPendingUpdate")]
    pub active_observations_pending_update: Option<String>,
    #[sea_orm(column_name = "originType")]
    pub origin_type: String,
    pub config: String,
    #[sea_orm(column_name = "generationCount")]
    pub generation_count: i32,
    #[sea_orm(column_name = "lastObservedAt")]
    pub last_observed_at: Option<DateTime>,
    #[sea_orm(column_name = "lastReflectionAt")]
    pub last_reflection_at: Option<DateTime>,
    #[sea_orm(column_name = "pendingMessageTokens")]
    pub pending_message_tokens: i32,
    #[sea_orm(column_name = "totalTokensObserved")]
    pub total_tokens_observed: i32,
    #[sea_orm(column_name = "observationTokenCount")]
    pub observation_token_count: i32,
    #[sea_orm(column_name = "isObserving")]
    pub is_observing: bool,
    #[sea_orm(column_name = "isReflecting")]
    pub is_reflecting: bool,
    #[sea_orm(column_name = "observedMessageIds")]
    pub observed_message_ids: Option<Json>,
    #[sea_orm(column_name = "observedTimezone")]
    pub observed_timezone: Option<String>,
    #[sea_orm(column_name = "bufferedObservations")]
    pub buffered_observations: Option<String>,
    #[sea_orm(column_name = "bufferedObservationTokens")]
    pub buffered_observation_tokens: Option<i32>,
    #[sea_orm(column_name = "bufferedMessageIds")]
    pub buffered_message_ids: Option<Json>,
    #[sea_orm(column_name = "bufferedReflection")]
    pub buffered_reflection: Option<String>,
    #[sea_orm(column_name = "bufferedReflectionTokens")]
    pub buffered_reflection_tokens: Option<i32>,
    #[sea_orm(column_name = "bufferedReflectionInputTokens")]
    pub buffered_reflection_input_tokens: Option<i32>,
    #[sea_orm(column_name = "reflectedObservationLineCount")]
    pub reflected_observation_line_count: Option<i32>,
    #[sea_orm(column_name = "bufferedObservationChunks")]
    pub buffered_observation_chunks: Option<Json>,
    #[sea_orm(column_name = "isBufferingObservation")]
    pub is_buffering_observation: bool,
    #[sea_orm(column_name = "isBufferingReflection")]
    pub is_buffering_reflection: bool,
    #[sea_orm(column_name = "lastBufferedAtTokens")]
    pub last_buffered_at_tokens: i32,
    #[sea_orm(column_name = "lastBufferedAtTime")]
    pub last_buffered_at_time: Option<DateTime>,
    pub metadata: Option<Json>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "lastObservedAtZ", column_type = "TimestampWithTimeZone")]
    pub last_observed_at_z: Option<DateTimeWithTimeZone>,
    #[sea_orm(
        column_name = "lastReflectionAtZ",
        column_type = "TimestampWithTimeZone"
    )]
    pub last_reflection_at_z: Option<DateTimeWithTimeZone>,
    #[sea_orm(
        column_name = "lastBufferedAtTimeZ",
        column_type = "TimestampWithTimeZone"
    )]
    pub last_buffered_at_time_z: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "createdAtZ", column_type = "TimestampWithTimeZone")]
    pub created_at_z: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "updatedAtZ", column_type = "TimestampWithTimeZone")]
    pub updated_at_z: Option<DateTimeWithTimeZone>,
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
        if insert && self.last_observed_at_z.is_not_set() {
            self.last_observed_at_z = Set(Some(now.fixed_offset()));
        }
        if insert && self.last_reflection_at_z.is_not_set() {
            self.last_reflection_at_z = Set(Some(now.fixed_offset()));
        }
        if insert && self.last_buffered_at_time_z.is_not_set() {
            self.last_buffered_at_time_z = Set(Some(now.fixed_offset()));
        }
        if insert && self.created_at_z.is_not_set() {
            self.created_at_z = Set(Some(now.fixed_offset()));
        }
        if insert && self.updated_at_z.is_not_set() {
            self.updated_at_z = Set(Some(now.fixed_offset()));
        }
        Ok(self)
    }
}
