use super::insight_state::InsightState;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "project_insights")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_type = "Uuid")]
    pub id: String,
    #[sea_orm(column_name = "projectId")]
    pub project_id: String,
    #[sea_orm(column_name = "moduleKey")]
    pub module_key: String,
    #[sea_orm(column_name = "dimensionKey")]
    pub dimension_key: String,
    #[sea_orm(column_name = "windowKind")]
    pub window_kind: String,
    #[sea_orm(default_value = "active")]
    pub state: InsightState,
    pub title: String,
    pub summary: Option<String>,
    #[sea_orm(column_name = "displayName", default_value = "")]
    pub display_name: String,
    #[sea_orm(default_value = "{}")]
    pub payload: Json,
    pub direction: Option<String>,
    #[sea_orm(column_name = "impactScore", default_value = 0)]
    pub impact_score: f64,
    #[sea_orm(column_name = "severityBand")]
    pub severity_band: Option<String>,
    #[sea_orm(column_name = "relevanceScore")]
    pub relevance_score: Option<f64>,
    #[sea_orm(column_name = "aiSummary")]
    pub ai_summary: Option<String>,
    #[sea_orm(column_name = "aiCategory")]
    pub ai_category: Option<String>,
    #[sea_orm(column_name = "emailWorthy", default_value = false)]
    pub email_worthy: bool,
    #[sea_orm(column_name = "referenceWorthy", default_value = false)]
    pub reference_worthy: bool,
    #[sea_orm(column_name = "enrichedAt")]
    pub enriched_at: Option<DateTime>,
    #[sea_orm(column_name = "enrichVersion")]
    pub enrich_version: Option<i32>,
    #[sea_orm(default_value = 1)]
    pub version: i32,
    #[sea_orm(column_name = "threadId", column_type = "Uuid")]
    pub thread_id: String,
    #[sea_orm(column_name = "windowStart")]
    pub window_start: Option<DateTime>,
    #[sea_orm(column_name = "windowEnd")]
    pub window_end: Option<DateTime>,
    #[sea_orm(column_name = "firstDetectedAt")]
    pub first_detected_at: DateTime,
    #[sea_orm(column_name = "lastUpdatedAt")]
    pub last_updated_at: DateTime,
    #[sea_orm(column_name = "lastSeenAt")]
    pub last_seen_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(has_many = "super::insight_event::Entity")]
    Events,
}

impl Related<super::insight_event::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Events.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.first_detected_at.is_not_set() {
            self.first_detected_at = Set(now.naive_utc());
        }
        if !self.last_updated_at.is_set() {
            self.last_updated_at = Set(now.naive_utc());
        }
        if insert && self.last_seen_at.is_not_set() {
            self.last_seen_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
