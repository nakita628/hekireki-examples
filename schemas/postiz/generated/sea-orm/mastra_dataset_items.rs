use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "mastra_dataset_items")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(column_name = "datasetId")]
    pub dataset_id: String,
    #[sea_orm(primary_key, auto_increment = false)]
    #[sea_orm(column_name = "datasetVersion")]
    pub dataset_version: i32,
    #[sea_orm(column_name = "validTo")]
    pub valid_to: Option<i32>,
    #[sea_orm(column_name = "isDeleted")]
    pub is_deleted: bool,
    pub input: Json,
    #[sea_orm(column_name = "groundTruth")]
    pub ground_truth: Option<Json>,
    #[sea_orm(column_name = "requestContext")]
    pub request_context: Option<Json>,
    pub metadata: Option<Json>,
    pub source: Option<Json>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
    #[sea_orm(column_name = "createdAtZ", column_type = "TimestampWithTimeZone")]
    pub created_at_z: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "updatedAtZ", column_type = "TimestampWithTimeZone")]
    pub updated_at_z: Option<DateTimeWithTimeZone>,
    #[sea_orm(column_name = "expectedTrajectory")]
    pub expected_trajectory: Option<Json>,
    #[sea_orm(column_name = "organizationId")]
    pub organization_id: Option<String>,
    #[sea_orm(column_name = "projectId")]
    pub project_id: Option<String>,
    #[sea_orm(column_name = "toolMocks")]
    pub tool_mocks: Option<Json>,
    #[sea_orm(column_name = "unmockedToolPolicy")]
    pub unmocked_tool_policy: Option<String>,
    #[sea_orm(column_name = "scorerIds")]
    pub scorer_ids: Option<Json>,
    #[sea_orm(column_name = "externalId")]
    pub external_id: Option<String>,
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
